using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class pao : MonoBehaviour {

    private GameObject[] life;
    private int qtdLife;
    private Animator anim;
    private Vector3 pozicija = new Vector3(-7.53f, 1.33f,0f);
    private Collider2D igrac;
	// Use this for initialization
	void Start () {
        life = GameObject.FindGameObjectsWithTag("Life");
        qtdLife = life.Length;
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            igrac = other;
            print("Box went through!");
            Destroy(life[qtdLife - 1]);
            qtdLife -= 1;
            //Invoke("Respawn", 2.0f);
            igrac.transform.position = pozicija;
            anim.SetBool("Respawning",true);
            
        }
        //Invoke("Respawn",2f);
    }

    /*void Respawn()
    {
        anim.Play("Idle");
        
    }*/
}
