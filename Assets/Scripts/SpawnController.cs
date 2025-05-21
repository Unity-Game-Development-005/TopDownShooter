
using UnityEngine;


public class SpawnController : MonoBehaviour
{
    // get a reference to the bounds controller script
    public BoundsController boundsController;

    // create an array of health pickup objects
    public GameObject[] healthPickup;

    // create a health pickup index to allow objects to be randomly spawned
    private int healthPickupIndex;

    // elapsed time before pickups are spawned
    public float spawnDelay;

    // the time interval before next pickup is spawned
    public float spawnInterval;



    private void Start()
    {
        InvokeRepeating(nameof(SpawnHealthPickup), spawnDelay, spawnInterval);
    }


    private void SpawnHealthPickup()
    {
        // get a random position along the 'x' axis
        float randomLocationX = Random.Range(boundsController.leftBoundary.position.x, boundsController.rightBoundary.position.x);

        // get a random position along the 'z' axis
        float randomLocationZ = Random.Range(boundsController.upperBoundary.position.z, boundsController.lowerBoundary.position.z);

        // set the new random position
        Vector3 spawnPosition = new Vector3(randomLocationX, 0.25f, randomLocationZ);

        // selected a random object to spawn
        healthPickupIndex = (Random.Range(0, healthPickup.Length));

        // spawn the object
        Instantiate(healthPickup[healthPickupIndex], spawnPosition , healthPickup[healthPickupIndex].transform.rotation);
    }


} // end of class
