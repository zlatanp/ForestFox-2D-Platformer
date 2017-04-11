using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class dugmici : MonoBehaviour {

   

    public void ButtonPlay()
    {
        SceneManager.LoadScene(1);
    }

   
    public void ButtonKontrole()
    {
        SceneManager.LoadScene(3);
    }


    public void ButtonExit()
    {
        Application.Quit();
    }

    public void Back()
    {
        SceneManager.LoadScene(0);
    }

    public void NextLVL()
    {
        SceneManager.LoadScene(5);
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Play()
    {
    }

    void DugmeKontrole()
    {
    }

    void DugmeExit()
    {
        Application.Quit();
    }
}
