using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicSimulator.Simulation
{
	[Serializable]
	public abstract class Node
	{
		public RectangleF Rect;

		public Input[] Inputs = Array.Empty<Input>();
		public Output[] Outputs = Array.Empty<Output>();

		public Node(RectangleF rect)
		{
			Rect = rect;
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
				DrawInput(g, input);
			}

			foreach (var output in Outputs)
			{
				DrawOutput(g, output);
			}
		}

		private void DrawInput(Graphics g, Input input)
		{
			float radius = 5;

			float x = Rect.X + input.Pos.X - radius;
			float y = Rect.Y + input.Pos.Y - radius;

			g.FillEllipse(
				Brushes.LightGray,
				x,
				y,
				radius * 2,
				radius * 2);
		}

		private void DrawOutput(Graphics g, Output output)
		{
			float radius = 5;

			float x = Rect.X + output.Pos.X - radius;
			float y = Rect.Y + output.Pos.Y - radius;

			foreach (var i in output.Inputs)
			{
				float x2 = i.Node.Rect.X + i.Pos.X;
				float y2 = i.Node.Rect.Y + i.Pos.Y;

				g.DrawLine(Pens.Black, x + radius, y + radius, x2, y2);
			}

			g.FillEllipse(
				Brushes.Gray,
				x,
				y,
				radius * 2,
				radius * 2);
		}
	}
}
