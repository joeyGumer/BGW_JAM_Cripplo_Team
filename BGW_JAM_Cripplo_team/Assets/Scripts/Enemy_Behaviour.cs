using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Enemy_Type
{
    ENEMY_STATIC,
    ENEMY_KINEMATIC,
    ENEMY_RANDOM
}

public enum Enemy_direction
{
    E_RIGHT,
    E_LEFT,
    E_UP,
    E_DOWN,
}

public enum Enemy_state
{
    E_IDLE,
    E_WALK,
    E_HAPPY,
}

public class Enemy_Behaviour : MonoBehaviour
{

    public Enemy_Type e_type;
    public Enemy_direction e_dir_state;
    public Enemy_state e_state = Enemy_state.E_IDLE;

    public float e_speed = 1.0f;
    public float e_max_distance = 0;
    public float e_target_min_distance = 0.2f;
    public Vector3 e_dir = new Vector3(0.0f, 1.0f);

    Vector3 e_start_position;
    Vector3 e_target_position;

    // Use this for initialization
    void Start()
    {
        ChangeObjective();
    }

    // Update is called once per frame
    void Update()
    {
        DirectionState();
        Movement();
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (e_state != Enemy_state.E_HAPPY)
        {
            if (coll.tag == "smilerang")
            {
                //TODO: temporal measure
                StateMachine(Enemy_state.E_HAPPY);

                gameObject.GetComponent<Collider2D>().isTrigger = true;

                //Count enemy to win
                GameObject.Find("Main Game").GetComponent<Game_cycle>().KillEnemy();
            }
        }

    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        string tag = coll.collider.tag;

        if (tag == "wall" || tag == "item" || tag == "enemy")
        {
            ChangeDirection();
            ChangeObjective();
        }
    }

    void Movement()
    {
        if (e_state != Enemy_state.E_HAPPY)
        {
            switch (e_type)
            {
                case Enemy_Type.ENEMY_STATIC:
                    break;
                case Enemy_Type.ENEMY_KINEMATIC:
                    MovementKinematic();
                    break;
                case Enemy_Type.ENEMY_RANDOM:
                    MovementRandom();
                    break;

            }
        }
    }

    void MovementKinematic()
    {
        transform.position += e_dir * e_speed;

        Vector3 distance = e_target_position - transform.position;
        if (distance.magnitude < e_target_min_distance)
        {
            e_dir = -e_dir;

            ChangeObjective();
        }
    }

    void MovementRandom()
    {
        transform.position += e_dir * e_speed;

        Vector3 distance = e_target_position - transform.position;
        if (distance.magnitude < e_target_min_distance)
        {

            ChangeDirection();
            ChangeObjective();
        }
    }



    void ChangeObjective()
    {
        StateMachine(Enemy_state.E_WALK);
        e_start_position = transform.position;
        e_target_position = e_start_position + e_dir * e_max_distance;
    }

    void ChangeDirection()
    {
        int rand = Random.Range(0, 3);
        switch (rand)
        {
            case 0:
                e_dir = new Vector3(-1.0f, 0.0f);
                break;
            case 1:
                e_dir = new Vector3(1.0f, 0.0f);
                break;
            case 2:
                e_dir = new Vector3(0.0f, -1.0f);
                break;
            case 3:
                e_dir = new Vector3(0.0f, 1.0f);
                break;
        }
    }

    void DirectionState()
    {
        if (e_dir != Vector3.zero)
        {
            float dir_angle = Mathf.Atan2(e_dir.y, e_dir.x) * Mathf.Rad2Deg;


            if (dir_angle < -135.0f || dir_angle >= 135.0f)
            {
                //RIGHT
                e_dir_state = Enemy_direction.E_RIGHT;
            }
            else if (dir_angle < 45.0F && dir_angle >= -45.0f)
            {
                //LEFT
                e_dir_state = Enemy_direction.E_RIGHT;
            }
            else if (dir_angle < 135.0f && dir_angle >= 45.0f)
            {
                //DOWN
                e_dir_state = Enemy_direction.E_RIGHT;
            }
            else if (dir_angle < -45.0f && dir_angle >= -135.0f)
            {
                //UP
                e_dir_state = Enemy_direction.E_RIGHT;
            }
        }
        else
        {
            e_dir_state = Enemy_direction.E_RIGHT;
        }

    }

    public void StateMachine(Enemy_state state)
    {
        e_state = state;
    }
}
