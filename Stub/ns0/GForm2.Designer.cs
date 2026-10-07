namespace ns0
{
	// Token: 0x02000059 RID: 89
	[global::Microsoft.VisualBasic.CompilerServices.DesignerGenerated]
	public partial class GForm2 : global::System.Windows.Forms.Form
	{
		// Token: 0x060003FB RID: 1019 RVA: 0x00019304 File Offset: 0x00017504
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					try { Microsoft.Win32.SystemEvents.SessionEnding -= this.GForm2_SessionEnding; } catch { }
					try { Microsoft.Win32.SystemEvents.PowerModeChanged -= this.GForm2_PowerModeChanged; } catch { }
				}
				bool flag = disposing && this.container_0 != null;
				if (flag)
				{
					((global::System.IDisposable)this.container_0).Dispose();
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x040000BF RID: 191
		private global::System.ComponentModel.Container container_0;
	}
}
