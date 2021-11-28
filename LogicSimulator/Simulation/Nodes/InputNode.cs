using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicSimulator.Simulation.Nodes
{
	public abstract class InputNode : Node
	{
		protected InputNode(RectangleF rect) : base(rect)
		{
		}

		public abstract void Interact(PointF point);
	}
}
