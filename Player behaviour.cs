using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerbehaviour : MonoBehaviour {

    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        HandleInput();
	}

    void HandleInput()
    {

        float mov_horizontal = Input.GetAxis("horizontal");
        float mov_vertical = Input.GetAxis("vertical");

        Vector2 mov;

        mov.x = mov_horizontal;
        mov.y = mov_vertical;

        transform.Translate(mov);
    }
}
