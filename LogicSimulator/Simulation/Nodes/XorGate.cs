using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicSimulator.Simulation.Nodes
{
	public class XorGate : LogicNode
	{
		static int nodeSize = 40;

		public XorGate(PointF pos) : base(new RectangleF(pos.X, pos.Y, nodeSize, nodeSize))
		{
			Inputs = new Input[]
			{
				new Input(this, new PointF(0, 10)),
				new Input(this, new PointF(0, 30)),
			};
			Outputs = new Output[]
			{
				new Output(this, new PointF(40, 20))
			};
		}

		public override void Update()
		{
			if (Inputs[0].Source == null || Inputs[1].Source == null)
			{
				Outputs[0].Value = false;
				return;
			}

			Outputs[0].Value = Inputs[0].Source.Value ^ Inputs[1].Source.Value;
		}

		static Bitmap img = Properties.Resources.xor_gate;

		public override void OnPaint(Graphics g)
		{
			g.DrawImage(img, Rect.X, Rect.Y, nodeSize, nodeSize);

			base.OnPaint(g);
		}
	}
}
