using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smilerang_behaviour : MonoBehaviour {

    public GameObject target = null;
    public float s_max_velocity = 10.0f;
    public float s_acceleration = 5.0f;
    public Vector2 s_direction = new Vector2( 0.0f, 1.0f );

    Vector2 s_movement = Vector2.zero;

	// Use this for initialization
	void Start () {
        s_movement = s_direction * s_max_velocity;
	}
	
	// Update is called once per frame
	void Update () {
        Seek();
        Move();
	}

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
    
}
