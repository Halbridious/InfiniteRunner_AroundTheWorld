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
 * >>>This class originally handled this differently, using a methodology since abandoned<<<
 * 
 **/


public class Object_Death : MonoBehaviour {

    #region Variables

    [Tooltip("How long this object is immune from dying to the kill volume.  May still die to player collision")]
    public float lifeDelay = 3f;

    [Tooltip("Whether or not this object has been marked for deletion, used in collision detection.")]
    public bool isDead = false;

    #endregion

    // Update is called once per frame
    void Update () {
        lifeDelay -= Time.deltaTime;
        //this properties value will be checked elsewhere and can tick indefinitely as long as the object is in the field of play.
	}
}
