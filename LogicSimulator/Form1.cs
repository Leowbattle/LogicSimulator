﻿using LogicSimulator.Simulation;
using LogicSimulator.Simulation.Nodes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
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

		private void andGateButton_Click(object sender, EventArgs e)
		{
			circuitViewControl1.AddNode(new AndGate(circuitViewControl1.ScreenCentre));
		}

		private void orGateButton_Click(object sender, EventArgs e)
		{
			circuitViewControl1.AddNode(new OrGate(circuitViewControl1.ScreenCentre));
		}

		private void xorGateButton_Click(object sender, EventArgs e)
		{
			circuitViewControl1.AddNode(new XorGate(circuitViewControl1.ScreenCentre));
		}

		JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings
		{
			ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
			PreserveReferencesHandling = PreserveReferencesHandling.All,
			Formatting = Formatting.Indented
		};

		private void saveToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var d = new SaveFileDialog();
			if (d.ShowDialog() == DialogResult.OK)
			{
				using (var f = d.OpenFile())
				{
					var b = new BinaryFormatter();
					b.Serialize(f, circuitViewControl1.circuit);
				}
			}
		}

		private void openToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var d = new OpenFileDialog();
			if (d.ShowDialog() == DialogResult.OK)
			{
				using (var f = d.OpenFile())
				{
					var b = new BinaryFormatter();
					var circuit = (Circuit)b.Deserialize(f);
					circuitViewControl1.circuit = circuit;
					circuitViewControl1.Invalidate();
				}
			}
		}
	}
}
