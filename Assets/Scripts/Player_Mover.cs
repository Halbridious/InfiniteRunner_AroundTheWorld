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
 *  >Impliment either 1-press or timed jump system
 **/

public class Player_Mover : MonoBehaviour {

    [SerializeField]
    float jumpPower = 50f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
        //If the jump button is pressed ("Jump" is assigned in program properties)
        if(Input.GetAxisRaw("Jump") > 0) {
            
            //accelerate into the jump
            transform.position += new Vector3(0, 0, jumpPower) * Time.deltaTime;
            
            //Once they've reached the highest point of the jump, cap it
            if( transform.position.z >= 30 ) transform.position = new Vector3(0, 0, 30);

        } else {//if the button is released

            //accelerate back towards neutral position
            if(transform.position.z > 26) transform.position -= new Vector3(0, 0, jumpPower) * Time.deltaTime;

            //once in or past neutral position, set exactly to neutral
            if( transform.position.z <= 26 ) transform.position = new Vector3(0, 0, 26);
        }
	}
}
