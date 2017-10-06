using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PLAYER_DIRECTION
{
    P_RIGHT,
    P_UP,
    P_LEFT,
    P_DOWN,
}
public class Player_behaviour : MonoBehaviour {

    public GameObject boomerang = null;
    public bool alive = true;
    public bool p_multiboomerang = false;
    public uint p_hp_max = 3;
    public float p_speed = 5.0f;
    public bool p_with_acceleration = false;

    public PLAYER_DIRECTION p_dir_state;

    Vector3 p_direction = Vector3.zero;
    bool p_boomerang_current = false;
    uint p_hp_current;

    //Collider2D col;
	// Use this for initialization
	void Start ()
    {
        
	}
	
	// Update is called once per frame
    void FixedUpdate()
    {
      
    }

	void Update ()
    {
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
        Vector3 p_last_position = transform.position;

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
             
        p_direction = (p_last_position - transform.position).normalized;
    }

    //Throws Smilerang
    void ThrowSmilerang()
    {
        if (p_multiboomerang || !p_boomerang_current)
        {
            Smilerang_behaviour boom_behaviour = Instantiate(boomerang).GetComponent<Smilerang_behaviour>();
            boom_behaviour.AssignTarget(gameObject);

            if (p_direction != Vector3.zero)
                boom_behaviour.s_direction = -p_direction;
            else
                boom_behaviour.s_direction = new Vector3(0.0f, -1.0f);   
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

    void DirectionState()
    {
        float dir_angle = Mathf.Atan2(p_direction.y, p_direction.x);

        if(dir_angle < 45.0f || dir_angle > 315)
        {
            //RIGHT
            p_dir_state = PLAYER_DIRECTION.P_RIGHT;
        }
        else if(dir_angle < 135)
        {
            //UP
            p_dir_state = PLAYER_DIRECTION.P_UP;
        }
        else if(dir_angle < 225 )
        {
            //LEFT
            p_dir_state = PLAYER_DIRECTION.P_LEFT;
        }
        else if(dir_angle < 315 )
        {
            //DOWN
            p_dir_state = PLAYER_DIRECTION.P_DOWN;
        }

    }
}
