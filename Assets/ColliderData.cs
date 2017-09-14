using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * 
 * This script just checks the AABB of the object and updats the collision data for the obstacles
 * 
 **/

public class ColliderData : MonoBehaviour {

    public Vector3 maxs = Vector3.zero;
    public Vector3 mins = Vector3.zero;

    [SerializeField]
    Vector3 halfSize = Vector3.zero;

    [SerializeField]
    private GameObject obstacle;

    // Update is called once per frame
    void Update() {
        /**
         * Because we're using AABB, the obstacles have to be axis aligned.
         * This wasn't the original plan, but C'est La Vie
         * After they're rotated by the parent, we want to clear their personal rotations in world space?
         **/
        obstacle.transform.rotation = Quaternion.identity;

        calcEdges();               
    }

    void calcEdges() {
        mins = obstacle.transform.position - halfSize;
        maxs = obstacle.transform.position + halfSize;
    }

    public bool CheckOverlap( ColliderData other ) {

        if( mins.x > other.maxs.x ) return false;
        if( maxs.x < other.mins.x ) return false;

        if( mins.y > other.maxs.y ) return false;
        if( maxs.y < other.mins.y ) return false;

        if( mins.z > other.maxs.z ) return false;
        if( maxs.z < other.mins.z ) return false;

        return true;
    }
    /**
    public void SetCollisionWith( bool isColliding, ColliderAABB other ) {
        if( isColliding ) {
            if( !currentOverlaps.Contains(other) ) {
                currentOverlaps.Add(other);

                OnCollisionStart();
            }
        } else {
            if( currentOverlaps.Contains(other) ) {
                currentOverlaps.Remove(other);

                OnCollisionEnd();
            }
        }
    }
    **/
}

