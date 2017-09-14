using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * 
 * This behaviour attempts to determine if the obstacle object it's attatched to should be destroyed
 * 
 * Obstacles are given an initial lifetime to move from spawn into the viewing area
 * 
 * After this, they remain active until they disappear off the players view
 * 
 * *NOTE* The current version of this uses the obstacle's renderer's "isVisible" property.
 * This property checks visiblity among ALL cameras active - including the scene render panel.
 * Align scene to the camera, close the scene, or open as a standalone to avoid this feature-bug
 * 
 * 
 * TODO: This isn't working as intended.  We're going to create a kill-field using the colliders at another time
 **/


public class OutOfSight_OutOfMind : MonoBehaviour {

    [SerializeField]
    private GameObject obj;

    [SerializeField]
    private float lifeDelay = 3f;

    [SerializeField]
    private bool isDead = false;
    
    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if ( lifeDelay > 0f ) {
            lifeDelay -= Time.deltaTime;
        }

        if ( lifeDelay <= 0f ) {
            if( !obj.GetComponent<MeshRenderer>().isVisible ) {
                //destoy the object
                isDead = true;
            }
        }
	}
}
