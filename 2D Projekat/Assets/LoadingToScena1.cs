using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class LoadingToScena1 : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Invoke("Level1",2.5f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Level1()
    {
        Application.LoadLevel(2);
    }
}
