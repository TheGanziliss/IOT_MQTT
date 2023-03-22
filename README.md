1.Objective
The aim of this repository is to retrieve real-time currency values from the Investing.com website and create an application that generates alerts based on this data.



2. Why .NET/.NET Core for this Unofficial investing alarm
At this point in time, The .NET Core are widely adoped by a large community of enterprise developers. It paves way for use programming language like C# for making currency value alarm application. The enterprise developers can integrate this functionality into Console Application, Windows Application and Xamarin mobile applications etc.

3. Investing.com Website Currency Alarm (Unofficial)
It is typically not permitted to send web requests to retrieve the HTML of a website due to the presence of CloudFlare. When attempting to access investing.com, CloudFlare may verify that a valid web browser is being used. To address this issue, the implementation of the WebView2 Nuget package has been utilized as a solution.

3a. Development Environment
Windows 10
Visual Studio 2022 with latest updates
3b. Runtime Requirements
.NET Runtime (.NET 6)
Windows OS
Suitable investing.com website link
WebView2 package
First Run
Note: Your investing.com link page should be in english due to localization problems.(Some regions uses , instead of . in numeric system)

Using the application is straightforward. On the first run, the combo box will be empty, meaning there are no linked instruments (currencies). To add the first instrument, click the "Add" button. A new form will appear where you can add a new instrument by entering a title and link.


Example

Instrument's Label : "BTC-USD"
Instrument's Investing Link : "https://www.investing.com/crypto/bitcoin/btc-usd" .
Running The APP
The application allows you to select an instrument from a drop-down menu (also known as a combo box) that you have previously added. You also have the option to specify a range for the selected instrument by defining a minimum and/or maximum limit. The minimum limit can be entered in the text box located in the bottom left corner, while the maximum limit can be entered in the text box located in the bottom right corner. If the selected instrument exceeds the defined limits, the application will provide an audible warning.



As demonstrated below, the application is designed to retrieve real-time data from a specified investment website link at regular intervals in order to provide the most up-to-date information for analysis and decision-making purposes.


It is possible to minimize the application by clicking the "Minimize" button after starting the alarm, as demonstrated below. Once this action is taken, the application will be placed in the toolbox and will continue to function in the background.


The complete .NET project source for the publisher is available in this repository.
