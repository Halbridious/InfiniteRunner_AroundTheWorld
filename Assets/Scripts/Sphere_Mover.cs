using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * SPHERE MOVER is a BEHAVIOUR that simulates the movement laterally for the player
 * Players will change lanes by hitting left or right, giving the illusion of sliding side to side.
 * Sphere also constantly rotates directly "towards" the player
 *  >Player changing lanes will, in turn, change angle of approaching obstacles
 * 
 * TODO:
 * accelerate and decelerate lateral movement
 **/


public class Sphere_Mover : MonoBehaviour {

    [SerializeField]
    private float horSpeed = 20f;

    [SerializeField]
    private float verSpeed = 20f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        transform.RotateAround(Vector3.zero, new Vector3(1, 0, 0), verSpeed * Time.deltaTime);

        float move = Input.GetAxisRaw("Horizontal");

        if (move > 0 ) {

            transform.RotateAround(Vector3.zero, new Vector3(0, 1, 0), horSpeed * Time.deltaTime);

        } else if (move < 0 ) {
            transform.RotateAround(Vector3.zero, new Vector3(0, 1, 0), -horSpeed * Time.deltaTime);
        } else {
            //no input, just calculate friction
        }

	}
}
