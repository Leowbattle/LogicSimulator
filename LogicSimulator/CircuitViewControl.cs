using LogicSimulator.Simulation;
using LogicSimulator.Simulation.Nodes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LogicSimulator
{
	public partial class CircuitViewControl : Control
	{
		public CircuitViewControl()
		{
			InitializeComponent();

			ResizeRedraw = true;
			DoubleBuffered = true;

			MouseDown += CircuitViewControl_MouseDown;
			MouseUp += CircuitViewControl_MouseUp;
			MouseMove += CircuitViewControl_MouseMove;
			MouseWheel += CircuitViewControl_MouseWheel;

			circuit = new Circuit();

			circuit.Nodes.Add(new ExampleNode(new PointF(50, 50)));
			circuit.Nodes.Add(new ExampleNode(new PointF(150, 50)));

			circuit.Nodes.Add(new Switch(new PointF(200, 200)));
		}

		public Circuit circuit;

		public void AddNode(Node node)
		{
			circuit.Nodes.Add(node);
			Invalidate();
		}

		Font font = new Font(FontFamily.GenericMonospace, 10);

		PointF camPos = new PointF(0, 0);
		float scale = 1;

		public PointF ScreenCentre => ScreenToWorld(new PointF(Width / 2, Height / 2));

		protected override void OnPaint(PaintEventArgs pe)
		{
			base.OnPaint(pe);

			var g = pe.Graphics;
			g.SmoothingMode = SmoothingMode.AntiAlias;
			g.TextRenderingHint = TextRenderingHint.AntiAlias;

			g.ScaleTransform(scale, scale);
			g.TranslateTransform(-camPos.X, -camPos.Y);

			DrawGridLines(g);

			g.DrawString("Test", font, Brushes.Black, 0, 0);

			DrawWireDrag(g);

			circuit.OnPaint(g);
		}

		private void DrawWireDrag(Graphics g)
		{
			if (dragType != DragType.Wire)
			{
				return;
			}

			g.DrawLine(Pens.Red, dragStart, ScreenToWorld(lastMousePos));
		}

		private void DrawGridLines(Graphics g)
		{
			g.SmoothingMode = SmoothingMode.HighSpeed;

			var lineDistance = 50;
			var numLinesX = (int)(Width / lineDistance / scale + 1);
			var numLinesY = (int)(Height / lineDistance / scale + 1);

			//PointF[] linePoints = new PointF[numLinesX * numLinesY * 4];
			//int pointI = 0;

			for (int i = 0; i <= numLinesX; i++)
			{
				var x = i * lineDistance + camPos.X - camPos.X % lineDistance;

				//linePoints[pointI++] = new PointF(x, camPos.Y);
				//linePoints[pointI++] = new PointF(x, Height / scale + camPos.Y);
				g.DrawLine(Pens.Black, x, camPos.Y, x, Height / scale + camPos.Y);
			}
			for (int i = 0; i <= numLinesY; i++)
			{
				var y = i * lineDistance + camPos.Y - camPos.Y % lineDistance;

				//linePoints[pointI++] = new PointF(camPos.X, y);
				//linePoints[pointI++] = new PointF(Width / scale + camPos.X, y);
				g.DrawLine(Pens.Black, camPos.X, y, Width / scale + camPos.X, y);
			}

			//g.DrawLines(Pens.Black, linePoints);

			g.SmoothingMode = SmoothingMode.AntiAlias;
		}

		public PointF ScreenToWorld(PointF screen)
		{
			return new PointF(
				screen.X / scale + camPos.X,
				screen.Y / scale + camPos.Y);
		}

		public PointF WorldToScreen(PointF world)
		{
			return new PointF(
				(world.X - camPos.X) * scale,
				(world.Y - camPos.Y) * scale);
		}

		private void CircuitViewControl_MouseWheel(object sender, MouseEventArgs e)
		{
			float scaleChange = scale * e.Delta / 2000f;
			float newScale = scale + scaleChange;

			var oldWorldMouseX = e.X / scale + camPos.X;
			var oldWorldMouseY = e.Y / scale + camPos.Y;

			var worldMouseX = e.X / newScale + camPos.X;
			var worldMouseY = e.Y / newScale + camPos.Y;

			camPos.X -= worldMouseX - oldWorldMouseX;
			camPos.Y -= worldMouseY - oldWorldMouseY;

			scale = newScale;

			Invalidate();
		}

		DragType dragType;
		PointF lastMousePos = new PointF(0, 0);
		Node draggedNode = null;
		PointF dragStart;
		Output draggedOutput;
		bool dragMoved;

		private void CircuitViewControl_MouseMove(object sender, MouseEventArgs e)
		{
			if (dragType != DragType.None)
			{
				dragMoved = true;
			}

			float dx = (e.X - lastMousePos.X) / scale;
			float dy = (e.Y - lastMousePos.Y) / scale;

			switch (dragType)
			{
				case DragType.None:
					break;

				case DragType.Camera:
					camPos.X -= dx;
					camPos.Y -= dy;

					Invalidate();

					break;

				case DragType.Node:
					draggedNode.Rect.X += dx;
					draggedNode.Rect.Y += dy;

					Invalidate();

					break;

				case DragType.Wire:
					Invalidate();
					break;
			}

			lastMousePos = e.Location;
		}

		private void CircuitViewControl_MouseUp(object sender, MouseEventArgs e)
		{
			PointF worldCursor = ScreenToWorld(e.Location);

			if (dragType == DragType.Wire)
			{
				foreach (var node in circuit.Nodes)
				{
					foreach (var i in node.Inputs)
					{
						var posx = node.Rect.X + i.Pos.X;
						var posy = node.Rect.Y + i.Pos.Y;
						var r = 5;

						var rect = RectangleF.FromLTRB(posx - r, posy - r, posx + r, posy + r);
						if (rect.Contains(worldCursor))
						{
							Console.WriteLine("drop");

							if (!draggedOutput.Inputs.Contains(i))
							{
								if (i.Source != null)
								{
									i.Source.Inputs.Remove(i);
								}

								i.Source = draggedOutput;
								draggedOutput.Inputs.Add(i);

								circuit.EvaluateCircuit(draggedOutput.Node);
							}

							goto loopend;
						}
					}
				}
			}
			else if (dragType == DragType.Node)
			{
				foreach (var node in circuit.Nodes)
				{
					if (node.Rect.Contains(worldCursor))
					{
						if (node is InputNode inp && !dragMoved)
						{
							Console.WriteLine("interact");
							inp.Interact(PointF.Subtract(worldCursor, new SizeF(node.Rect.Location)), circuit);
						}

						goto loopend;
					}
				}
			}
			loopend:;

			dragType = DragType.None;
			draggedNode = null;
			draggedOutput = null;
			dragMoved = false;

			Invalidate();
		}

		private void CircuitViewControl_MouseDown(object sender, MouseEventArgs e)
		{
			dragType = DragType.None;
			draggedNode = null;
			draggedOutput = null;
			dragMoved = false;

			PointF worldCursor = ScreenToWorld(e.Location);
			dragStart = worldCursor;

			// You cannot remove items from a list while iterating through it, so nodes to be deleted are added
			// to another list and deleted after the main iteration.
			List<Node> deleteList = new List<Node>();

			foreach (var node in circuit.Nodes)
			{
				foreach (var o in node.Outputs)
				{
					var posx = node.Rect.X + o.Pos.X;
					var posy = node.Rect.Y + o.Pos.Y;
					var r = 5;

					var rect = RectangleF.FromLTRB(posx - r, posy - r, posx + r, posy + r);
					if (rect.Contains(worldCursor))
					{
						Console.WriteLine("wire");

						dragType = DragType.Wire;
						draggedOutput = o;

						return;
					}
				}

				if (node.Rect.Contains(worldCursor))
				{
					if (e.Button == MouseButtons.Left)
					{
						dragType = DragType.Node;
						draggedNode = node;
						return;
					}
					else if (e.Button == MouseButtons.Right)
					{
						deleteList.Add(node);
					}
				}
			}

			foreach (var node in deleteList)
			{
				circuit.DeleteNode(node);
			}

			dragType = DragType.Camera;
		}
	}

	enum DragType
	{
		None,
		Camera,
		Node,
		Wire
	}
}
