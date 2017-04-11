using UnityEngine;
using System.Collections;

public class datas : MonoBehaviour {

    public GameObject ga;
    public int poeni;
    public bool dalje = false;
    Fox_Move enemyScript;

    //
	void Start () {
        enemyScript = ga.GetComponent<Fox_Move>();
	}
	
	// Update is called once per frame
	void Update () {
        poeni = enemyScript.poeni;
	}

    void Awake()
    {

        // Do not destroy this game object:

        DontDestroyOnLoad(this);

    } 
}
