# publish-subscribe-demo
This repo contains code that demonstrates publish/subscribe pattern using .net c# and [NServiceBus](https://docs.particular.net/nservicebus/) library.

# Prerequisite
Ensure the following are installed to run the projects:
* At least .NET Framework 4.7.2 SDK
* C# version 7.3
* Visual Studio 2019 (Recommended)

# Running The Application
1. Once you download/clone the code, open the solution in Visual Studio.
1. Build the solution. This will automatically restore the nuget packages for you.
1. Set the startup project of the solution. Choose the **Multiple startup projects** option and select **Publisher** and **Subscriber** projects as "Start" action. Click apply.
1. Run the project and there will be 2 console windows, i.e. one for publisher and one for subscriber.

# About the Application
This application uses NServiceBus library to simulate a publish/subscribe architecture. It is using the [**LearningTransport**](https://docs.particular.net/transports/learning/) as transport which does not require any message-broker setup.
#### Publisher
You can enter any value in the console as the message to publish.
Whitespace or empty strings will be ignored. Enter "exit" text to exit the publisher program.<br/>The publisher will hash the input (SHA256) and construct a message of type **Message** before publishing it to all the subscribers.
#### Subscriber
This is a console application that contains 2 handlers for the message published by the publisher. The two handlers are:
1. MessageJsonDisplayHandler - prints the message in JSON string format into the console window
1. SHA256DisplayHandler - prints only the SHA256 value of the original input value into the console window
