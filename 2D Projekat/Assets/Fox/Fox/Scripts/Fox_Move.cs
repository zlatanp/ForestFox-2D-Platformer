using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Fox_Move : MonoBehaviour {

    public float speed,jumpForce,cooldownHit;
	public bool running,up,down,jumping,crouching,dead,attacking,special,respawning;
    private Rigidbody2D rb;
    private Animator anim;
	private SpriteRenderer sp;
	private float rateOfHit;
	private GameObject[] life;
	private int qtdLife;
    private Vector3 pozicija = new Vector3(-7.53f, 1.33f, 0f);
    private Collider2D igrac;
    private Collider2D novcic;
    public int poeni = 0;
    public Text pointsText;
    public Text gameover;
    public Animation kraj;
    public Animation cekpoint;
    private bool prvicek = false;
    private bool drugicek = false;
    public AudioSource novcicSnd;
    public AudioSource skociSnd;
    public AudioSource umriSnd;
    public AudioSource glavna;
    public Animation win;
    private bool winer = false;
    //private GameObject propo, propo2;

	// Use this for initialization
	void Start () {
         igrac = GetComponent<Collider2D>();
         novcic = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
		sp=GetComponent<SpriteRenderer>();
		running=false;
		up=false;
		down=false;
		jumping=false;
		crouching=false;
        respawning = false;
        special = false;
        novcicSnd.mute = true;
        skociSnd.mute = true;
        umriSnd.mute = true;
		rateOfHit=Time.time;
		life=GameObject.FindGameObjectsWithTag("Life");
		qtdLife=life.Length;

        //if(igrac.transform.position.x >55.5f && igrac.transform.position.x <62.5f)
        //   anim.SetBool("Crouching", true);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(dead==false){
		//Character doesnt choose direction in Jump									//If you want to choose direction in jump
			if(attacking==false){													//just delete the (jumping==false)
				if(jumping==false&&crouching==false){
					Movement();
					Attack();
					Special();
				}
				Jump();
				Crouch();
                Special();
			}
			Dead();
            Special();
            
		}
        pointsText.text = "Coins: " + poeni;
        gameover.text = poeni + " Coins";

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Esc();
        }
	}

    private void Esc()
    {
        SceneManager.LoadScene(0);
    }

	void Movement(){
		//Character Move
		float move = Input.GetAxisRaw("Horizontal");
		if(Input.GetKey(KeyCode.LeftShift)){
			//Run
			rb.velocity = new Vector2(move*speed*Time.deltaTime*3,rb.velocity.y);
			running=true;
		}else{
			//Walk
			rb.velocity = new Vector2(move*speed*Time.deltaTime,rb.velocity.y);
			running=false;
		}

		//Turn
		if(rb.velocity.x<0){
			sp.flipX=true;
		}else if(rb.velocity.x>0){
			sp.flipX=false;
		}
		//Movement Animation
		if(rb.velocity.x!=0&&running==false){
			anim.SetBool("Walking",true);
		}else{
			anim.SetBool("Walking",false);
		}
		if(rb.velocity.x!=0&&running==true){
			anim.SetBool("Running",true);
		}else{
			anim.SetBool("Running",false);
		}
	}

	void Jump(){
		//Jump
		if(Input.GetKeyDown(KeyCode.Space)&&rb.velocity.y==0){
			rb.AddForce(new Vector2(0,jumpForce));
            skociSnd.mute = false;
            skociSnd.Play();
		}
		//Jump Animation
		if(rb.velocity.y>0&&up==false){
			up=true;
			jumping=true;
			anim.SetTrigger("Up");
           
		/*}else if(rb.velocity.y<0&&down==false){
			down=true;
			jumping=true;
			//anim.SetTrigger("Down");*/
		}else if(rb.velocity.y==0&&(up==true||down==true)){
			up=false;
			down=false;
			jumping=false;
            
			anim.SetTrigger("Ground");
		}
	}

	void Attack(){																//I activated the attack animation and when the 
		//Atacking																//animation finish the event calls the AttackEnd()
		if(Input.GetKeyDown(KeyCode.C)){
			rb.velocity=new Vector2(0,0);
			anim.SetTrigger("Attack");
			attacking=true;
		}
	}

	void AttackEnd(){
		attacking=false;
	}

	void Special(){
        if (Input.GetKeyDown(KeyCode.X))
        {
            
			anim.SetBool("Special",true);
            special = true;
		}else{
			anim.SetBool("Special",false);
            special = false;
		}
	}

	void Crouch(){
		//Crouch
		if(Input.GetKey(KeyCode.DownArrow)){
			anim.SetBool("Crouching",true);
		}else{
			anim.SetBool("Crouching",false);
		}
	}

	/*void OnTriggerEnter2D(Collider2D other){							//Case of Bullet
		if(other.tag=="Enemy"){
			anim.SetTrigger("Damage");
			Hurt();
		}
	}		*/

    void OnTriggerEnter2D(Collider2D other)
    {							//Case of Bullet
        if (other.tag == "Van")
        {
            if (!special)
            {
                Destroy(life[qtdLife - 1]);
                qtdLife -= 1;
                igrac.transform.position = pozicija;
                anim.SetBool("Respawning", true);
                Invoke("Normala", 2f);
            }
        }

        if (other.tag == "Trap")
        {
            
            if (!special)
            {
                Destroy(life[qtdLife - 1]);
                qtdLife -= 1;
                igrac.transform.position = pozicija;
                anim.SetBool("Respawning", true);
                Invoke("Normala", 2f);
            }
        }

        if (other.tag == "sponPrvi" && prvicek == false)
        {

            pozicija = new Vector3(30.933f, -1.87f, 0f);
            cekpoint.Play();
            prvicek = true;

        }

        if (other.tag == "sponDrugi" && drugicek == false)
        {

            pozicija = new Vector3(54.5102f,-0.3714f,0f);
            cekpoint.Play();
            drugicek = true;
        }
        
        if (other.tag == "Novcic")
        {

            novcicSnd.mute = false;
            novcicSnd.Play();
            novcic = other;
            Destroy(novcic.gameObject);
            poeni++;

        }

        if (other.tag == "win" && winer == false)
        {
            win.Play();
            winer = true;
            Invoke("winerSi", 6f);
        }

       /* if (other.tag == "Propo")
        {
            propo = other.gameObject;
            propo.GetComponent<Animation>().Play();
            propo2 = GameObject.Find("propoDva");
            propo2.GetComponent<Animation>().Play();

        }*/
        /*
        if (Input.GetKey(KeyCode.C))
        {
            if (other.gameObject.tag == "Enemy")
            {
                Destroy(other.gameObject);
            }
        }
        */
        /*if (!Input.GetKey(KeyCode.C))
        {
            if (other.gameObject.tag == "Enemy")
            {
                anim.SetTrigger("Damage");
                Hurt();
            }
        }*/

        
        
        if (other.tag == "Enemy")
        {
            print("ddd");
            Destroy(other.gameObject);

        }

    }

    private void winerSi()
    {
        SceneManager.LoadScene(4);
    }

    void Normala()
    {
        igrac.enabled = true;
        anim.SetBool("Respawning", false);
    }

	/*void OnCollisionEnter2D(Collision2D other) {						//Case of Touch
		if(other.gameObject.tag=="Enemy"){
			anim.SetTrigger("Damage");
			Hurt();
		}
	}*/

	void Hurt(){
		if(rateOfHit<Time.time){
			rateOfHit=Time.time+cooldownHit;
			Destroy(life[qtdLife-1]);
			qtdLife-=1;
		}
	}

	void Dead(){
		if(qtdLife<=0){
            glavna.Stop();
			anim.SetTrigger("Dead");
			dead=true;
            umriSnd.mute = false;
            umriSnd.Play();
            Invoke("krajj",2f);
            Invoke("glavniMeni", 5f);
		}
	}

    private void glavniMeni(){
        SceneManager.LoadScene(0);
    }

    private void krajj()
    {
        kraj.Play();
    }

	/*public void TryAgain(){														//Just to Call the level again
		SceneManager.LoadScene(0);
	}*/
}
