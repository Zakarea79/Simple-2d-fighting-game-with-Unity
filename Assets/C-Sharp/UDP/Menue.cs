using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using System.Net;
using Supernova_Server;
using static MainPlayer;

public class Menue : MonoBehaviour
{
    [SerializeField] private Toggle Server , Client;
    [SerializeField] private GameObject PS , PC;
    [SerializeField] private Text ShowIp;
    [SerializeField] private InputField GetIp;
    [SerializeField] private Button btn_Serch , btn_nextIp , btn_StartServer ,btnConectClitent;
    private List<string> ListIP = new List<string>();
    int ipCont = 0;
    private Thread ThreadServer;

    [HideInInspector] public string getServerSata , getClientSata;
    [SerializeField] private Text Show_Error;
    private string Error;
    void Start()
    {
        //اضافه کردن ایونت توگل ها
        Server.onValueChanged.AddListener(delegate { ChToggle(); });
        Client.onValueChanged.AddListener(delegate { ChToggle(); });
        //پید اکردن تمام ایپی های قابل اتصال
        btn_Serch.onClick.AddListener(delegate()
        {
            ListIP = Custom_Server.GetIpAdress();
            ShowIp.text = ListIP.Count != 0 ? ListIP[0] : "No Connected Serves";
        });
        //نمایش اپی های پیدا شده رولی لیبل
        btn_nextIp.onClick.AddListener(delegate()
        {
            ipCont ++;
            if(ipCont >= ListIP.Count){ipCont = 0;}
            ShowIp.text = ListIP[ipCont];
        });
        //روشن کردن سرور
        btn_StartServer.onClick.AddListener(delegate ()
        {
            new Thread(new ThreadStart(()=>
            {
                try
                {
                    ObjectServerUDP.Server_Conected(2500,  IPAddress.Parse(ShowIp.text));
                    ObjectServerUDP.ServerSendAndGetData("Test" ,  out getServerSata);
                }
                catch(System.Exception ex)
                {
                    Error = ex.Message;
                }
            })).Start();
            btn_StartServer.gameObject.SetActive(false);
            btn_nextIp.gameObject.SetActive(false);
            btn_Serch.gameObject.SetActive(false);
        });
        //وصل شدن کلاینت به سرور
        btnConectClitent.onClick.AddListener(delegate ()
        {
            print("Test");
            new Thread(new ThreadStart(()=>
            {
                try
                {
                    ObjectClientUDP.Client_Conected(2500 ,IPAddress.Parse(GetIp.text));
                    ObjectClientUDP.ClientSendAndGetData("Test" ,  out getClientSata);
                }
                catch(System.Exception ex)
                {
                    Error = ex.Message;
                }
                
            })).Start();
        });
    }

    private void FixedUpdate()
    {
        Show_Error.text = Error;   
    }

    private void ChToggle()
    {
        PS.SetActive(Server.isOn);
        PC.SetActive(Client.isOn);
    }
}
