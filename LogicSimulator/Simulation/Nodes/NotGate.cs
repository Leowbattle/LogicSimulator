using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicSimulator.Simulation.Nodes
{
	[Serializable]
	public class NotGate : LogicNode
	{
		static int nodeSize = 40;

		public NotGate(PointF pos) : base(new RectangleF(pos.X, pos.Y, nodeSize, nodeSize))
		{
			Inputs = new Input[]
			{
				new Input(this, new PointF(0, 20))
			};
			Outputs = new Output[]
			{
				new Output(this, new PointF(40, 20))
			};
		}

		public override void Update()
		{
			if (Inputs[0].Source == null)
			{
				Outputs[0].Value = true;
				return;
			}

			Outputs[0].Value = !Inputs[0].Source.Value;
		}

		static Bitmap img = Properties.Resources.not_gate;

		public override void OnPaint(Graphics g)
		{
			g.DrawImage(img, Rect.X, Rect.Y, nodeSize, nodeSize);

			base.OnPaint(g);
		}
	}
}
