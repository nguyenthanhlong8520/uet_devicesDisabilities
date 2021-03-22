using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace YoutubeMacDemo
{
    public partial class MainControl : UserControl
    {
        public MainControl()
        {
            InitializeComponent();

            try
            {
                Control.CheckForIllegalCrossThreadCalls = false;
                ThreadStart ts = new ThreadStart(Subscribe);
                Thread sub = new Thread(ts);
                sub.Start();
            }
            catch (Exception a)
            {
                Console.WriteLine(a.Message);
            }
           
        }

        public void publicMqtt()
        {

            MqttClient localClient = new MqttClient("192.168.2.190");
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
            MqttClient localClient = new MqttClient("192.168.2.190");
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
            if (details["temprature"] != null)
            {
                //label1.Text = "Temprature: : " + details["temprature"] + "°C";
                //label2.Text = "Humidity: " + details["humidity"] + "%";
                int humidity = details["humidity"].ToObject<int>();
                int temprature = details["temprature"].ToObject<int>();
                this.temprature.Value = temprature;
                this.humidity.Value = humidity;
            }
        }

        // click on light bulb
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                publicMqtt();
            }
            catch (Exception a)
            {
                Console.WriteLine(a.Message);
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {

        }

        private void guna2CircleProgressBar2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void guna2CircleProgressBar1_ValueChanged(object sender, EventArgs e)
        {
           
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
