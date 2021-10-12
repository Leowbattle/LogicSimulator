using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicSimulator.Simulation
{
	public class Circuit
	{
		public List<Node> Nodes = new List<Node>();

		public void Update()
		{

		}

		public void OnPaint(Graphics g)
		{
			foreach (var node in Nodes)
			{
				node.OnPaint(g);
			}
		}
	}
}
