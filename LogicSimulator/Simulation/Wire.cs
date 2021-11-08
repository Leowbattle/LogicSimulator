using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicSimulator.Simulation
{
	public class Input
	{
		public PointF Pos;

		// Where this input will read a value from
		public Output Source;

		public Input(PointF pos)
		{
			Pos = pos;
		}
	}

	public class Output
	{
		public PointF Pos;

		public bool Value;
		public List<Input> Inputs;

		public Output(PointF pos)
		{
			Pos = pos;
		}
	}
}
