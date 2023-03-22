#  1.Objective

The aim of this repository is to create an application that takes the data published by multiple machines and saves it to the database.

# 2. Why .NET/.NET Core

.NET Core is a cross-platform framework that can be run for Windows, macOS, and Linux. Moreover, it is open-source and accepts contributions from the developer’s community.

# 3.What is MQTT?

MQTT (Message Queuing Telemetry Transport) is a messaging protocol for restricted low-bandwidth networks and extremely high-latency IoT devices. Since Message Queuing Telemetry Transport is specialized for low-bandwidth, high-latency environments, it is an ideal protocol for machine-to-machine (M2M) communication.

MQTT works on the publisher / subscriber principle and is operated via a central broker. This means that the sender and receiver have no direct connection. The data sources report their data via a publish and all recipients with interest in certain messages (“marked by the topic”) get the data delivered because they have registered as subscribers.

## 4a. Development Environment
- Windows 10
- Visual Studio 2022 with latest updates

## 4b. Runtime Requirements
- .NET Runtime [(.NET 6)](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
- mosquitto-2.0.15(https://mosquitto.org/download/)
- MS SQL Server

# Running The APP

- As demonstrated below, the application is it takes the data under the specified topics and saves it to the database.

<p>
    <img src="/Images/mqttServer.jpg" width="40%" height="40%">
</p>

<p>
    <img src="/Images/mqttClient.jpg" width="30%" height="30%">
</p>
