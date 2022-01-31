using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using static MainPlayer;
public class SimpelUDP : MonoBehaviour
{
    public string Ramz = "";
    private string send = "0,0" , Get = "0,0";
    private ControlMain PlayAnim1 , PlayAnim2;
    public GameObject Player1 , Player2;
    //ارسال اطلاعات 
    public void Send_and_Get_Data()
    {
        if(PlayerCS == WPlayer.Player1)
        {
            ObjectServerUDP.ServerSendAndGetData(send ,out Get);
        }
        else
        {
            ObjectClientUDP.ClientSendAndGetData(send ,out Get);
        }
    }
    private void Awake() 
    {
        if(PlayerCS == WPlayer.Player1)
        {
            PlayAnim2 = Player2.GetComponent<ControlMain>();
            PlayAnim2.enabled = false;
        }
        else
        {
            PlayAnim1 =  Player1.GetComponent<ControlMain>();
            PlayAnim1.enabled = false;
        }
    }
    public void Send()
    {
        Task.Run(()=>
        {
            Send_and_Get_Data();
        });
        if(PlayerCS == WPlayer.Player1)
        {
            PlayAnim2.ContraolRayCast();
            //قالب بندی اطلاعات ارسالی
            send = $"{Player1.transform.position.x},{Player1.transform.position.y},{Ramz}";
            //استخراج اطلاعات دریافتی
            float xGet = float.Parse(Get.Split(',')[0]);
            float yGet = float.Parse(Get.Split(',')[1]);
            PlayAnim2.AIC(Get.Split(',')[2]);
            Ramz = "";
            //حرکت پلیر 2
            Player2.transform.position = new Vector2(xGet, yGet); 
        }
        else
        {
            PlayAnim1.ContraolRayCast();
            //قالب بندی اطلاعات ارسالی
            send = $"{Player2.transform.position.x},{Player2.transform.position.y},{Ramz}";
            //استخراج اطلاعات دریافتی
            float xGet = float.Parse(Get.Split(',')[0]);
            float yGet = float.Parse(Get.Split(',')[1]);
            PlayAnim1.AIC(Get.Split(',')[2]);
            Ramz = "";
            //حرکت پلیر 2
            Player1.transform.position = new Vector2(xGet, yGet); 
        }
    }
}
