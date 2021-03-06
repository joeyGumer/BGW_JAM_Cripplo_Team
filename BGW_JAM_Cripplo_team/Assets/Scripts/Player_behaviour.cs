﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PLAYER_DIRECTION
{
    P_RIGHT,
    P_UP,
    P_LEFT,
    P_DOWN,
}

public enum PLAYER_STATE
{
    P_IDLE,
    P_WALK,
    P_ATTACK,
    P_HURT,
    P_DIE,
}

public class Player_behaviour : MonoBehaviour {

    public GameObject item = null;
    public GameObject boomerang = null;
    public bool alive = true;
    public bool p_multiboomerang = false;
    public uint p_hp_max = 3;
    public float p_speed = 5.0f;
    public bool p_with_acceleration = false;
    public uint p_boomerang_limit = 1;
    public float p_smile_effect_time = 5.0f;
    public float p_double_effect_time =  1.0f;
    public List<buff> buffs;


    public PLAYER_DIRECTION p_dir_state;
    public PLAYER_STATE p_state;

    Vector3 p_direction = Vector3.zero;
    public int p_boomerang_current = 0;
    public float p_smile_timer = 0.0f;
    public bool p_smile_timer_on = false;
    public float p_limit_timer = 0.0f;
    public bool p_limit_timer_on = false;
    public uint p_hp_current;

    //Collider2D col;
	// Use this for initialization
	void Start ()
    {
        p_hp_current = p_hp_max;

    }
	
	// Update is called once per frame
    void FixedUpdate()
    {
      
    }

	void Update ()
    {
        if (p_smile_timer_on)
        {
            p_smile_timer -= Time.deltaTime;

            if (p_smile_timer <= 0.0f)
            {
                p_smile_timer_on = false;
                item.GetComponent<Item_behaviour>().SmileReset();
            }
        }

        if(p_limit_timer_on)
        {
            p_limit_timer -= Time.deltaTime;

            if(p_limit_timer <= 0.0f)
            {
                ThrowSmilerang();
                if(p_boomerang_current < p_boomerang_limit)
                {
                    p_limit_timer = p_double_effect_time;
                }
                else
                {
                    p_limit_timer_on = false;
                }
            }
        }

        HandleInput();
	}

    void HandleInput()
    {
        Movement();
        DirectionState();

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

    void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.collider.tag == "enemy")
        {
            GetHurt();
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

        if (mov != Vector2.zero)
        {
            StateMachine(PLAYER_STATE.P_WALK);
            transform.Translate(mov);
        }
        else
        {
            StateMachine(PLAYER_STATE.P_IDLE);
        }
             
        p_direction = (p_last_position - transform.position).normalized;
    }

    //Throws Smilerang
    public void ThrowSmilerang()
    {
        if (p_multiboomerang || p_boomerang_current < p_boomerang_limit)
        {
            Smilerang_behaviour boom_behaviour = Instantiate(boomerang).GetComponent<Smilerang_behaviour>();
            boom_behaviour.AssignTarget(gameObject);

            if (p_direction != Vector3.zero)
                boom_behaviour.s_direction = -p_direction;
            else
                boom_behaviour.s_direction = new Vector3(0.0f, -1.0f);   
            p_boomerang_current++;

        }
    }

    //Activate when the player gets hurts
    void GetHurt()
    {
        StateMachine(PLAYER_STATE.P_HURT);

        p_hp_current--;
        //Reduce the UI hp

        if(p_hp_current <= 0)
        {
            //Player is dead
            StateMachine(PLAYER_STATE.P_DIE);
            alive = false;

            //Wait some seconds?
            GameObject.Find("Main Game").GetComponent<Game_cycle>().FinishGame();
        }

        if(!p_multiboomerang)
        {
            p_boomerang_current--;
            if(p_boomerang_current < 0)
            {
                p_boomerang_current = 0;
            }

            if(p_boomerang_limit > 1)
            {
                p_boomerang_limit--;
            }
        }
    }



    void DirectionState()
    {
        if (p_direction != Vector3.zero)
        {
            float dir_angle = Mathf.Atan2(p_direction.y, p_direction.x) * Mathf.Rad2Deg;


            if (dir_angle < -135.0f || dir_angle >= 135.0f)
            {
                //RIGHT
                p_dir_state = PLAYER_DIRECTION.P_RIGHT;
            }
            else if (dir_angle < 45.0F && dir_angle >= -45.0f)
            {
                //LEFT
                p_dir_state = PLAYER_DIRECTION.P_LEFT;
            }
            else if (dir_angle < 135.0f && dir_angle >= 45.0f)
            {
                //DOWN
                p_dir_state = PLAYER_DIRECTION.P_DOWN;
            }
            else if (dir_angle < -45.0f && dir_angle >= -135.0f )
            {
                //UP
                p_dir_state = PLAYER_DIRECTION.P_UP;
            }
        }
        else
        {
            p_dir_state = PLAYER_DIRECTION.P_DOWN;
        }

    }

    //Item effects
    public void DoubleLimit()
    {
        p_boomerang_limit *= 2;

        GameObject.Find("Player").GetComponent<Player_behaviour>().ThrowSmilerang();
        p_limit_timer_on = true;
        p_limit_timer = p_double_effect_time;

        //Time?
    }

    void IterateBuffs()
    {
        //Reset
        p_boomerang_limit = 1;
        item.GetComponent<Item_behaviour>().SmileReset();
        
        //Iterate buffs

        //Add buffs

        
    }

    //Smile timer
    public void StartSmileTimer()
    {
        p_smile_timer_on = true;
        p_smile_timer = p_smile_effect_time;
    }

    public void StateMachine(PLAYER_STATE state)
    {
        p_state = state;
        switch (p_state)
        {
            case PLAYER_STATE.P_IDLE:
                break;
            case PLAYER_STATE.P_WALK:
                break;
            case PLAYER_STATE.P_ATTACK:
                break;
            case PLAYER_STATE.P_HURT:
                break;
            case PLAYER_STATE.P_DIE:
                break;
        }

        
    }
}
