using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_behaviour : MonoBehaviour {

    public GameObject Target;
    Vector3 offset;
    float speed = 0.9f;

    // Use this for initialization
    void Start()
    {
        offset = Target.transform.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 new_pos = Target.transform.position - offset;
        transform.position = Vector3.Lerp(new_pos, transform.position, speed);
    }
}
