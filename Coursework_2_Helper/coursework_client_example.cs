/**
 * This is an example file that demonstrates the basics of setting up
 * the client side of the flight simulator. It is not intended as a solution
 * but part of the solution, that demonstrates the network connection, and
 * message serialisation / deserialisation.
 * you should take this code and use it to help you build a GUI that implements
 * a lot of the techniques to solve the coursework.
 */

using System.Text;
using System.Net.Sockets;
using Messaging;
using System.Text.Json;
using System.Text.Json.Serialization;

const int PORT = 1000;
const string IP_ADDRESS = "192.168.174.1";

//string interpolation allows us to print variables in the message - note the $ at the start
Console.WriteLine($"Connecting to {IP_ADDRESS} on port {PORT}");

//set up and connect in one go. Note that we use a string here
TcpClient client = new TcpClient(IP_ADDRESS, PORT);

NetworkStream stream = client.GetStream();
//this is just a test - it doesn't actually benefit the coursework, but
//you should see the message come up in the flight simulator
string message = "Hello from client";

byte[] data = Encoding.ASCII.GetBytes(message);
stream.Write(data, 0, data.Length);

//we will accept up to 256 bytes.
byte[] buffer = new byte[256];

/**
 * Now let's try sending some telemetry data - if we want the simulator
 * to respond to this, we need it to be connected, and to be started.
 * As a result (since this is a command line) we will wait for some
 * data to be sent to us
 */

//read is a blocking call - the program waits for the client to send data.
int num_bytes = stream.Read(buffer, 0, 256);
string response = Encoding.ASCII.GetString(buffer, 0, num_bytes);
Console.WriteLine("Received message: " + response);

// now we know that the simulator is running
// the code wouldn't get here if it wasn't!
// let's construct the telemetry data - use the class from our control file
Messaging.ControlsUpdate controlUpdate = new Messaging.ControlsUpdate(50, 0);

//now we turn it into JSON
string jsonString = JsonSerializer.Serialize(controlUpdate, AppJsonContext.Default.ControlsUpdate);

//then we send the jsonString
data = Encoding.ASCII.GetBytes(jsonString);
stream.Write(data, 0, data.Length);

/**
 * In this class we set up our messaging structure for the JSON
 * these are taken from the coursework file.
 * We want to serialise/deserialise these into JSON to send and
 * recieve messages.
 * It's important that we get this correct as this is the key
 * protocol for the flight simulator to understand us so case 
 * sensitivity matters.
 */

[JsonSourceGenerationOptions(
    PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
[JsonSerializable(typeof(ControlsUpdate))]
 internal partial class AppJsonContext:JsonSerializerContext{}


namespace Messaging{
    public class ControlsUpdate
    {
        //these need to be public properties
        //see slide 26 of Lecture 9 Sockets & Serialisation.pptx 
        public double Throttle {get; set;} = 0;
        public double ElevatorPitch {get; set;} = 0;

        //Constructor for the class
        public ControlsUpdate(double newThrottle, double newElevatorPitch)
        {
            Throttle = newThrottle;
            ElevatorPitch = newElevatorPitch;   
        }
    }

    class TelemetryUpdate
    {
    
    }
}