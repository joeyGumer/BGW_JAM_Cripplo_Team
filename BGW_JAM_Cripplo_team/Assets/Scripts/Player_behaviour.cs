using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_behaviour : MonoBehaviour {

    public GameObject boomerang = null;
    public bool alive = true;
    public bool p_multiboomerang = false;
    public uint p_hp_max = 3;
    public float p_speed = 5.0f;
    public bool p_with_acceleration = false;

    bool p_boomerang_current = false;
    uint p_hp_current;
    //Collider2D col;
	// Use this for initialization
	void Start ()
    {
        
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
            ThrowSmilerang();
        }
    }

 
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "smilerang")
        {
            if (coll.gameObject.GetComponent<Smilerang_behaviour>().s_collidable)
            {
                GetHurt();
                Destroy(coll.gameObject);

            }

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
        if (p_multiboomerang || !p_boomerang_current)
        {
            Smilerang_behaviour boom_behaviour = Instantiate(boomerang).GetComponent<Smilerang_behaviour>();
            boom_behaviour.AssignTarget(gameObject);
            p_boomerang_current = true;
        }
    }

    //Activate when the player gets hurts
    void GetHurt()
    {
        p_hp_current--;
        //Reduce the UI hp

        if(p_hp_current <= 0)
        {
            //Player is dead
            alive = false;
        }

        if(!p_multiboomerang)
        {
            p_boomerang_current = false;
        }
    }
}
