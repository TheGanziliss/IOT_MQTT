using System;
using System.Text;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using System.Threading;
using System.IO;

namespace mqttServerTHL7
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // IP address is taken from user
            string IP = "YOUR_IP_ADDRESS";
            string ipAddress = IP;

            // MQTT client creating
            MqttClient client = new MqttClient(ipAddress);

            // Connecting Server
            client.Connect("messagePublisher123");
            Console.WriteLine("Server connection is successful.");

            // 'hl7' Subscribe to the topic
            client.Subscribe(new string[] { "HL7" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
            client.Subscribe(new string[] { "NST" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
            client.Subscribe(new string[] { "SUAT" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
            client.MqttMsgPublishReceived += Client_MqttMsgPublishReceived;

            // HL7 read packages file
            string filePath = "C:\\......\\NST.json";
            string[] hl7Packets = File.ReadAllLines(filePath);

            string filepath1 = "C:\\.......\\HL7MSG.json";
            string[] hl7Packets1 = File.ReadAllLines(filepath1);

            string filepath2 = "C:\\.........\\SUAT.json";
            string[] hl7Packets2 = File.ReadAllLines(filepath2);

            // 'n' threads are created

            Thread thread1 = new Thread(() => SendHL7Packets(client, hl7Packets));
            thread1.Start();

            Thread thread2 = new Thread(() => SendHL7Packets1(client, hl7Packets1));
            thread2.Start();

            Thread thread3 = new Thread(() => SendHL7Packets2(client, hl7Packets2));
            thread3.Start();

            // Publish packets in infinite loop
            static void SendHL7Packets(MqttClient client, string[] hl7Packets)
            {
                while (true)
                {
                    foreach (string hl7Packet in hl7Packets)
                    {
                        client.Publish("NST", Encoding.UTF8.GetBytes(hl7Packet));
                        Console.WriteLine("NST packets published : " + hl7Packet);
                        Thread.Sleep(500);
                    }
                }
            }
            static void SendHL7Packets1(MqttClient client, string[] hl7Packets1)
            {
                while (true)
                {
                    foreach (string hl7Packet1 in hl7Packets1)
                    {
                        client.Publish("HL7", Encoding.UTF8.GetBytes(hl7Packet1));
                        Console.WriteLine("HL7 packets publishedı: " + hl7Packet1);
                        Thread.Sleep(500);
                    }
                }
            }
            static void SendHL7Packets2(MqttClient client, string[] hl7Packets2)
            {
                while (true)
                {
                    foreach (string hl7Packet2 in hl7Packets2)
                    {
                        client.Publish("SUAT", Encoding.UTF8.GetBytes(hl7Packet2));
                        Console.WriteLine("SUAT packets published: " + hl7Packet2);
                        Thread.Sleep(500);
                    }
                }
            }

            // Event to run when MQTT message is received
            static void Client_MqttMsgPublishReceived(object sender, uPLibrary.Networking.M2Mqtt.Messages.MqttMsgPublishEventArgs e)
            {
                // If subject is 'hl7', write package to console
                if (e.Topic == "NST" || e.Topic == "HL7" || e.Topic == "SUAT")
                {
                    Console.WriteLine("Received NST packets: " + Encoding.UTF8.GetString(e.Message));
                    Console.WriteLine("Received HL7 packets: " + Encoding.UTF8.GetString(e.Message));
                    Console.WriteLine("Received SUAT packets: " + Encoding.UTF8.GetString(e.Message));

                }
            }
        }
    }
}