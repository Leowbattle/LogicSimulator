using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicSimulator.Simulation.Nodes
{
	[Serializable]
	public class LightNode : OutputNode
	{
		static int nodeSize = 40;

		public LightNode(PointF pos) : base(new RectangleF(pos.X, pos.Y, nodeSize, nodeSize))
		{
			Inputs = new Input[]
			{
				new Input(this, new PointF(20, 40))
			};
		}

		bool isOn = false;

		public override void Update()
		{
			isOn = Inputs[0].Source.Value;
		}

		static Bitmap offImg = Properties.Resources.light_off;
		static Bitmap onImg = Properties.Resources.light_on;

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
