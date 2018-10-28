using System;
using System.Windows.Forms;

namespace NotificationTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        #region Events handlers

        #region Notify icon events

        private void ShowFormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm();
        }

        private void HideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HideForm();
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ShowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowNotification();
        }

        private void NotifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            ShowForm();
        }

        #endregion

        #region Form events

        private void NotificationBtn_Click(object sender, EventArgs e)
        {
            ShowNotification();
        }


        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (e.CloseReason == CloseReason.WindowsShutDown) return;

            // Confirm user wants to close
            if (MessageBox.Show(this, "Совсем-совсем выйти?", "Closing", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                HideForm();
                e.Cancel = true;
            }
        }

        #endregion

        #endregion

        #region Private methods

        // прячем в трей
        private void HideForm()
        {
            WindowState = FormWindowState.Minimized;
            ShowInTaskbar = false;
        }

        // !прячем в трей
        private void ShowForm()
        {
            WindowState = FormWindowState.Normal;
            ShowInTaskbar = false;
        }

        // уведомление
        private void ShowNotification()
        {
            notifyIcon1.ShowBalloonTip(4000, "Алярма!", "Алярма алярма, алярма. АЛЯРМА!", ToolTipIcon.Warning);
        }

        #endregion
    }
}