using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicSimulator.Simulation
{
	[Serializable]
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

		public void DeleteNode(Node node)
		{
			foreach (var inp in node.Inputs)
			{
				if (inp.Source == null)
				{
					continue;
				}
				inp.Source.Inputs.Remove(inp);
			}
			foreach (var outp in node.Outputs)
			{
				foreach (var inp in outp.Inputs)
				{
					inp.Source = null;
				}
			}
			Nodes.Remove(node);
		}

		public void EvaluateCircuit(Node root)
		{
			var queue = new Queue<Node>();
			queue.Enqueue(root);
			var visited = new HashSet<Node>();
			foreach (var output in root.Outputs)
			{
				foreach (var i in output.Inputs)
				{
					var node = i.Node;
					if (!queue.Contains(node))
					{
						queue.Enqueue(node);
					}
				}
			}
			while (queue.Count != 0)
			{
				var node = queue.Dequeue();
				node.Update();
				visited.Add(node);

				foreach (var output in node.Outputs)
				{
					foreach (var i in output.Inputs)
					{
						var node2 = i.Node;
						if (!visited.Contains(node2))
						{
							queue.Enqueue(node2);
						}
					}
				}
			}

			Console.WriteLine();
		}
	}
}
