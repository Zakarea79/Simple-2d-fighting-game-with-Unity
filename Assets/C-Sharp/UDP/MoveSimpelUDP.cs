using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using static MainPlayer;
public class MoveSimpelUDP : MonoBehaviour
{
    [SerializeField] private string send = "0,0" , Get = "0,0";
    public GameObject Player1 , Player2;
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
    
    void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        float x = Input.GetAxis("Horizontal") * 10 * Time.deltaTime;
        float y = Input.GetAxis("Vertical") * 10 * Time.deltaTime;

        Task.Run(()=>
        {
            Send_and_Get_Data();
        });

        if(PlayerCS == WPlayer.Player1)
        {
            Player1.transform.Translate(new Vector2(x ,y));
            send = $"{Player1.transform.position.x},{Player1.transform.position.y}";
            float xGet = float.Parse(Get.Split(',')[0]);
            float yGet = float.Parse(Get.Split(',')[1]);
            Player2.transform.position = new Vector2(xGet, yGet);
        }
        else
        {
            Player2.transform.Translate(new Vector2(x ,y));
            send = $"{Player2.transform.position.x},{Player2.transform.position.y}";
            float xGet = float.Parse(Get.Split(',')[0]);
            float yGet = float.Parse(Get.Split(',')[1]);
            Player1.transform.position = new Vector2(xGet, yGet);
        }
    }
}
