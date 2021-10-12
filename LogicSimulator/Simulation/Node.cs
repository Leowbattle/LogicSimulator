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
				DrawWireConnector(g, output);
			}
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
