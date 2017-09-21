using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    //a list of powerup types
    List<GameObject> powerupTypes = new List<GameObject>();

    [Tooltip("Obstacle type 1")]
    [SerializeField]
    private GameObject obs1;

    [Tooltip("Obstacle type 2")]
    [SerializeField]
    private GameObject obs2;

    [Tooltip("Obstacle type 3")]
    [SerializeField]
    private GameObject obs3;

    [Tooltip("Powerup type 1")]
    [SerializeField]
    private GameObject pUp;

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
    private float powerTimer = 0f;//timer for powerups
    /* Specifies which power is active
     * 0:none 1:hor speed 2:vert speed 3:score mult
     */
    private int power = 0;
    private int scoreMult = 1;

    #endregion

    #region itialize
    /**
     * Adds obstacle types to the list when the game starts
     **/
    void Start () {
        if( obs1 ) obstacleTypes.Add(obs1);
        if( obs2 ) obstacleTypes.Add(obs2);
        if( obs3 ) obstacleTypes.Add(obs3);

        if( pUp ) powerupTypes.Add(pUp);
	}
    #endregion

    #region updates

    // Update is called once per frame
    void Update () {
        spawnTimer -= Time.deltaTime;

        //Spawn objects when the timer hits zero, then reset the Timer
        if (spawnTimer <= 0 ) {
            spawnObs();
            spawnPUps();
        }

        //if there is an active powerup, tick down the timer
        if( powerTimer > 0 ) powerTimer -= Time.deltaTime;
    }

    private void LateUpdate() {    

        if ( obstacles.Count > 0 ) {
            //check for collisions (w/ player and kill volume)
            detectObsCol();

            //check for deaths
            detectObsDeath();
        }

        if ( powerups.Count > 0 ) {
            //check for collisions
            detectPUpsCol();
            //check for deaths
            detectPUpsDeath();
        }

        checkScore();

        if( powerTimer <= 0 && power != 0 ) deactivatePower();

    }

    #endregion

    #region Methods

    //Spawns obstacles on the backside of the sphere
    private void spawnObs() {
        for( int i = 0; i < spawnCount; i++ ) {
            //Randomly pick an obstacle from the list of obstacle types
            GameObject obstacle = Instantiate(obstacleTypes[Random.Range(0, obstacleTypes.Count)], Vector3.zero, Quaternion.Euler(-90, Random.Range((int)-80, (int)80), 0));
            //obs is offset from a pivot object in it's heirarchy.  Rotated behind sphere and randomly along the horizontal
            //add the object to the sphere's heirarchy
            obstacle.transform.SetParent(gameObject.transform);
            //add the object to the list of obstacles
            obstacles.Add(obstacle);
        }
        //reset for next cycle
        spawnTimer = Random.Range(spawnTime - ( spawnTime / 2 ), spawnTime + ( spawnTime / 2 ));
    }

    //spawns powerups on the backside of the sphere
    private void spawnPUps() {

        //only spawn new powerups if there isn't a power active
        if( powerTimer <= 0 ) {

            //check if you should spawn a powerup (20% chance)
            float rand = Random.value;
            if( rand <= .2f ) {
                //Randomly pick an obstacle from the list of obstacle types
                GameObject pUp = Instantiate(powerupTypes[Random.Range(0, powerupTypes.Count)], Vector3.zero, Quaternion.Euler(-90, Random.Range((int)-80, (int)80), 0));
                //powerup is offset from a pivot object in it's heirarchy.  Rotated behind sphere and randomly along the horizontal
                //add the object to the sphere's heirarchy
                pUp.transform.SetParent(gameObject.transform);
                //add the object to the list of obstacles
                powerups.Add(pUp);
            }
        }
    }

    //checks for colliding obstacles
    private void detectObsCol() {
        for( int i = obstacles.Count - 1; i >= 0; i-- ) {
            //check each obstacle's collision with the player
            if( player.GetComponent<ColliderData>().CheckOverlap(obstacles[i].GetComponent<ColliderData>()) ) {
                //this obstacle has collided w/ a player, so mark it for death
                obstacles[i].GetComponent<Object_Death>().isDead = true;

                //Kill the player!
                player.GetComponent <Object_Death>().isDead = true;
            }
            if( killVolume.GetComponent<ColliderData>().CheckOverlap(obstacles[i].GetComponent<ColliderData>()) ) {
                //check to see if it's timer is sub-zero
                if( obstacles[i].GetComponent<Object_Death>().lifeDelay < 0 ) {
                    //it's both in the kill volume AND timed out, so we conk it
                    obstacles[i].GetComponent<Object_Death>().isDead = true;
                }
            }//end else if
        }//end for obstacles
    }

    //checks and removes dead obstacles
    private void detectObsDeath() {
        for( int i = obstacles.Count - 1; i >= 0; i-- ) {
            //if dead, remove from list, desroy it
            if( obstacles[i].GetComponent<Object_Death>().isDead ) {
                Destroy(obstacles[i]);
                obstacles.Remove(obstacles[i]);
            }
        }//end death check
    }

    //checks for collisions with powerUps
    private void detectPUpsCol() {
        for( int i = powerups.Count - 1; i >= 0; i-- ) {
            //check each powerups's collision with the player
            if( player.GetComponent<ColliderData>().CheckOverlap(powerups[i].GetComponent<ColliderData>()) ) {
                //this powerup has collided w/ a player, so mark it for death
                powerups[i].GetComponent<Object_Death>().isDead = true;
                activatePower();
            }
            //check powerups for expiration
            if( killVolume.GetComponent<ColliderData>().CheckOverlap(powerups[i].GetComponent<ColliderData>()) ) {
                //check to see if it's timer is sub-zero
                if( powerups[i].GetComponent<Object_Death>().lifeDelay < 0 ) {
                    //it's both in the kill volume AND timed out, so we conk it
                    powerups[i].GetComponent<Object_Death>().isDead = true;
                }
            }//end else if
        }//end for pups
    }

    //checks and removes dead obstacles
    private void detectPUpsDeath() {
        for( int i = powerups.Count - 1; i >= 0; i-- ) {
            //if dead, remove from list, desroy it
            if( powerups[i].GetComponent<Object_Death>().isDead ) {
                Destroy(powerups[i]);
                powerups.Remove(powerups[i]);
            }
        }//end death check
    }

    //checks for player death and ticks score
    private void checkScore() {
        if( !player.GetComponent<Object_Death>().isDead ) {
            Game_Manager.score += Time.deltaTime * scoreMult;
        } else {
            Game_Manager.score = (int)Game_Manager.score;
            SceneManager.LoadScene("Menu_Scene");
        }
    }

    //picks a power and starts it moving
    private void activatePower() {

        //clear the list of powerups, only one active at a time
        if( powerups.Count > 0 ) {
            for( int i = powerups.Count - 1; i >= 0; i-- ) {
                Destroy(powerups[i]);
                powerups.Remove(powerups[i]);
            }
        }

        //activate one of three powers
        float num = Random.value;

        if (num < .3 ) {
            //powerup type 1: faster horizontal player movement for a few seconds
            GetComponent<Sphere_Mover>().horSpeed *= 2;
            powerTimer = 3f;
            power = 1;
        }
        
        else if (num >=.3 && num < .6 ) {
            //powerup 2: slower vertical movement and spawn rate
            GetComponent<Sphere_Mover>().verSpeed /= 2;
            powerTimer = 3f;
            power = 2;
            spawnTime *= 2;
        } 
        
        else {
            //powerup 3 doubles the score multiplier
            scoreMult = 2;
            power = 3;
            powerTimer = 5f;
        }
    }

    private void deactivatePower() {
        
        //reset to default behaviour based on power type
        switch(  power ) {
            case 1://faster hor movement
                power = 0;
                GetComponent<Sphere_Mover>().horSpeed /= 2;
                break;
            case 2://faster ver movement
                power = 0;
                spawnTime /= 2;
                GetComponent<Sphere_Mover>().verSpeed *= 2;
                break;
            case 3://score mult
                power = 0;
                scoreMult = 1;
                break;
            case 0:
                print("Tried to deActivate a null power!");
                break;
            default:
                break;
        }
    }

    #endregion
}