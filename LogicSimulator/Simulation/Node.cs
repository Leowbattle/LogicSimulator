using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicSimulator.Simulation
{
	public class Node
	{
		public RectangleF rect;

		public Node(PointF p)
		{
			rect = new RectangleF(p.X, p.Y, 40, 40);
		}

		public virtual void OnPaint(Graphics g)
		{
			g.FillRectangle(Brushes.White, rect);
		}
	}
}
