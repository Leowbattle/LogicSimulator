using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicSimulator.Simulation
{
	public abstract class Node
	{
		public RectangleF Rect;

		public WireConnector[] Inputs;
		public WireConnector[] Outputs;

		public Node(RectangleF rect, WireConnector[] inputs, WireConnector[] outputs)
		{
			Rect = rect;
			Inputs = inputs;
			Outputs = outputs;
		}

		public abstract void Update();

		public virtual void OnPaint(Graphics g)
		{
			if (Program.DebugDraw)
			{
				g.DrawRectangle(Pens.Red, Rect.X, Rect.Y, Rect.Width, Rect.Height);
			}

			foreach (var input in Inputs)
			{
				DrawWireConnector(g, input);
			}

			foreach (var output in Outputs)
			{
				DrawWire(g, output, output.Wire);
				DrawWireConnector(g, output);
			}
		}

		private void DrawWire(Graphics g, WireConnector c, Wire wire)
		{
			if (wire == null)
			{
				return;
			}

			float x1 = Rect.X + c.Pos.X;
			float y1 = Rect.Y + c.Pos.Y;

			float x2 = wire.To.Rect.X + wire.ToC.Pos.X;
			float y2 = wire.To.Rect.Y + wire.ToC.Pos.Y;

			g.DrawLine(Pens.Black, x1, y1, x2, y2);
		}

		private void DrawWireConnector(Graphics g, WireConnector input)
		{
			float x = Rect.X + input.Pos.X - WireConnector.Radius;
			float y = Rect.Y + input.Pos.Y - WireConnector.Radius;

			Brush brush =
				input.Type == WireConnectorType.Input ? WireConnector.InputColour : WireConnector.OutputColour;

			g.FillEllipse(
				brush,
				x,
				y,
				WireConnector.Radius * 2,
				WireConnector.Radius * 2);
		}
	}
}
