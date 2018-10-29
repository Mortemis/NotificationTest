using System;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Threading;

namespace NotificationTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitSocket();
        }

        Socket socket;

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
            ShowNotification("test");
        }

        private void NotifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            ShowForm();
        }

        #endregion

        #region Form events

        private void NotificationBtn_Click(object sender, EventArgs e)
        {
            ShowNotification("test");
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
            } else
            {
                socket.Close();
            }
        }

        #endregion

        #endregion

        #region Private form methods

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
        private void ShowNotification(string notification)
        {
            notifyIcon1.ShowBalloonTip(4000, "Warning!", notification, ToolTipIcon.Warning);
        }

        #endregion

        #region Socket methods
        
        private void InitSocket() {
            try
            {
                IPEndPoint serverAddress = new IPEndPoint(IPAddress.Parse("10.10.10.155"), 1024);

                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.Connect(serverAddress);

                Thread socketThread = new Thread(SocketThread);
                socketThread.Start();
            } catch (SocketException e)
            {
                MessageBox.Show("Can not connect to server. . .");
            }
        }

        void SocketThread()
        {
            while (socket.Connected)
            {
                try
                {
                    byte[] rcvLenBytes = new byte[4];
                    socket.Receive(rcvLenBytes);
                    int rcvLen = System.BitConverter.ToInt32(rcvLenBytes, 0);
                    byte[] rcvBytes = new byte[rcvLen];
                    socket.Receive(rcvBytes);
                    String rcv = System.Text.Encoding.ASCII.GetString(rcvBytes);

                    ShowNotification(rcv);
                } catch (SocketException e)
                {
                    
                }
            }
        }
        #endregion
    }
}