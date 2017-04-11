using UnityEngine;
using System.Collections;

public class loadingLevel2 : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Invoke("Level2", 2.5f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void Level2()
    {
        
        Application.LoadLevel(6);
    }
}
