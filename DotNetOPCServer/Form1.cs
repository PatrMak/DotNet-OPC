using Opc.UaFx.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DotNetOPCServer
{
    public partial class Form1 : Form
    {
        OpcServer server;
        public Form1()
        {
            InitializeComponent();


            try
            {
                server = new OpcServer("opc.tcp://localhost:4840/", new NodeManager());
                server.Start();
                controlStatus.FillColor = Color.Green;
            }
            catch
            {
                controlStatus.FillColor = Color.Red;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (server != null)
                server.Stop();
        }
    }
}
