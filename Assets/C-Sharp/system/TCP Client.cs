using System;
using System.Text;
using System.Net.Sockets;
using System.Net;
namespace Supernova_Server
{
	public class CustomClientTCP
	{
		Socket sClient = new Socket(AddressFamily.InterNetwork , SocketType.Stream , ProtocolType.Tcp);
		public void ConectedToServer(int port , string ipAderss)
		{
			IPEndPoint ipsever = new IPEndPoint(IPAddress.Parse(ipAderss) ,port);
			sClient.Connect(ipsever);
		}
		
		public void GetData(out string DataReseve , int byteL = 255)
		{
			try 
			{
				byte[] b = new byte[1024];
				int r = sClient.Receive(b);
				if(r > 0)
				{
					DataReseve = Encoding.UTF8.GetString(b , 0 , r);
				}else{DataReseve = "";}
			} 
			catch
			{
				DataReseve = "";
			}
		}
		public void SendData(string Data , int byteL = 255)
		{
			byte[] b = new byte[byteL];
			b = Encoding.UTF8.GetBytes(Data);
			sClient.Send(b);
		}
	}
}