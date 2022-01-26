using System;
using UnityEngine;
using UnityEngine.UI;

public class ControlMain : MonoBehaviour
{
	//----------------------------------------------------------------
	[Header("Control Helse")]
	[SerializeField] private controlLose SelectLoser;
	//----------------------------------------------------------------
	[Header("Move Player")]
	[Range(0 ,3)]
	[SerializeField] private float RayCastHitLenth = 10;
	[SerializeField] private WPlayer PlayerOneOrTow;
	[SerializeField] private bool invrse;
	[SerializeField] private float speed;
	[SerializeField] private float JumpForce = 10;
	[SerializeField] private KeyCode right;
	[SerializeField] private KeyCode left;
	[SerializeField] private KeyCode JumpKey;
	[SerializeField] private MovePlayerLeftAndRite ThisSide;
	public bool ISDoneE = false;
	public bool CanMoves = true;
	//----------------------------------------------------------------
	[Header("Helse Control")]
	[SerializeField] private WPlayer HowPlayer = WPlayer.Player1;
	//----------------------------------------------------------------
	[Header("contrrol Anmtion and  Crsh")]
	[SerializeField] private anmtionControl[] statinfo;
	[SerializeField] private AdvanceRamz[] Ramzv;
	[SerializeField] private AdvanceRamz[] RamzEmpty;
	[SerializeField] private Transform otherPlayerOrBot;
	public Animator otherPlayerAnimator;
	private bool PlayAnmtion = false;
	//----------------------------------------------------------------
	public bool Move = false;
	private bool ranAnim = true;
	private string ramz = "";
	public Animator anim;
	//----------------------------------------------------------------
	Rigidbody2D rg;
	SpriteRenderer SelfSpriteRenderer;
	//----------------------------------------------------------------
    void Start()
    {
    	rg = GetComponent<Rigidbody2D>();
		SelfSpriteRenderer = GetComponent<SpriteRenderer>();
    	if(invrse == true)
    	{
    		speed = -speed;
    	}
    }
    
    void Update()
    {
    	
    	if(ControlGelobalVarebal.HelsePlayer1 <= 0f || ControlGelobalVarebal.HelsePlayer2 <= 0f)
    	{
    		ControlGelobalVarebal.EndGame = true;
    	}
		ContraolRayCast();
    	ControlAnmiton();
    	ControlMove();
    	JUMPControl();
    }
    
	public void ContraolRayCast()
	{	
    	RaycastHit2D hitR = Physics2D.Raycast(new Vector2(transform.position.x + 3, transform.position.y + 3) , Vector2.right , RayCastHitLenth);
    	if(hitR.collider != null)
    	{
    		if(hitR.collider.gameObject == otherPlayerOrBot.gameObject)
    		{
	    		if(otherPlayerOrBot.GetComponent<ControlMain>().ISDoneE == false)
	    		{
		    		//SelfSpriteRenderer.flipX = false;
    				otherPlayerOrBot.GetComponent<SpriteRenderer>().flipX = true;
	    		}

			}
    	}
    	Debug.DrawRay(new Vector2(transform.position.x + 3 , transform.position.y + 3) , Vector2.right * RayCastHitLenth , Color.red);
    	//----------------------------------------------------------------
    	RaycastHit2D hitL = Physics2D.Raycast(new Vector2(transform.position.x - 3 , transform.position.y + 3) , Vector2.left , RayCastHitLenth);
    	if(hitL.collider != null)
    	{
    		if(hitL.collider.gameObject == otherPlayerOrBot.gameObject)
    		{
	    		if(otherPlayerOrBot.GetComponent<ControlMain>().ISDoneE == false)
	    		{
    				//SelfSpriteRenderer.flipX = true;
    				otherPlayerOrBot.GetComponent<SpriteRenderer>().flipX = false;
	    		}
			}
    	}
    	Debug.DrawRay(new Vector2(transform.position.x - 3 , transform.position.y + 3) , Vector2.left * RayCastHitLenth , Color.green);
    	//----------------------------------------------------------------
	}
	
