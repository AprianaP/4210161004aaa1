using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace UDP_Client
{
	//tulis di read.me : data yg encapsulasinya, selesai/belum kenapa,    
	class Program
	{
		private static byte[] receive_buffer;

		static void Main(string[] args)
		{
			Boolean done = false;
			Boolean exception_thrown = false;

			Socket sending_socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

			Socket receiving_socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

			IPAddress send_to_address = IPAddress.Parse("127.0.0.1");

			IPEndPoint sending_end_point = new IPEndPoint(send_to_address, 11000);

			IPEndPoint reveiving_end_point = new IPEndPoint(send_to_address, 11000);

			Console.WriteLine("Enter text to broadcast via UDP.");
			Console.WriteLine("Enter a blank line to exit the program.");
			while (!done)
			{
				Console.WriteLine("Enter text to send, blank line to quit");
				string text_to_send = Console.ReadLine();
				if (text_to_send.Length == 0)
				{
					done = true;
				}
				else

				{
					// the socket object must have an array of bytes to send.

					// this loads the string entered by the user into an array of bytes.

					byte[] send_buffer = Encoding.ASCII.GetBytes(text_to_send);


					// Remind the user of where this is going.

					Console.WriteLine("sending to address: {0} port: {1}",
					sending_end_point.Address,
					sending_end_point.Port);
					try

					{
						sending_socket.SendTo(send_buffer, sending_end_point);
						byte[] receivedData;

						sending_socket.Receive(receive_buffer);
						String RE = Encoding.ASCII.GetString(receive_buffer);
						Console.WriteLine(RE);

					}
					catch (Exception send_exception)
					{
						exception_thrown = true;
						Console.WriteLine(" Exception {0}", send_exception.Message);
					}
					if (exception_thrown == false)
					{
						Console.WriteLine("Message has been sent to the broadcast address");
					}
					else

					{
						exception_thrown = false;
						Console.WriteLine("The exception indicates the message was not sent.");
					}
				}
			} // end of while (!done)
		}
	}
}
