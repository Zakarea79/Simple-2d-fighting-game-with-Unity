using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
	//کد انمیشن حرکت  جهت ایجاد رمز های ترکیبی 
	[SerializeField] private string moveAn = "ura";
	//کد انیمشن های ضربه های معمولی (مشت , لگد , ...)
	[SerializeField] private string fite = "cbd";
	[SerializeField] private ControlMain Player2ControlAI;
	[SerializeField] private Transform Player1;
	[SerializeField] private AIState AIstate = AIState.hard;

	private MoveBake BFMove = MoveBake.forward;
    void Start()
    {
		//اجرای متد ساخت کد برای ربات 
	    //InvokeRepeating("AICustom" , (float)AIstate / 10 , (float)AIstate / 10);
		StartCoroutine(AICustom());
		/*بر اساس متغییر 
		AIstate
		*/ 
    }
	bool GanrateCode = true;
	bool Move = false;

	//تصمیم برای بلند شدن 
	private void WakeUp()
	{
		if(Player2ControlAI.ISDoneE == false)
			return;
			
		Player2ControlAI.ISDoneE = false;
		
		int moveSelect = Random.Range(0 , moveAn.Length);
		int fiteSelect = Random.Range(0 , fite.Length);
		Player2ControlAI.anim.SetTrigger("" + fite.ToCharArray()[fiteSelect]);
	}
	private IEnumerator AICustom()
	{
		while(true)
		{
			//تصمیم برای بلند شدن 
			if(Player2ControlAI.ISDoneE == true){Invoke("WakeUp" , Random.Range(1 , (int)AIstate));}
			//در صورت فاصله مناسب شروع به ساخت کد کند
			if(Vector2.Distance(transform.position , Player1.position) < 3)
			{
				//تصمیم برای استفاده از رمز ساده یا ترکیبی
				int CHRSOH = Random.Range(1 , (int)AIstate * 5);

				if(CHRSOH > (int)AIstate && GanrateCode == true)
				{
					//ساخت ربات پیچیده
					int moveSelect = Random.Range(0 , moveAn.Length);
					int fiteSelect = Random.Range(0 , fite.Length);
					/*ارسال کد ساخته شده به اسکریپت 
					ControlMain
					متد 
					AIC*/
					Player2ControlAI.AIC("" + moveAn.ToCharArray()[moveSelect] + fite.ToCharArray()[fiteSelect]);
				}
				else if(GanrateCode == true)
				{
					//ساخت ضربات ساده
					int fiteSelect = Random.Range(0 , fite.Length);
					/*ارسال کد ساخته شده به اسکریپت 
					ControlMain
					متد 
					AIC*/
					Player2ControlAI.AIC("" + fite.ToCharArray()[fiteSelect]);
				}
			}
			else
			{
				// تصمیم برای حرکت به سمت کاراکتر یا دور شدن
				GanrateCode = false;
				if(Random.Range(1 ,11) > 5)
				{
					Move  = true;
					if(Random.Range(1 , 11) > 5 && Player1.GetComponent<ControlMain>().RanRamz == false)
					{
						BFMove = MoveBake.forward;
					}
					else if(ControlGelobalVarebal.HelsePlayer2 < 60)
					{
						BFMove = MoveBake.bake;
					}
				};
			};
			yield return new WaitForSeconds((float)AIstate / 10);
		}
		
	}
	protected void FixedUpdate()
	{
		//اجرای متد کنترل ریکست برای روبه روقرار دادن دو کاراکتر
		Player2ControlAI.ContraolRayCast();

		//حرکت به سمت کاراکتر
		if(ControlGelobalVarebal.EndGame == false)
		if(Move == true)
	    {
			if(Player2ControlAI.Move == true &&Player2ControlAI.CanMoves == true)
			{
				if(BFMove == MoveBake.forward)
				{
					transform.position = new Vector2(Mathf.MoveTowards(transform.position.x , Player1.position.x , .1f) , transform.position.y);
				}
				else
				{
					transform.position = new Vector2(Mathf.MoveTowards(transform.position.x , -Player1.position.x , .1f) , transform.position.y);
				}
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
