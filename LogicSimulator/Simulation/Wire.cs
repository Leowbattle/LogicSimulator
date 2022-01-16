using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicSimulator.Simulation
{
	[Serializable]
	public class Input
	{
		public Node Node;
		public PointF Pos;

		// Where this input will read a value from
		public Output Source;

		public Input(Node node, PointF pos)
		{
			Node = node;
			Pos = pos;
		}
	}

	[Serializable]
	public class Output
	{
		public Node Node;
		public PointF Pos;

		public bool Value;
		public List<Input> Inputs = new List<Input>();

		public Output(Node node, PointF pos)
		{
			Node = node;
			Pos = pos;
		}
	}
}
