using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene_Transitions : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void MenuToNewGame(string scene)
    {
        Application.LoadLevel(scene);
    }


    public void QuitGame()
    {
        Application.Quit();
    }
}
