using UnityEngine;
using UnityEngine.UI;
public class controlLose : MonoBehaviour
{

	[SerializeField] private Slider player1 , player2;
	[SerializeField] private Text EndGamepanel;
	[SerializeField] private GameObject TimePuseAndRemouse;

	private void Update()
	{
		player1.value = ControlGelobalVarebal.HelsePlayer1;
		player2.value = ControlGelobalVarebal.HelsePlayer2;
	}

	public void controlLosePlayer()
	{
		if((ControlGelobalVarebal.HelsePlayer1 <= 0f && ControlGelobalVarebal.HelsePlayer2 <= 0f) ||
			(ControlGelobalVarebal.HelsePlayer1 == ControlGelobalVarebal.HelsePlayer2 && ControlGelobalVarebal.TimeV <= 0)
			)
		{
			EndGamepanel.text = ("player 1 , 2 Win");
			if(ControlGelobalVarebal.HelsePlayer2 <= 0 && ControlGelobalVarebal.HelsePlayer1 <= 0)
			{
				player1.gameObject.transform.Find("Fill Area").gameObject.SetActive(false);
				player2.gameObject.transform.Find("Fill Area").gameObject.SetActive(false);
			}
			TimePuseAndRemouse.SetActive(true);
			Time.timeScale = .3f;
		}
		else if(ControlGelobalVarebal.HelsePlayer1 <= 0f || 
			(ControlGelobalVarebal.HelsePlayer1 < ControlGelobalVarebal.HelsePlayer2 && ControlGelobalVarebal.TimeV <= 0))
		{
			EndGamepanel.text = ("Second Player Win");
			if(ControlGelobalVarebal.HelsePlayer1 <= 0)
			{
				player1.gameObject.transform.Find("Fill Area").gameObject.SetActive(false);
			}
			TimePuseAndRemouse.SetActive(true);
			Time.timeScale = .3f;
		}
		else if(ControlGelobalVarebal.HelsePlayer2 <= 0f || 
			(ControlGelobalVarebal.HelsePlayer1 > ControlGelobalVarebal.HelsePlayer2  && ControlGelobalVarebal.TimeV <= 0))
		{
			EndGamepanel.text = ("First player Win");
			if(ControlGelobalVarebal.HelsePlayer2 <= 0)
			{
				player2.gameObject.transform.Find("Fill Area").gameObject.SetActive(false);
			}
			TimePuseAndRemouse.SetActive(true);
			Time.timeScale = .3f;
		}
	}
}
