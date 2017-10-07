using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_behaviour : MonoBehaviour {

    public GameObject Target;
    Vector3 offset;

	// Use this for initialization
	void Start () {
        offset = Target.transform.position - transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = Target.transform.position - offset;
	}
}
