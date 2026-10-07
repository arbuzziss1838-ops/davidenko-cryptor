using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using Microsoft.VisualBasic.ApplicationServices;

namespace ns0
{
	[GeneratedCode("MyTemplate", "11.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	internal class Form0 : WindowsFormsApplicationBase
	{
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[STAThread]
		internal static void Main(string[] args)
		{
			AppDomain.CurrentDomain.UnhandledException += delegate(object sender, System.UnhandledExceptionEventArgs e)
			{
				WriteCrash(e.ExceptionObject as Exception);
			};
			Application.ThreadException += delegate(object sender, System.Threading.ThreadExceptionEventArgs e)
			{
				WriteCrash(e.Exception);
			};
			try
			{
				WriteCrash(null);
			}
			catch
			{
			}
			try
			{
				Application.SetCompatibleTextRenderingDefault(WindowsFormsApplicationBase.UseCompatibleTextRendering);
			}
			finally
			{
			}
			try
			{
				Class1.Form0_0.Run(args);
			}
			catch (Exception ex)
			{
				WriteCrash(ex);
				throw;
			}
		}

		private static void WriteCrash(Exception ex)
		{
			try
			{
				string path = Path.Combine(Path.GetTempPath(), "stub_crash.log");
				if (ex == null)
				{
					File.AppendAllText(path, "[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "] Stub.exe started (Main entered)\r\n");
					return;
				}
				File.AppendAllText(path,
					"[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "] UNHANDLED EXCEPTION\r\n" +
					"Type:    " + ex.GetType().FullName + "\r\n" +
					"Message: " + ex.Message + "\r\n" +
					"Source:  " + ex.Source + "\r\n" +
					"Stack:\r\n" + ex.StackTrace + "\r\n" +
					"Inner:   " + (ex.InnerException != null ? ex.InnerException.GetType().FullName + ": " + ex.InnerException.Message + "\r\n" + ex.InnerException.StackTrace : "none") + "\r\n" +
					"----\r\n");
			}
			catch
			{
			}
		}
		public Form0() : base(AuthenticationMode.Windows)
		{
			base.IsSingleInstance = true;
			base.EnableVisualStyles = false;
			base.SaveMySettingsOnExit = false;
			base.ShutdownStyle = ShutdownMode.AfterMainFormCloses;
		}
		protected override void OnCreateMainForm()
		{
			base.MainForm = Class1.MyForms_0._o_program;
		}
	}
}
