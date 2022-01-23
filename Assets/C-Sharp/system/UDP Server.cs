using System;
using System.Net;
using System.Text;
using System.Linq;
using System.Net.Sockets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Timers;
using UnityEngine.UI;

namespace Supernova_Server
{
	public class Custom_Server
	{
		
		public static List<String> GetIpAdress()
		{
			List<string> ipAdress = new List<string>();
			foreach (var element in Dns.GetHostAddresses(Dns.GetHostName())) 
			{
				if(element.AddressFamily == AddressFamily.InterNetwork && element.ToString() != "127.0.0.1")
				{
					ipAdress.Add(element.ToString());
				}
			}
			return ipAdress;
		}
		//-------------------------------------------------------------------
		//-------------------------------------------------------------------
		//-------------------------------------------------------------------
		//-------------------------------------------------------------------
		//-------------------------------------------------------------------
		//-------------------------------------------------------------------
		//Socket so1;
		//public void INITSERVER(string IPAderssV , int port)
		//{
		//	so1 = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
		//	so1.Bind(new IPEndPoint(IPAddress.Parse(IPAderssV), port));
		//}
		//public string GetDataServerUDP()
		//{
		//	byte[] buffer = new byte[2048];
		//	int r = so1.Receive(buffer);
		//	if (r > 0)
		//	{
		//		return (Encoding.Unicode.GetString(buffer, 0, r));
		//	}
		//	else
		//	{
		//		return "";
		//	}
		//}
		
		//public void ServerSendUDP(String SendData ,string IPAddressV , int port)
		//{
		//	byte[] buffer = new byte[2048];
		//	buffer = Encoding.Unicode.GetBytes(SendData);
		//	so1.SendTo(buffer, new IPEndPoint(IPAddress.Parse(IPAddressV), port));
		//}
		////-------------------------------------------------------------------
		////-------------------------------------------------------------------
		////-------------------------------------------------------------------
		//Socket so2;
		//public void INITCLIENT(string IPAderssV , int port)
		//{
		//	so2 = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
		//	so2.Bind(new IPEndPoint(IPAddress.Parse(IPAderssV), port));
		//}
		
		//public string CientRseveUDP()
		//{
		//	byte[] buffer = new byte[2048];
		//	int r = so2.Receive(buffer);
		//	if (r > 0)
		//	{
		//		return Encoding.Unicode.GetString(buffer, 0, r);
		//	}
		//	else
		//	{
		//		return "";
		//	}
		//}
		//public void ClientSendUDP(String SendData ,string IPAddressV , int port)
		//{
		//	byte[] buffer = new byte[2048];
		//	buffer = Encoding.Unicode.GetBytes(SendData);
		//	so2.SendTo(buffer, new IPEndPoint(IPAddress.Parse(IPAddressV), port));
		//}
		//-------------------------------------------------------------------
		//-------------------------------------------------------------------
		//-------------------------------------------------------------------
		//-------------------------------------------------------------------
		//-------------------------------------------------------------------
		//-------------------------------------------------------------------
		//-------------------------------------------------------------------
		private IPEndPoint ipeServer;
		public UdpClient UDPServer;
		
		public void Server_Conected(int port , IPAddress ip)
		{
			UDPServer = new UdpClient(port);
			ipeServer = new IPEndPoint(ip ,0);
		}
		public void ServerSendAndGetData(string Send , out string Get , int byteLength = 255)
		{
			byte[] b = new byte[byteLength];
			b = UDPServer.Receive(ref ipeServer);
			Get = ASCIIEncoding.UTF8.GetString(b);
			
			byte[] b2 = new byte[byteLength];
			b2 = ASCIIEncoding.UTF8.GetBytes(Send);
			UDPServer.Send(b2 , b2.Length , ipeServer);
		}
		
		public void ServerSendAndGetData2(int port , IPAddress ip , string Send , out string Get , int byteLength = 255)
		{
			UDPServer = new UdpClient(port);
			ipeServer = new IPEndPoint(ip ,0);
			
			byte[] b = new byte[byteLength];
			b = UDPServer.Receive(ref ipeServer);
			Get = ASCIIEncoding.UTF8.GetString(b);
			
			byte[] b2 = new byte[byteLength];
			b2 = ASCIIEncoding.UTF8.GetBytes(Send);
			UDPServer.Send(b2 , b2.Length , ipeServer);
			
			UDPServer.Close();
		}
		//-------------------------------------------------------------------
		public void ServerSendData(string Send , int byteLength = 255)
		{
			byte[] b2 = new byte[byteLength];
			b2 = ASCIIEncoding.UTF8.GetBytes(Send);
			UDPServer.Send(b2 , b2.Length , ipeServer);
		}
		
		public string ServerGetData(int byteLength = 255)
		{
			byte[] b = new byte[byteLength];
			b = UDPServer.Receive(ref ipeServer);
			return  ASCIIEncoding.UTF8.GetString(b);
		}
		//-------------------------Client-----------------------------------------
		public UdpClient UDPClient = new UdpClient();
		private IPEndPoint ipeClient;
		
		public void Client_Conected(int port , IPAddress ip)
		{
			ipeClient = new IPEndPoint(ip , port);
			UDPClient.Connect(ipeClient);
		}
		
		public void ClientSendAndGetData(string Send , out string Get , int byteLength = 255)
		{
			byte[] b = new byte[byteLength];
			b = ASCIIEncoding.UTF8.GetBytes(Send);
			UDPClient.Send(b , b.Length);
			
			byte[] b2 = new byte[byteLength];
			b2 = UDPClient.Receive(ref ipeClient);
			Get = ASCIIEncoding.UTF8.GetString(b2);
		}
		
		public void ClientSendAndGetData2(int port , IPAddress ip , string Send , out string Get , int byteLength = 255)
		{
			ipeClient = new IPEndPoint(ip , port);
			UDPClient.Connect(ipeClient);
			
			byte[] b = new byte[byteLength];
			b = ASCIIEncoding.UTF8.GetBytes(Send);
			UDPClient.Send(b , b.Length);
			
			byte[] b2 = new byte[byteLength];
			b2 = UDPClient.Receive(ref ipeClient);
			Get = ASCIIEncoding.UTF8.GetString(b2);
			
			UDPClient.Close();
		}
		
		//-----------------------------------------------------------------------
		public void ClientSendData(string Send , int byteLength = 255)
		{
			byte[] b = new byte[byteLength];
			b = ASCIIEncoding.UTF8.GetBytes(Send);
			UDPClient.Send(b , b.Length);
		}
		
		public string ClientGetData(int byteLength = 255)
		{
			byte[] b2 = new byte[byteLength];
			b2 = UDPClient.Receive(ref ipeClient);
			return ASCIIEncoding.UTF8.GetString(b2);
		}
		//------------------------------------------------------------------------
	}
}