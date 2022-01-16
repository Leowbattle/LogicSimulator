using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicSimulator.Simulation
{
	[Serializable]
	public class ExampleNode : Node
	{
		public ExampleNode(PointF pos) : base(
			new RectangleF(pos.X, pos.Y, 40, 40))
		{
			Inputs = new Input[]
			{
				new Input(this, new PointF(0, 5)),
				new Input(this, new PointF(0, 15)),
				new Input(this, new PointF(0, 25)),
				new Input(this, new PointF(0, 35)),
			};
			Outputs = new Output[]
			{
				new Output(this, new PointF(40, 10)),
				new Output(this, new PointF(40, 30)),
			};
		}

		public override void Update()
		{

		}

		public override void OnPaint(Graphics g)
		{
			g.FillRectangle(Brushes.White, Rect);

			base.OnPaint(g);
		}
	}
}