    private void ControlMove()
    {
    	if(ControlGelobalVarebal.EndGame == false && Time.timeScale != 0)
    	{
	    	if(chekDestansePlayer() > 5 && chekDestansePlayer() < 15)
	    	{
	    		Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize , chekDestansePlayer() , .1f);
	    	}
	    	
	    	if(Input.GetKey(left) && Move == true && CanMoves == true)
	    	{
	    		transform.Translate(Vector2.left * Time.deltaTime * speed);
	    		//rg.MovePosition(Vector2.left * Time.deltaTime * speed);
	    		anim.SetBool("walk" , true);
				if(ISDoneE == true)
				{
					anim.SetTrigger("setup");
					ISDoneE = false;
				}
	    	}
	    	else if(Input.GetKey(right) && Move == true == CanMoves == true)
	    	{
	    		transform.Translate(Vector2.left * Time.deltaTime * -speed);
	    		//rg.MovePosition(Vector2.left * Time.deltaTime * -speed);
	    		anim.SetBool("walk" , true);
				if(ISDoneE == true)
				{
					anim.SetTrigger("setup");
					ISDoneE = false;
				}
	    	}
	    	else
	    	{
	    		anim.SetBool("walk" , false);
	    	}
    	}
    }
    public bool Jump = false;
    public bool ThisNotOnErze = false;
    private void JUMPControl()
    {
    	if(Input.GetKeyDown(JumpKey) && Jump == false)
    	{
    		Jump = true;
    	}
    	
    	if(Jump == true && transform.position.y < 5)
    		transform.Translate(Vector2.up * JumpForce * Time.deltaTime);
    	else if( ThisNotOnErze == true)
    	{
    		Jump = false;
    		transform.Translate(-Vector2.up * JumpForce * Time.deltaTime);
    	}
    	if(transform.position.y < -5)
    	{
    		ThisNotOnErze = true;
    		transform.position = new Vector2(transform.position.x , -5);
    	}
    }
    
	public void AIC(string ramz)
	{
		if(ranAnim == true && ControlGelobalVarebal.EndGame == false && Time.timeScale != 0)
		{
			foreach (var elementv in Ramzv) 
			{
				if(ramz == elementv.stateNameZarbeh)
				{
					anim.SetTrigger(ramz);
					ranAnim = false;
					ramz = "";
					DamageAndPlayBestAnmiton(elementv.stateNameDameage);
					//print(elementv.stateNameDameage);
					damageValue = elementv.damage;
					Move = elementv.Move;
					DestanseWork = elementv.Destanse;
					return;
				}
			}
			foreach (var elementRamzEmpty in RamzEmpty) 
			{
				if(elementRamzEmpty.stateNameZarbeh == ramz)
				{
					anim.SetTrigger(ramz);
					DamageAndPlayBestAnmiton(elementRamzEmpty.stateNameDameage);
					//print(elementRamzEmpty.stateNameDameage);
					damageValue = elementRamzEmpty.damage;
					Move = elementRamzEmpty.Move;
					DestanseWork = elementRamzEmpty.Destanse;
					ranAnim = false;
					return;
				}
				Invoke("Cansel_Ramz" , 1f);
			}
		}
	}
    
    private void ControlAnmiton()
    {
    	if(ranAnim == true && ControlGelobalVarebal.EndGame == false && Time.timeScale != 0)
    	{
	    	foreach (var element in statinfo)
	    	{
	    		if(Input.GetKeyDown(element.Kay))
	    		{
	    			
	    			ramz += element.stateName;
	    			
	    			foreach (var elementv in Ramzv) 
	    			{
	    				if(ramz == elementv.stateNameZarbeh)
	    				{
	    					anim.SetTrigger(ramz);
	    					ranAnim = false;
	    					ramz = "";
	    					DamageAndPlayBestAnmiton(elementv.stateNameDameage);
	    					damageValue = elementv.damage;
	    					Move = elementv.Move;
	    					DestanseWork = elementv.Destanse;
	    					return;
	    				}
	    			}
	    			if(ramz.Length > 1)
	    			{
	    				ramz = "";
	    			}
	    			foreach (var elementRamzEmpty in RamzEmpty) 
	    			{
	    				if(elementRamzEmpty.stateNameZarbeh == element.stateName)
	    				{
	    					anim.SetTrigger(element.stateName);
	    					DamageAndPlayBestAnmiton(elementRamzEmpty.stateNameDameage);
	    					damageValue = elementRamzEmpty.damage;
	    					Move = elementRamzEmpty.Move;
	    					DestanseWork = elementRamzEmpty.Destanse;
	    					ranAnim = false;
	    					return;
	    				}
	    				Invoke("Cansel_Ramz" , 1f);
	    			}
	    			ramz = element.stateName;
	    		}
	    	}
    	}
    }
    string stateNameorjnal = "";
    float damageValue;
    float DestanseWork;
    public void PlayAnmtionDamageOterPlayer()
    {
    	if(stateNameorjnal != "" && chekDestansePlayer() <= DestanseWork && PlayAnmtion == false)
    	{
    		otherPlayerAnimator.SetTrigger(stateNameorjnal);
    		print(stateNameorjnal);
    		if(HowPlayer == WPlayer.Player1)
    		{
    			ControlGelobalVarebal.HelsePlayer1 -= damageValue;
    		}
    		else
    		{
    			ControlGelobalVarebal.HelsePlayer2 -= damageValue;
    		}
    		SelectLoser.controlLosePlayer();
    	}
    }
    
    private void DamageAndPlayBestAnmiton(string stateName)
    {
    	stateNameorjnal = stateName;
    }
    
    private float chekDestansePlayer()
    {
    	return Vector2.Distance(transform.position , otherPlayerOrBot.position);
    }
    public void Cansel_Ramz()
    {
    	ramz = "";
    }
    public void SteatAnimtrue()
    {
    	if(ControlGelobalVarebal.EndGame == true)
    	{
    		Invoke("KO" , 2f);
    	}
    	else
    	{
			PlayAnmtion = false;
	    	ranAnim = true;
	    	Move = true;
			CanMoves = true;
    	}
    }
	
	public void IsDowne()
	{
		ISDoneE = true;
	}
	public void CannotMove()
	{
		CanMoves = false;
		PlayAnmtion = true;
	}
	public void CanMove()
	{
		CanMoves = true;
		PlayAnmtion = false;
		Move = true;
	}
    private void KO()
    {
    	Time.timeScale = 0;
    }
}