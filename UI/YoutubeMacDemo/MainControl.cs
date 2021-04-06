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
using System.IO.Ports;


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
                ThreadStart ts = new ThreadStart(Subscribe_temp);
                Thread sub = new Thread(ts);
                sub.Start();

            }
            catch (Exception a)
            {
                Console.WriteLine(a.Message);
            }
           
        }


        // light turn ON  
        public void publicMqtt_LightTurnOn()
        {

            MqttClient localClient = new MqttClient("192.168.0.109");
            string clientId = Guid.NewGuid().ToString();
            string user_name = "username";
            string pass_word = "123456";
            localClient.Connect(clientId, user_name, pass_word);
            string allData = "Hello world 123";
            // subscribe to the topic "dev/test" with QoS 2
            string json = "{\"id\": \"1037600\", \"data\":{\"out1\":100,\"out2\":100,\"out3\":0}}";
            localClient.Publish("CyberLink/commands/1037600", Encoding.UTF8.GetBytes(json), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
        }

        // light turn OFF
        public void publicMqtt_LightTurnOff()
        {

            MqttClient localClient = new MqttClient("192.168.0.109");
            string clientId = Guid.NewGuid().ToString();
            string user_name = "username";
            string pass_word = "123456";
            localClient.Connect(clientId, user_name, pass_word);
            string allData = "Hello world 123";
            // subscribe to the topic "dev/test" with QoS 2
            string json = "{\"id\": \"1037600\", \"data\":{\"out1\":100,\"out2\":0,\"out3\":0}}";
            localClient.Publish("CyberLink/commands/1037600", Encoding.UTF8.GetBytes(json), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
        }


        // TV turn ON/OFF
        public void publicMqtt_TvTurnOn()
        {

            MqttClient localClient = new MqttClient("192.168.0.109");
            string clientId = Guid.NewGuid().ToString();
            string user_name = "username";
            string pass_word = "123456";
            localClient.Connect(clientId, user_name, pass_word);
            string allData = "Hello world 123";
            // subscribe to the topic "dev/test" with QoS 2
            string json = "{\"infra_id\": \"TV_SS\", \"value\":0}";
            localClient.Publish("Iot/Ir", Encoding.UTF8.GetBytes(json), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
        }

        // TV +volume
        public void publicMqtt_TvIncreaseVolume()
        {

            MqttClient localClient = new MqttClient("192.168.0.109");
            string clientId = Guid.NewGuid().ToString();
            string user_name = "username";
            string pass_word = "123456";
            localClient.Connect(clientId, user_name, pass_word);
            string allData = "Hello world 123";
            // subscribe to the topic "dev/test" with QoS 2
            string json = "{\"infra_id\": \"TV_SS\", \"value\":1}";
            localClient.Publish("Iot/Ir", Encoding.UTF8.GetBytes(json), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
        }

        // -volume
        public void publicMqtt_TvDecreaseVolume()
        {

            MqttClient localClient = new MqttClient("192.168.0.109");
            string clientId = Guid.NewGuid().ToString();
            string user_name = "username";
            string pass_word = "123456";
            localClient.Connect(clientId, user_name, pass_word);
            string allData = "Hello world 123";
            // subscribe to the topic "dev/test" with QoS 2
            string json = "{\"infra_id\": \"TV_SS\", \"value\":2}";
            localClient.Publish("Iot/Ir", Encoding.UTF8.GetBytes(json), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
        }

        // Mute
        public void publicMqtt_Mute()
        {

            MqttClient localClient = new MqttClient("192.168.0.109");
            string clientId = Guid.NewGuid().ToString();
            string user_name = "username";
            string pass_word = "123456";
            localClient.Connect(clientId, user_name, pass_word);
            string allData = "Hello world 123";
            // subscribe to the topic "dev/test" with QoS 2
            string json = "{\"infra_id\": \"TV_SS\", \"value\":3}";
            localClient.Publish("Iot/Ir", Encoding.UTF8.GetBytes(json), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
        }

        // + 
        public void publicMqtt_TvIncrease()
        {

            MqttClient localClient = new MqttClient("192.168.0.109");
            string clientId = Guid.NewGuid().ToString();
            string user_name = "username";
            string pass_word = "123456";
            localClient.Connect(clientId, user_name, pass_word);
            string allData = "Hello world 123";
            // subscribe to the topic "dev/test" with QoS 2
            string json = "{\"infra_id\": \"TV_SS\", \"value\":4}";
            localClient.Publish("Iot/Ir", Encoding.UTF8.GetBytes(json), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
        }

        // -
        public void publicMqtt_TvDecrease()
        {

            MqttClient localClient = new MqttClient("192.168.0.109");
            string clientId = Guid.NewGuid().ToString();
            string user_name = "username";
            string pass_word = "123456";
            localClient.Connect(clientId, user_name, pass_word);
            string allData = "Hello world 123";
            // subscribe to the topic "dev/test" with QoS 2
            string json = "{\"infra_id\": \"TV_SS\", \"value\":5}";
            localClient.Publish("Iot/Ir", Encoding.UTF8.GetBytes(json), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
        }

        void Subscribe_temp()
        {
            MqttClient localClient = new MqttClient("192.168.0.109");
            string clientId = Guid.NewGuid().ToString();
            string user_name = "username";
            string pass_word = "123456";
            localClient.Connect(clientId, user_name, pass_word);
            // register to message received 
            localClient.MqttMsgPublishReceived += client_MqttMsgPublishReceived;
            // subscribe to the topic "" with QoS 2 
            localClient.Subscribe(new string[] { "Iot/Sensor" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
        }

        // show temp and humidity on label 
        void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {

            string a = Encoding.UTF8.GetString(e.Message);
            var details = JObject.Parse(a);
            
            if (details["infra_id"] != null)
            {
              
                string myString = details["infra_id"].ToString();
                if (myString == "AM2301_T")
                {
                    show_temp.Text = details["value"] + "°C";
                }
                else
                {
                    show_h.Text = details["value"] + "%";
                }
            }
        }
    
        // click on light bulb
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                publicMqtt_LightTurnOn();
                
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

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {

                publicMqtt_LightTurnOff(); 

            }
            catch (Exception a)
            {
                Console.WriteLine(a.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {

                publicMqtt_TvTurnOn();

            }
            catch (Exception a)
            {
                Console.WriteLine(a.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {

                publicMqtt_TvDecreaseVolume();

            }
            catch (Exception a)
            {
                Console.WriteLine(a.Message);
            }
        }

        private void label19_Click_1(object sender, EventArgs e)
        {

        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            try
            {

                publicMqtt_TvIncreaseVolume();

            }
            catch (Exception a)
            {
                Console.WriteLine(a.Message);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            publicMqtt_Mute();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {

                publicMqtt_TvIncrease();

            }
            catch (Exception a)
            {
                Console.WriteLine(a.Message);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {

                publicMqtt_TvDecrease();

            }
            catch (Exception a)
            {
                Console.WriteLine(a.Message);
            }
        }

        private void Humidity_label_Click(object sender, EventArgs e)
        {

        }
    }
}
