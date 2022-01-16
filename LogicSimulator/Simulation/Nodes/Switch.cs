using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicSimulator.Simulation.Nodes
{
	[Serializable]
	public class Switch : InputNode
	{
		static int nodeSize = 40;

		public Switch(PointF pos) : base(new RectangleF(pos.X, pos.Y, nodeSize, nodeSize))
		{
			Outputs = new Output[]
			{
				new Output(this, new PointF(40, 20))
			};
		}

		bool isOn;

		public override void Interact(PointF point, Circuit circuit)
		{
			isOn = !isOn;
			circuit.EvaluateCircuit(this);
		}

		public override void Update()
		{
			Outputs[0].Value = isOn;
		}

		static Bitmap offImg = Properties.Resources.toggle_off;
		static Bitmap onImg = Properties.Resources.toggle_on;

		public override void OnPaint(Graphics g)
		{
			Bitmap img = offImg;
			if (isOn)
			{
				img = onImg;
			}
			g.DrawImage(img, Rect.X, Rect.Y, nodeSize, nodeSize);

			base.OnPaint(g);
		}
	}
}
