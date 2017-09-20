using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * 
 * This script just checks the AABB of the object and updats the collision data for the obstacles
 * 
 * Script also contains the method functionality for checking collisions
 * 
 **/

public class ColliderData : MonoBehaviour {

    public Vector3 maxs = Vector3.zero;
    public Vector3 mins = Vector3.zero;

    [SerializeField]
    Vector3 halfSize = Vector3.zero;

    [SerializeField]
    private GameObject hull;

    // Update is called once per frame
    void Update() {
        /**
         * Because we're using AABB, the obstacles have to be axis aligned.
         * This wasn't the original plan, but C'est La Vie
         * After they're rotated by the parent, we want to set their personal rotation in world space to zeros.
         **/
        hull.transform.rotation = Quaternion.identity;

        calcEdges();               
    }

    /**
     * Calculates where the edges of the object are by finding the upper left and lower right vertices.
     * Stores these 2 points in two Vector3s
     **/
    void calcEdges() {
        mins = hull.transform.position - halfSize;
        maxs = hull.transform.position + halfSize;
    }

    /**
     * Determines if an object is overlapping with this collider or not.
     * Returns true if colliding.
     **/
    public bool CheckOverlap( ColliderData other ) {

        if( mins.x > other.maxs.x ) return false;
        if( maxs.x < other.mins.x ) return false;

        if( mins.y > other.maxs.y ) return false;
        if( maxs.y < other.mins.y ) return false;

        if( mins.z > other.maxs.z ) return false;
        if( maxs.z < other.mins.z ) return false;

        return true;
    }
}

