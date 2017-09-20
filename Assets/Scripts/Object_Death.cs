using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * 
 * This behaviour attempts to determine if the object it's attatched to has expired
 * 
 * Objects spawn with a delay timer that must expire before they can be marked dead (to give them time to enter the field of play)
 * 
 * After the timer, objects can be marked dead by the collision handler
 * Objects can die by collision w/ a player, or by collision w/ a kill volume after the timer expires
 * 
 **/


public class Object_Death : MonoBehaviour {

    public float lifeDelay = 3f;

    public bool isDead = false;
	
	// Update is called once per frame
	void Update () {
        lifeDelay -= Time.deltaTime;
        //this properties value will be checked elsewhere and can tick indefinitely as long as the object is in the field of play.
	}
}
