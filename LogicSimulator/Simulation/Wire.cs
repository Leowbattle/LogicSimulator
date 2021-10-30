using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicSimulator.Simulation
{
	public class Wire
	{
		public Node From;
		public Node To;

		public WireConnector FromC;
		public WireConnector ToC;

		public bool Value;
	}

	public class WireConnector
	{
		public Wire Wire;
		public PointF Pos;
		public WireConnectorType Type;

		public WireConnector(PointF pos, WireConnectorType type)
		{
			Pos = pos;
			Type = type;
		}

		public const float Radius = 5;
		public static Brush InputColour = Brushes.LightGray;
		public static Brush OutputColour = Brushes.Gray;
	}

	public enum WireConnectorType
	{
		Input,
		Output
	}
}
