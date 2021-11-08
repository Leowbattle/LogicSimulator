using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicSimulator.Simulation
{
	public class ExampleNode : Node
	{
		public ExampleNode(PointF pos) : base(
			new RectangleF(pos.X, pos.Y, 40, 40),
			new Input[]
			{
				new Input(new PointF(0, 20))
			},
			new Output[]
			{
				new Output(new PointF(40, 10)),
				new Output(new PointF(40, 30)),
			})
		{

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
