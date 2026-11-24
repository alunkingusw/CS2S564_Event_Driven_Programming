using System.Text;
using System.Net;
using System.Net.Sockets;

//use a port that no one else uses
const int port = 1000;

IPAddress localhost = IPAddress.Parse("127.0.0.1");
//could also use the constant IPAddress.Loopback

var listener = new TcpListener(localhost, port);

listener.Start();

Console.WriteLine("Server is waiting for a client...");

TcpClient client = listener.AcceptTcpClient();
//the program will block here until we get a connection.
//we could use delegates here to call a function when a connection is made.

IPEndPoint ipEndPoint = (IPEndPoint)client.Client.RemoteEndPoint;
Console.WriteLine("Client connected from "+ ipEndPoint.Address + ":" + ipEndPoint.Port);


NetworkStream stream = client.GetStream();

//we will accept up to 256 bytes.
byte[] buffer = new byte[256];

//read is a blocking call - the program waits for the client to send data.
int num_bytes = stream.Read(buffer, 0, 256);

string message = Encoding.ASCII.GetString(buffer, 0, num_bytes);

Console.WriteLine("Received message: " + message);

string response = "Hello from server";

byte[] data = Encoding.ASCII.GetBytes(response);
stream.Write(data, 0, data.Length);

listener.Stop();