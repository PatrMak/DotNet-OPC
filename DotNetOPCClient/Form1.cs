using Opc.UaFx.Client;
using System.Drawing;
using System.Windows.Forms;

namespace DotNetOPCClient
{
    public partial class Form1 : Form
    {
        OpcData data;

        Timer timer;
        Timer timerReset;

        const string opcIP = "opc.tcp://localhost:4840/";
        const string folderNode = "ns=2;s=Machine/";

        const string temperature = "Temperature";
        const string alarm = "Alarm";
        const string reset = "Reset";

        public Form1()
        {
            InitializeComponent();

            

            try
            {
                data = new OpcData(opcIP, folderNode);
                controlStatus.FillColor = Color.Green;
                timer = new Timer();
                timer.Interval = 500;
                timer.Tick += Timer_Tick;
                timer.Start();
            }
            catch
            {
                controlStatus.FillColor = Color.Red;
            }
        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (timer != null)
                timer.Stop();

            data.Disconnet();
        }


        private void btReset_Click(object sender, System.EventArgs e)
        {
            data.Reset(true, reset);
            timerReset = new Timer();
            timerReset.Interval = 1000;
            timerReset.Tick += TimerReset_Tick;
            timerReset.Start();
        }


        private void Timer_Tick(object sender, System.EventArgs e)
        {
            var temp = data.Temperature(temperature);
            txtTemperature.Text = temp.ToString();

            var status = data.Alarm(alarm);

            controlLvl.FillColor = status ? Color.Red : Color.Green;
        }



        private void TimerReset_Tick(object sender, System.EventArgs e)
        {
            if (timerReset != null)
                timerReset.Stop();

            data.Reset(false, reset);
            timerReset = null;
        }



    }
}
