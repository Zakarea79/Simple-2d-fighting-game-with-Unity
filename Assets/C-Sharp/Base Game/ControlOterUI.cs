using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ControlOterUI : MonoBehaviour
{

    private void Start()
    {
        InvokeRepeating("TimeX" , 1f ,1f);
    }
    [SerializeField] private Text TimerText;
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
