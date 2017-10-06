using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_behaviour : MonoBehaviour {

    public GameObject boomerang = null;
    public float p_speed = 5.0f;
    public bool p_with_acceleration = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        HandleInput();
	}

    void HandleInput()
    {
        Movement();

        if (Input.GetKeyDown("space"))
        {
            Instantiate(boomerang);
        }
    }

    //Handles player movement
    void Movement()
    {
        float mov_horizontal = 0.0f, mov_vertical = 0.0f, m = 0.0f;

        if (p_with_acceleration)
        {
            mov_horizontal = Input.GetAxis("Horizontal");
            mov_vertical = Input.GetAxis("Vertical");
        }
        else
        {
            m = Input.GetAxis("Horizontal");
            if (m != 0.0f)
            {
                if (m > 0.0f)
                    mov_horizontal = p_speed;
                else
                    mov_horizontal = -p_speed;
            }

            m = Input.GetAxis("Vertical");
            if (m != 0.0f)
            {
                if (m > 0.0f)
                    mov_vertical = p_speed;
                else
                    mov_vertical = -p_speed;
            }
        }

        float sp = p_speed / 20.0f;
        Vector2 mov;

        mov.x = mov_horizontal * sp;
        mov.y = mov_vertical * sp;

        transform.Translate(mov);
    }

    //Throws Smilerang
    void ThrowSmilerang()
    {
        Smilerang_behaviour boom_behaviour = Instantiate(boomerang).GetComponent<Smilerang_behaviour>();
        boom_behaviour.AssignTarget(gameObject);
    }
}
