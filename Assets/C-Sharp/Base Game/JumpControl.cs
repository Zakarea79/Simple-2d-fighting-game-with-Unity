using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpControl : MonoBehaviour
{
	protected void OnCollisionExit2D(Collision2D collisionInfo)
	{
		if(collisionInfo.gameObject.name == "player 1" || collisionInfo.gameObject.name == "player 2")
		{
			collisionInfo.gameObject.GetComponent<ControlMain>().ThisNotOnErze = true;
			//collisionInfo.gameObject.GetComponent<ControlMain>().jumpCansel = true;
		}
	}
	protected void OnCollisionEnter2D(Collision2D collisionInfo)
	{
		if(collisionInfo.gameObject.name == "player 1" || collisionInfo.gameObject.name == "player 2")
		{
			collisionInfo.gameObject.GetComponent<ControlMain>().ThisNotOnErze = false;
		}
	}
}
