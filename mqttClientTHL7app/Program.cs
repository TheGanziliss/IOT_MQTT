using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using Newtonsoft.Json;
using mqttClientTHL7app.Models;
using Microsoft.EntityFrameworkCore;
namespace mqttClientTHL7app
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create Mqtt client
            string IP = "YOUR_IP_ADDRESS";
            MqttClient client = new MqttClient(IP);

            //Connecting Server
            client.Connect("Successful");
            Console.WriteLine("Connection is successful.");

            //Thread creating
            Thread hl7received = new Thread(() => hl7Received(client));
            hl7received.Start();
            Thread nstreceived = new Thread(() => nstReceived(client));
            nstreceived.Start();
            Thread suatreceived = new Thread(() => suatReceived(client));
            suatreceived.Start();
        }

        //Subscribe topic and calling thread
        static void hl7Received(MqttClient client)
        {
            client.Subscribe(new string[] { "HL7" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
            client.MqttMsgPublishReceived += Client_MqttMsgPublishReceived;
        }
        static void nstReceived(MqttClient client)
        {
            client.Subscribe(new string[] { "NST" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
            client.MqttMsgPublishReceived += Client_MqttMsgPublishReceived1;
        }
        static void suatReceived(MqttClient client)
        {
            client.Subscribe(new string[] { "SUAT" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
            client.MqttMsgPublishReceived += Client_MqttMsgPublishReceived2;
        }

        //Parsing and saving database
        private static void Client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            if (e.Topic == "HL7")
            {
                string hl7Packet = Encoding.UTF8.GetString(e.Message);
                string[] fields = hl7Packet.Split('|');
                for (int i = 0; i < fields.Length; i++)
                {
                    string[] parts = fields[i].Split(":");
                    if (parts.Length == 2)
                    {
                        string code = parts[0];
                        string value = parts[1];

                        Console.WriteLine("Value:" + value);
                        Thread.Sleep(250);

                        using (var db = new AppdbContext()) // DbContext class name
                        {
                            var hl7 = new Hl7
                            {
                                SinyalId = code,
                                Deger = value,
                            };
                            db.Hl7s.Add(hl7);
                            db.SaveChanges();
                        }

                    }
                }
            }
        }
        private static void Client_MqttMsgPublishReceived1(object sender, MqttMsgPublishEventArgs e)
        {
            if (e.Topic == "NST")
            {
                string nstPacket = Encoding.UTF8.GetString(e.Message);

                dynamic data1 = JsonConvert.DeserializeObject<dynamic>(nstPacket);
                int fhr = data1.FHR;
                int ua = data1.UA;

                Console.WriteLine("FHR: " + fhr);
                Console.WriteLine("UA: " + ua);
                Thread.Sleep(250);

                using (var db = new AppdbContext()) 
                {
                    var nst = new Nst
                    {
                        Fhr = fhr,
                        Ua = ua,
                    };

                    db.Nsts.Add(nst);
                    db.SaveChanges();
                }
            }
        }
        private static void Client_MqttMsgPublishReceived2(object sender, MqttMsgPublishEventArgs e)
        {
            if (e.Topic == "SUAT")
            {

                string suatPacket = Encoding.UTF8.GetString(e.Message);
                Console.WriteLine(suatPacket);
                Thread.Sleep(250);

                using (var db = new AppdbContext())
                {
                    var suat = new Suat
                    {
                        Adi = suatPacket,
                        Takimi = suatPacket,
                        Plaka = suatPacket,
                        Lakap = suatPacket,
                    };
                    db.Suats.Add(suat);
                    db.SaveChanges();
                }

            }
        }

    }
}