using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Aghel 0101

public class AI : MonoBehaviour
{
	[SerializeField] private string moveAn = "ura";
	[SerializeField] private string fite = "cbd";
	[SerializeField] private ControlMain Player2ControlAI;
	[SerializeField] private Transform Player1;
	[SerializeField] private AIState AIstate = AIState.hard;
    void Start()
    {
	    InvokeRepeating("AICustom" , (float)AIstate / 10 , (float)AIstate / 10);
    }
	bool GanrateCode = true;
	bool Move = false;
	private void WakeUp()
	{
		if(Player2ControlAI.ISDoneE == false)
			return;
			
		Player2ControlAI.ISDoneE = false;
		
		int moveSelect = Random.Range(0 , moveAn.Length);
		int fiteSelect = Random.Range(0 , fite.Length);
		Player2ControlAI.anim.SetTrigger("" + fite.ToCharArray()[fiteSelect]);
	}
	private void AICustom()
	{
		if(Player2ControlAI.ISDoneE == true){Invoke("WakeUp" , Random.Range(1 , (int)AIstate));}
		if(Vector2.Distance(transform.position , Player1.position) < 3)
		{
			int CHRSOH = Random.Range(1 , (int)AIstate * 5);
			if(CHRSOH > (int)AIstate && GanrateCode == true)
			{
				int moveSelect = Random.Range(0 , moveAn.Length);
				int fiteSelect = Random.Range(0 , fite.Length);
				Player2ControlAI.AIC("" + moveAn.ToCharArray()[moveSelect] + fite.ToCharArray()[fiteSelect]);
			}
			else if(GanrateCode == true)
			{
				int fiteSelect = Random.Range(0 , fite.Length);
				Player2ControlAI.AIC("" + fite.ToCharArray()[fiteSelect]);
			}
		}
		else
		{
			GanrateCode = false;
			if(Random.Range(1 ,11) > 5)
			{
				Move  = true;
			};
		};
	}
	protected void FixedUpdate()
	{
		Player2ControlAI.ContraolRayCast();
		if(ControlGelobalVarebal.EndGame == false)
		if(Move == true)
	    {
			if(Player2ControlAI.Move == true &&Player2ControlAI.CanMoves == true)
			{
				
				transform.position = new Vector2(Mathf.MoveTowards(transform.position.x , Player1.position.x , .1f) , transform.position.y);
				Player2ControlAI.anim.SetBool("walk" , true);
				if(Player2ControlAI.ISDoneE == true)
				{
					Player2ControlAI.anim.SetTrigger("setup");
					Player2ControlAI.ISDoneE = false;
				}
			}
			else
			{
				Player2ControlAI.anim.SetBool("walk" , false);
			}
	    }
		if(Vector2.Distance(transform.position , Player1.position) < 3)
		{
			Move = false; GanrateCode = true;
		}
    }
}
