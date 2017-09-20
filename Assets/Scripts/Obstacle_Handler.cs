using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * 
 * The obstacle spawner spawns a series of objects over time
 * It also checks each obstacle for a death tag to remove it
 * 
 * Obstacles are spawned at the "back" of the sphere (opposite the camera) and are parented to sphere
 * They roll with the sphere
 * 
 * Obstacles handle their deletion condition's by themselves, but are deleted by the handler
 * 
 * Obstacles are currently spawned using a timer system
 *  The base time can be adjusted, and they'll spawn randomly based on that base time
 * 
 * >TODO: Can we create a list in the editor to pass in obstacle types instead of individual slots?
 **/



public class Obstacle_Handler : MonoBehaviour {

    #region variables

    //a list of the kinds of archetypes of obstacles
    List<GameObject> obstacleTypes = new List<GameObject>();
    //a list of the active obstacles in the game
    List<GameObject> obstacles = new List<GameObject>();
    //a list of active powerups
    List<GameObject> powerups = new List<GameObject>();
    
    [Tooltip("Obstacle type 1")]
    [SerializeField]
    private GameObject obs1;

    [Tooltip("Obstacle type 2")]
    [SerializeField]
    private GameObject obs2;

    [Tooltip("The Player Object")]
    [SerializeField]
    private GameObject player;

    [Tooltip("A volume which marks objects for death upon entry")]
    [SerializeField]
    private GameObject killVolume;

    [Tooltip("How long it should take to spawn obstacles, on average.  " +
        "Actual time is randomized between half-time and time-and-a-half")]
    [SerializeField]
    private float spawnTime = 2f;

    [Tooltip("How many obstacles are spawned each time")]
    [SerializeField]
    private int spawnCount = 2;

    private float spawnTimer = 0f;//the actual counter for the spawn timer

    #endregion

    #region itialize
    /**
     * Adds obstacle types to the list when the game starts
     **/
    void Start () {
        if(obs1) obstacleTypes.Add(obs1);
        if(obs2) obstacleTypes.Add(obs2);
	}
    #endregion

    #region update
    // Update is called once per frame
    void Update () {
        spawnTimer -= Time.deltaTime;

        //Spawn objects when the timer hits zero, then reset the Timer
        if (spawnTimer <= 0 ) {
            for( int i = 0; i < spawnCount; i++ ) {
                //Randomly pick an obstacle from the list of obstacle types
                GameObject obstacle = Instantiate(obstacleTypes[Random.Range(0, obstacleTypes.Count)], Vector3.zero, Quaternion.Euler(-90, Random.Range((int)-90, (int)90), 0));
                //obs is offset from a pivot object in it's heirarchy.  Rotated behind sphere and randomly along the horizontal
                //add the object to the sphere's heirarchy
                obstacle.transform.SetParent(gameObject.transform);
                //add the object to the list of obstacles
                obstacles.Add(obstacle);
            }
            //reset for next cycle
            spawnTimer = Random.Range(spawnTime-(spawnTime/2), spawnTime + (spawnTime/2));
            print(spawnTimer);
        }

        /**
         * >TODO: Repeat above loop for powerups
         **/
    }

    private void LateUpdate() {
        if (obstacles.Count > 0 ) {
            //check obstacles for collisions (w/ player and kill volume)
            for( int i = obstacles.Count - 1; i >= 0; i-- ) {
                //check each obstacle's collision with the player
                if( player.GetComponent<ColliderData>().CheckOverlap(obstacles[i].GetComponent<ColliderData>()) ) {
                    //this obstacle has collided w/ a player, so mark it for death
                    obstacles[i].GetComponent<Object_Death>().isDead = true;

                    //> TODO: KILL THE PLAYER TOO!

                }

                if( killVolume.GetComponent<ColliderData>().CheckOverlap(obstacles[i].GetComponent<ColliderData>()) ) {
                    //check to see if it's timer is sub-zero
                    if( obstacles[i].GetComponent<Object_Death>().lifeDelay < 0 ) {
                        //it's both in the kill volume AND timed out, so we conk it
                        obstacles[i].GetComponent<Object_Death>().isDead = true;
                    }
                }//end else if

            }//end for obstacles

            //check for obstacle death
            for( int i = obstacles.Count - 1; i >= 0; i-- ) {
                //if dead, remove from list, desroy it
                if( obstacles[i].GetComponent<Object_Death>().isDead ) {
                    Destroy(obstacles[i]);
                    obstacles.Remove(obstacles[i]);
                }
            }//end death check

            //check player collisions with powerups
        }
    }

    #endregion

}
