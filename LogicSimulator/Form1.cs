using LogicSimulator.Simulation.Nodes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LogicSimulator
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void switchButton_Click(object sender, EventArgs e)
		{
			circuitViewControl1.AddNode(new Switch(circuitViewControl1.ScreenCentre));
		}

		private void lightButton_Click(object sender, EventArgs e)
		{
			circuitViewControl1.AddNode(new LightNode(circuitViewControl1.ScreenCentre));
		}

		private void notGateButton_Click(object sender, EventArgs e)
		{
			circuitViewControl1.AddNode(new NotGate(circuitViewControl1.ScreenCentre));
		}
	}
}
