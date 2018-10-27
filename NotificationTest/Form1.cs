using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace NotificationTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (e.CloseReason == CloseReason.WindowsShutDown) return;

            // Confirm user wants to close
            switch (MessageBox.Show(this, "Совсем-совсем выйти?", "Closing", MessageBoxButtons.YesNo))
            {
                case DialogResult.No:
                    e.Cancel = true;
                    this.Hide();
                    break;
                default:
                    break;
            }
        }

        /* todo: сворачивание в трей.
         * 
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            if (this.WindowState.Equals(FormWindowState.Minimized))
            {
                this.Hide();
            }
        }
        */

        private void showNotification()
        {
            notifyIcon1.ShowBalloonTip(4000, "Алярма!", "Алярма алярма, алярма. АЛЯРМА!", ToolTipIcon.Error);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            showNotification();
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            this.Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e) 
        {
            showNotification();
        }
    }
}


/*
 * MessageBox.Show("Program loaded.");
 * 
 * 
 */
