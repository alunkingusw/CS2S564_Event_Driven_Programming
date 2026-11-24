using System.Text;
using System.Net;
using System.Net.Sockets;

const int port = 1000;


//set up and connect in one go. Note that we use a string here
TcpClient client = new TcpClient("127.0.0.1", port);

//alernatively, we could do this is two steps
//TcpClient client = new TcpClient();
//client.Connect("127.0.0.1", port);

NetworkStream stream = client.GetStream();
string message = "Hello from client";

byte[] data = Encoding.ASCII.GetBytes(message);
stream.Write(data, 0, data.Length);

//we will accept up to 256 bytes.
byte[] buffer = new byte[256];

//read is a blocking call - the program waits for the client to send data.
int num_bytes = stream.Read(buffer, 0, 256);

string response = Encoding.ASCII.GetString(buffer, 0, num_bytes);

Console.WriteLine("Received message: " + response);