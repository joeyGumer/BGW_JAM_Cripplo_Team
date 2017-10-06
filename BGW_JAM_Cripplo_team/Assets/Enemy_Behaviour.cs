using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Enemy_Type
{
    ENEMY_STATIC,
    ENEMY_KINEMATIC,
    ENEMY_RANDOM
}

public class Enemy_Behaviour : MonoBehaviour {

    public Enemy_Type e_type;
    public float e_speed = 1.0f;
    public float e_max_distance = 0;
    public float e_target_min_distance = 0.2f;
    public Vector3 e_dir = new Vector3(0.0f, 1.0f);

    Vector3 e_start_position;
    Vector3 e_target_position;

	// Use this for initialization
	void Start ()
    {
        ChangeObjective();
    }
	
	// Update is called once per frame
	void Update ()
    {
        Movement();
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.tag == "smilerang")
        {
            //TODO: temporal measure
            Destroy(gameObject);
        }
        
    }
    void Movement()
    {
        switch(e_type)
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
            int rand = Random.Range(0, 3);
            switch(rand)
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

            ChangeObjective();
        }
    }



    void ChangeObjective()
    {
        e_start_position = transform.position;
        e_target_position = e_start_position + e_dir * e_max_distance;
    }
}
