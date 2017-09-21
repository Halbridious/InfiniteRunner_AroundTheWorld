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

    #region variables

    [Tooltip("How quickly the player moves(sphere rotates) laterally")]
    [SerializeField]
    private float horSpeed = 20f;

    [Tooltip("How quickly the sphere moves/rotates vertically")]
    [SerializeField]
    private float verSpeed = 20f;

    #endregion
	
	// Update is called once per frame
	void Update () {

        //rotate the sphere vertically, simulating the player running "up"
        transform.RotateAround(Vector3.zero, new Vector3(1, 0, 0), verSpeed * Time.deltaTime);
        
        //Get player input (defined in unity properties)
        float move = Input.GetAxisRaw("Horizontal");

        //if input is positive the player wants to move right
        if (move > 0 ) {

            //rotate the sphere around a vertical axis at the origin, clockwise
            transform.RotateAround(Vector3.zero, new Vector3(0, 1, 0), horSpeed * Time.deltaTime);

        } else if (move < 0 ) {//if input is negative, we go left
            //rotate sphere around a vertical axis at the origin, counterclockwise
            transform.RotateAround(Vector3.zero, new Vector3(0, 1, 0), -horSpeed * Time.deltaTime);
        } else {
            //TODO: no input, just calculate friction (requires acceleration)
        }

	}
}
