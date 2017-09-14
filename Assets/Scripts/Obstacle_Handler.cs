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
 **/



public class Obstacle_Handler : MonoBehaviour {

    List<GameObject> obstacleTypes = new List<GameObject>();
    List<GameObject> obstacles = new List<GameObject>();

    [SerializeField]
    private GameObject obs1;

    [SerializeField]
    private GameObject obs2;

    [SerializeField]
    private float spawnTime = 2f;

    private float spawnTimer = 0f;

	// Use this for initialization
	void Start () {
        if(obs1) obstacleTypes.Add(obs1);
        if(obs2) obstacleTypes.Add(obs2);
	}
	
	// Update is called once per frame
	void Update () {
        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0 ) {
            //randomly choose which obstacle and spawn it.
            //obstacles have an anchor point in the center of the sphere, and are randomly rotated around that point.
            for( int i = 0; i < 3; i++ ) {
                GameObject obstacle = Instantiate(obstacleTypes[Random.Range(0, obstacleTypes.Count)], Vector3.zero, Quaternion.Euler(-90, Random.Range((int)-90, (int)90), 0));
                obstacle.transform.SetParent(gameObject.transform);
                obstacles.Add(obstacle);
            }
            //reset for next cycle
            spawnTimer = Random.Range(spawnTime-(spawnTime/2), spawnTime + (spawnTime/2));
            print(spawnTimer);
        }

        //check for death

	}
}
