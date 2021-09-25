using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LogicSimulator
{
	public partial class CircuitViewControl : Control
	{
		public CircuitViewControl()
		{
			InitializeComponent();
		}

		Font f = new Font(FontFamily.GenericMonospace, 10);

		protected override void OnPaint(PaintEventArgs pe)
		{
			base.OnPaint(pe);

			var g = pe.Graphics;
			g.SmoothingMode = SmoothingMode.AntiAlias;
			g.TextRenderingHint = TextRenderingHint.AntiAlias;

			g.ScaleTransform(15, 15);

			g.DrawString("Test", f, Brushes.Black, 0, 0);
		}
	}
}
