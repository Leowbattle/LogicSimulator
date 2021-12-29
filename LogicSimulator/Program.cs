using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LogicSimulator
{
	static class Program
	{
		public static bool DebugDraw = false;

		static void UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			var UnhandledEx = (Exception)e.ExceptionObject;
			MessageBox.Show($"{UnhandledEx.Message}\n{UnhandledEx.StackTrace}");
		}

		static void UIThreadException(object sender, ThreadExceptionEventArgs t)
		{
			MessageBox.Show($"{t.Exception.Message}\n{t.Exception.StackTrace}");
		}

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.ThreadException += new ThreadExceptionEventHandler(UIThreadException);
			AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(UnhandledException);

			Application.ThreadException += new ThreadExceptionEventHandler(UIThreadException);
			Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
			AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(UnhandledException);

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Form1());
		}
	}
}
