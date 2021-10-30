using LogicSimulator.Simulation;
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
		}

		Circuit circuit;

		Font font = new Font(FontFamily.GenericMonospace, 10);

		PointF camPos = new PointF(0, 0);
		float scale = 1;

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

			circuit.OnPaint(g);
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

		private void CircuitViewControl_MouseMove(object sender, MouseEventArgs e)
		{
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
					break;
			}

			lastMousePos = e.Location;
		}

		private void CircuitViewControl_MouseUp(object sender, MouseEventArgs e)
		{
			dragType = DragType.None;
			draggedNode = null;
		}

		private void CircuitViewControl_MouseDown(object sender, MouseEventArgs e)
		{
			dragType = DragType.None;
			draggedNode = null;

			PointF worldCursor = ScreenToWorld(e.Location);

			foreach (var node in circuit.Nodes)
			{
				var en = Enumerable.Concat(node.Inputs, node.Outputs);
				foreach (var wc in en)
				{
					var posx = node.Rect.X + wc.Pos.X;
					var posy = node.Rect.Y + wc.Pos.Y;
					var r = WireConnector.Radius;

					var rect = RectangleF.FromLTRB(posx - r, posy - r, posx + r, posy + r);
					if (rect.Contains(worldCursor))
					{
						dragType = DragType.Wire;
						Console.WriteLine("wire");
						return;
					}
				}

				if (node.Rect.Contains(worldCursor))
				{
					dragType = DragType.Node;
					draggedNode = node;
					return;
				}
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
