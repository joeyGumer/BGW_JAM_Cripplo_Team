﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smilerang_behaviour : MonoBehaviour {

    public GameObject target = null;
    public float s_base_max_velocity = 10.0f;
    public float s_item_speed_change = 0.1f;
    public float s_acceleration = 5.0f;
    public float s_rot_velocity = 1.0f;
    public Vector2 s_direction = new Vector2( 0.0f, 1.0f );

    public float s_max_velocity;
    public bool s_collidable = false;
    Vector2 s_movement = Vector2.zero;

	// Use this for initialization
	void Start () {

        s_max_velocity = s_base_max_velocity;


        s_movement = s_direction * s_max_velocity;
        target = GameObject.Find("Player");
        transform.position = target.transform.position; 
	}
	
	// Update is called once per frame
	void Update () {

        transform.RotateAroundLocal(new Vector3(0.0f, 0.0f, 1.0f), s_rot_velocity * Time.deltaTime);
        Seek();
        Move();
	}
    void OnTriggerExit2D(Collider2D coll)
    {
        if(coll.tag == "Player")
            s_collidable = true;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "wall")
        {
            /* ContactPoint2D coll_point = coll.contacts[0];
             Vector2 normal = coll_point.normal;
             CalculateReflexion(normal);*/
            s_movement = -s_movement;

        }
    }

    /*void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.tag == "wall")
        {

           ContactPoint2D coll_point =  coll.contacts[0];
            Vector2 normal = coll_point.normal;
            CalculateReflexion(normal);
        }
    }*/

    void Move()
    {
        if (s_movement.magnitude > s_max_velocity)
            s_movement = s_movement.normalized * s_max_velocity;

        Vector3 vel = new Vector3(s_movement.x, s_movement.y);
        transform.position += vel;
    }


    bool Seek()
    {
        bool ret = false;

        s_direction = target.transform.position - transform.position;
        s_direction = s_direction.normalized * s_acceleration;

        s_movement += s_direction;

        return ret;
    }
    
    public void AssignTarget(GameObject go)
    {
        target = go;
    }

    void CalculateReflexion(Vector2 normal)
    {
        Vector2 dir = s_movement.normalized;

        float angle_dir = Mathf.Atan2(dir.y, dir.x);
        float angle_normal = Mathf.Atan2(normal.y, normal.x);

        float angle_reflex = 2 * angle_normal - angle_dir;

        float reflex_x = Mathf.Cos(angle_reflex) * s_movement.magnitude;
        float reflex_y = Mathf.Sin(angle_reflex) * s_movement.magnitude;

        s_movement = new Vector2(reflex_x, reflex_y);
    }

    public void ChangeMaxSpeed(bool slow)
    {
        float sp = s_item_speed_change;
        if (slow)
            sp = -sp;

        s_max_velocity += sp;
        //Timer?

    }
}
