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

			ResizeRedraw = true;
			DoubleBuffered = true;

			MouseDown += CircuitViewControl_MouseDown;
			MouseUp += CircuitViewControl_MouseUp;
			MouseMove += CircuitViewControl_MouseMove;
			MouseWheel += CircuitViewControl_MouseWheel;
		}

		Font font = new Font(FontFamily.GenericMonospace, 10);

		PointF camPos = new PointF(0, 0);
		float scale = 1;

		protected override void OnPaint(PaintEventArgs pe)
		{
			base.OnPaint(pe);

			var g = pe.Graphics;
			g.SmoothingMode = SmoothingMode.AntiAlias;
			g.TextRenderingHint = TextRenderingHint.AntiAlias;

			g.ScaleTransform(scale, scale);
			g.TranslateTransform(-camPos.X, -camPos.Y);

			g.DrawString("Test", font, Brushes.Black, 0, 0);
		}

		private void CircuitViewControl_MouseWheel(object sender, MouseEventArgs e)
		{
			float scaleChange = scale * e.Delta / 2000f;
			float newScale = scale + scaleChange;

			var oldWorldMouseX = e.X / scale + camPos.X;
			var oldWorldMouseY = e.Y / scale + camPos.Y;

			var worldMouseX = e.X / newScale + camPos.X;
			var worldMouseY = e.Y / newScale + camPos.Y;

			camPos.X -= worldMouseX - oldWorldMouseX;
			camPos.Y -= worldMouseY - oldWorldMouseY;

			scale = newScale;

			Invalidate();
		}

		bool mouseDown = false;
		PointF lastMousePos = new PointF(0, 0);

		private void CircuitViewControl_MouseMove(object sender, MouseEventArgs e)
		{
			if (mouseDown)
			{
				camPos.X -= (e.X - lastMousePos.X) / scale;
				camPos.Y -= (e.Y - lastMousePos.Y) / scale;

				Invalidate();
			}

			lastMousePos = e.Location;
		}

		private void CircuitViewControl_MouseUp(object sender, MouseEventArgs e)
		{
			mouseDown = false;
		}

		private void CircuitViewControl_MouseDown(object sender, MouseEventArgs e)
		{
			mouseDown = true;
		}
	}
}
