using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace ns0
{
	public partial class GForm0 : Form
	{
		internal virtual Label g1
		{
			get { return this.label_0; }
			set { this.label_0 = value; }
		}
		public GForm0()
		{
			base.FormClosing += this.GForm0_FormClosing;
			this.method_0();
		}
		private void method_0()
		{
			this.g1 = new Label();
			base.SuspendLayout();
			this.g1.Dock = DockStyle.Fill;
			this.g1.Font = new Font("Lucida Console", 48f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.g1.ForeColor = Color.Red;
			this.g1.Location = new Point(0, 0);
			this.g1.Name = "g1";
			this.g1.Size = new Size(874, 408);
			this.g1.TabIndex = 1;
			this.g1.Text = ":3";
			this.g1.TextAlign = ContentAlignment.MiddleCenter;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			this.BackColor = Color.Black;
			base.ClientSize = new Size(874, 408);
			base.Controls.Add(this.g1);
			base.FormBorderStyle = FormBorderStyle.None;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "empty";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.StartPosition = FormStartPosition.CenterScreen;
			base.TopMost = true;
			base.WindowState = FormWindowState.Maximized;
			base.ResumeLayout(false);
		}
		private void GForm0_FormClosing(object sender, FormClosingEventArgs e)
		{
			e.Cancel = true;
		}
		[CompilerGenerated]
		[AccessedThroughProperty("g1")]
		private Label label_0;
	}
}
