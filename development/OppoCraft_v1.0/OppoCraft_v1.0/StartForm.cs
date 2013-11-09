using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using testClient;

namespace OppoCraft
{
    public partial class StartForm : Form
    {
        int UID = 0;
        public StartForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Program.network = new NetworkModule(IPAddr.Text,8898);
            Program.network.onMessage+=this.readMessage;
            Program.network.Send(new OppoMessage(OppoMessageType.GetClientID));
            ConnectionStatus.Text = "Connected";
        }

        private void readMessage(NetworkModule client)
        { 
            OppoMessage msg;
            if((msg=client.getMessage())!=null)
            {
                if (msg.Type == OppoMessageType.GetClientID)
                {
                    this.UID=msg["clientid"];
                }
            }
        }

    }
}
