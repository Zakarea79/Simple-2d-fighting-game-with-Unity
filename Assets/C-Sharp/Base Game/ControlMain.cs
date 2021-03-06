using UnityEngine;

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
	[SerializeField] private int R  = 4 , L = 5;
	[Space(10)]
	[SerializeField] private AdvanceRamz[] Ramzv;
	[SerializeField] private AdvanceRamz[] RamzEmpty;
	[SerializeField] private Transform otherPlayerOrBot;
	public Animator otherPlayerAnimator;
	private bool PlayAnmtion = false;
	//----------------------------------------------------------------
	public bool Move = false;
	private bool ranAnim = true;
	private string ramz = "";
	public bool RanRamz = false;
	public Animator anim;
	private SimpelUDP sendGetData;
	//----------------------------------------------------------------
	Rigidbody2D rg;
	SpriteRenderer SelfSpriteRenderer;
	//----------------------------------------------------------------
	[SerializeField] private bool mPLAYER = false;
    void Start()
    {
		if(mPLAYER == true)
			SendGetData = GameObject.Find("Control Scene").GetComponent<SimpelUDP>();
    	rg = GetComponent<Rigidbody2D>();
		SelfSpriteRenderer = GetComponent<SpriteRenderer>();
    	if(invrse == true)
    	{
    		speed = -speed;
    	}
    }
    
    void Update()
    {
		if(ControlGelobalVarebal.StartGame == false)
			return;
    	//?????????????? ???? ?????? ?????? ???? ???????? ???? 0 ?????? ?????????? ???????? ???? ???????? ???????? ??????
    	if(ControlGelobalVarebal.HelsePlayer1 <= 0f || ControlGelobalVarebal.HelsePlayer2 <= 0f)
    	{
    		ControlGelobalVarebal.EndGame = true;
    	}
		if(ZInput.GetKeyUp(statinfo[2].Kay) && defa == true)
		{
			anim.SetTrigger("cnd");
			SteatAnimtrue();
			print("Test");
			defa = false;
		}
		//?????????? ???? ???????? ?????????????? ???? ?????? ???? ????????
		ContraolRayCast();
		//?????????? ???????????? ???? ?? ???????????? ????????
    	ControlAnmiton();
		//???????? ???????? ???? ?????? ?????? ?? ??????
    	ControlMove();
		//????????
    	JUMPControl();
		if(mPLAYER == true)
			SendGetData.Send();
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
					string temp = statinfo[R].stateName;
					statinfo[R].stateName = statinfo[L].stateName;
					statinfo[L].stateName = temp;
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
					string temp = statinfo[R].stateName;
					statinfo[R].stateName = statinfo[L].stateName;
					statinfo[L].stateName = temp;
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
	    	
	    	if(ZInput.GetKeyPress(left) && Move == true && CanMoves == true)
	    	{
	    		transform.Translate(Vector2.left * Time.deltaTime * speed);
	    		anim.SetBool("walk" , true);
				if(ISDoneE == true)
				{
					anim.SetTrigger("setup");
					ISDoneE = false;
				}
	    	}
	    	else if(ZInput.GetKeyPress(right) && Move == true == CanMoves == true)
	    	{
	    		transform.Translate(Vector2.left * Time.deltaTime * -speed);
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
    	if(ZInput.GetKeyDown(JumpKey) && Jump == false)
    	{
    		Jump = true;
    	}
    	
    	if(Jump == true && transform.position.y < 3)
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
					//RanRamz = true;
					return;
				}
			}
			foreach (var elementRamzEmpty in RamzEmpty) 
			{
				if(elementRamzEmpty.stateNameZarbeh == ramz)
				{
					anim.SetTrigger(ramz);
					DamageAndPlayBestAnmiton(elementRamzEmpty.stateNameDameage);
					damageValue = elementRamzEmpty.damage;
					Move = elementRamzEmpty.Move;
					DestanseWork = elementRamzEmpty.Destanse;
					ranAnim = false;
					//RanRamz = true;
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
	    		if(ZInput.GetKeyDown(element.Kay))
	    		{
	    			ramz += element.stateName;
	    			
	    			foreach (var elementv in Ramzv) 
	    			{
	    				if(ramz == elementv.stateNameZarbeh)
	    				{
	    					anim.SetTrigger(ramz);
	    					ranAnim = false;
							if(mPLAYER == true)
								SendGetData.Ramz = ramz;
							//sendGetData.Send();
	    					ramz = "";
	    					DamageAndPlayBestAnmiton(elementv.stateNameDameage);
	    					damageValue = elementv.damage;
	    					Move = elementv.Move;
	    					DestanseWork = elementv.Destanse;
							RanRamz = true;
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
							if(mPLAYER == true)
								SendGetData.Ramz = ramz;
							//sendGetData.Send();
	    					DamageAndPlayBestAnmiton(elementRamzEmpty.stateNameDameage);
	    					damageValue = elementRamzEmpty.damage;
	    					Move = elementRamzEmpty.Move;
	    					DestanseWork = elementRamzEmpty.Destanse;
	    					ranAnim = false;
							RanRamz = true;
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
    	if(stateNameorjnal != ""
            && chekDestansePlayer() <= DestanseWork
            && PlayAnmtion == false
            && otherPlayerAnimator.GetComponent<ControlMain>().ISDoneE == false)
    	{
    		otherPlayerAnimator.SetTrigger(stateNameorjnal);
    		//print(stateNameorjnal);
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
    
	//?????????? ?????????? ???????????? ???????? ??????
    private void DamageAndPlayBestAnmiton(string stateName)
    {
    	stateNameorjnal = stateName;
    }
    
	//?????????? ???????? ???? ????????
    private float chekDestansePlayer()
    {
    	return Vector2.Distance(transform.position , otherPlayerOrBot.position);
    }
	//?????? ???????? ???? ?????????? ??????
    public void Cansel_Ramz()
    {
    	ramz = "";
    }
	//?????????? ?????????? ????????
    public void SteatAnimtrue()
    {
    	if(ControlGelobalVarebal.EndGame == true)
    	{
    		Invoke("KO" , 2f);
    	}
    	else
    	{
			RanRamz = false;
			PlayAnmtion = false;
	    	ranAnim = true;
	    	Move = true;
			CanMoves = true;
			ISDoneE = false;
    	}
    }
	// ???????? ?????????? ?????????? ?? ????????
	private bool defa = false;

    public SimpelUDP SendGetData { get => sendGetData; set => sendGetData = value; }

    public void IsDowne()
	{
		ISDoneE = true;
		defa = true;
	}
	//?????? ?????????????? ???????? ????????
	public void CannotMove()
	{
		CanMoves = false;
		PlayAnmtion = true;
	}
	//?????????? ???????? ????????
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