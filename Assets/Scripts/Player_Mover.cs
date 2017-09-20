using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * PLAYER MOVER is a BEHAVIOUR that allows the player to "Jump"
 * Players jump using the JUMP key
 * Players can NOT move during a jump!
 *  >Currently, moving is done on the sphere.  
 *    
 *  TODO:
 *  >Acceleration on jump
 *  >Tuning
 **/

public class Player_Mover : MonoBehaviour {

    [SerializeField]
    private float jumpPower = 50f;

    [SerializeField]
    private float jumpTime = 2f;

    [SerializeField]
    private float jumpTimer = 0f;

    [SerializeField]
    private bool isGrounded = true;//not currently used for anything

    //called on startup
    private void Start() {
        jumpTimer = jumpTime;
    }

    // Update is called once per frame
    void Update () {
        
        //If the jump button is pressed ("Jump" is assigned in program properties)
        if(Input.GetAxisRaw("Jump") > 0 && jumpTimer > 0) {

            //toggle jumping status
            isGrounded = false;
            
            //accelerate into the jump
            transform.position += new Vector3(0, 0, jumpPower) * Time.deltaTime;

            //tick down the jump timer
            jumpTimer -= Time.deltaTime;

        } else {//if the button is released

            //accelerate back towards neutral position
            if(transform.position.z > 26) transform.position -= new Vector3(0, 0, jumpPower) * Time.deltaTime;

            //once in or past neutral position, set exactly to neutral
            if( transform.position.z <= 26 ) {
                transform.position = new Vector3(0, 0, 26);
                //toggle jumping status
                isGrounded = true;
                //reset timer
                jumpTimer = jumpTime;
            }
        }
	}
}
