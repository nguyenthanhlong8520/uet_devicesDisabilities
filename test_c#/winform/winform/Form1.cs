using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace winform
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
            ThreadStart ts = new ThreadStart(Subscribe);
            Thread sub = new Thread(ts);
            sub.Start();

        }

        public void publicMqtt()
        {
            
            MqttClient localClient = new MqttClient("192.168.137.208");
            string clientId = Guid.NewGuid().ToString();
            string user_name = "raspi4";
            string pass_word = "long8520";
            localClient.Connect(clientId, user_name, pass_word);
            string allData = "Hello world 123";
            // subscribe to the topic "dev/test" with QoS 2
            string json = "{\"id\": \"1037600\", \"data\":{\"out1\":100,\"out2\":0,\"out3\":0}}";
            localClient.Publish("dev/test", Encoding.UTF8.GetBytes(json), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
        }

         void Subscribe()
        {
            MqttClient localClient = new MqttClient("192.168.137.208");
            string clientId = Guid.NewGuid().ToString();
            string user_name = "raspi4";
            string pass_word = "long8520";
            localClient.Connect(clientId, user_name, pass_word);

            // register to message received 
            localClient.MqttMsgPublishReceived += client_MqttMsgPublishReceived;


            // subscribe to the topic "" with QoS 2 
            localClient.Subscribe(new string[] { "dev/test" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
        }



        void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
          
            string a = Encoding.UTF8.GetString(e.Message);
            var details = JObject.Parse(a);
            label1.Text = "Temprature: : " + details["temprature"] + "°C";
            label2.Text = "Humidity: " + details["humidity"] + "%";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            publicMqtt();
            MessageBox.Show("Sent");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
