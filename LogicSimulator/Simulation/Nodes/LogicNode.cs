using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicSimulator.Simulation.Nodes
{
	[Serializable]
	public abstract class LogicNode : Node
	{
		protected LogicNode(RectangleF rect) : base(rect)
		{
		}
	}
}
