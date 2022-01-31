using System;
using System.Text;
using System.Net.Sockets;
using System.Net;
namespace Supernova_Server
{
	public class CustomServerTCP
	{
		Socket SocServer = new Socket(AddressFamily.InterNetwork , SocketType.Stream, ProtocolType.Tcp);
		Socket socClient = null;
		
		
		public void StartServer(int port , string ipadress)
		{
			IPEndPoint ipendpoeintServer = new IPEndPoint(IPAddress.Parse(ipadress) , port);
			SocServer.Bind(ipendpoeintServer);
			SocServer.Listen(1);
			socClient = SocServer.Accept();
		}
		
		public void GetData(out string Data , int byteL = 255)
		{
			try 
			{
				byte[] barry = new byte[byteL];
				int recb = socClient.Receive(barry);
				if(recb > 0)
				{
					Data = Encoding.UTF8.GetString(barry , 0 , recb);
				}
				else
				{
					Data = "";
				}
			} 
			catch
			{
				Data = "";
			}
		}
		public void SendData(string Data , int byteL = 255)
		{
			byte[] barry = new byte[byteL];
			barry = Encoding.UTF8.GetBytes(Data);
			socClient.Send(barry);
		}
	}
}