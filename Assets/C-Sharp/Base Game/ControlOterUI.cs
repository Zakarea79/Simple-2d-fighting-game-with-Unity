using UnityEngine;
using UnityEngine.UI;
public class ControlOterUI : MonoBehaviour
{

    private void Start()
    {
        InvokeRepeating("TimerStart" , 1f ,1f);
    }
    [SerializeField] private Text TimerText;
    [SerializeField] private Text TimerstartGameText;
    private void TimeX()
    {
        if(ControlGelobalVarebal.TimeV > 0)
        {
            ControlGelobalVarebal.TimeV--;
            TimerText.text = ControlGelobalVarebal.TimeV.ToString();
            GetComponent<controlLose>().controlLosePlayer();
        }
        else
        {
        	CancelInvoke("TimeX");
            ControlGelobalVarebal.EndGame = true;
        }
    }
    int time = 3;
    private void TimerStart()
    {
        if(time > 1)
        {
            time --;
            TimerstartGameText.text = time.ToString();
            //Play Sound
        }
        else if(time > 0)
        {
            TimerstartGameText.text = "Fight";
            //Play Sond
            InvokeRepeating("TimeX" , 1f ,1f);
            //TimerstartGameText.gameObject.SetActive(false);
            //ControlGelobalVarebal.StartGame = true;
            time --;
            
        }else
        {
            ControlGelobalVarebal.StartGame = true;
            TimerstartGameText.gameObject.SetActive(false);
            CancelInvoke("TimerStart");
        }
    }


    public void ToggleControl(Toggle ThisToggle)
    {
        if(ThisToggle.isOn == true)
        {
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0;
        }
    }
}
