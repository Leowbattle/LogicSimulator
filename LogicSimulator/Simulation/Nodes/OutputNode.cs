using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicSimulator.Simulation.Nodes
{
	public abstract class OutputNode : Node
	{
		public OutputNode(RectangleF rect) : base(rect)
		{
		}
	}
}
