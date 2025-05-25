
using System.Collections.Generic;
using UnityEngine;


public class SpawnController : MonoBehaviour
{
    // create a singleto for the spawn controller
    public static SpawnController spawnController;

    // create an array of health pickup objects
    public GameObject[] healthPickup;

    // array to store objects spawned
    private List<GameObject> spawnedObjectsList;

    // create a health pickup index to allow objects to be randomly spawned
    private int healthPickupIndex;

    // check to see if we can spawn
    private bool canSpawn;

    // elapsed time before pickups are spawned
    public float spawnDelay;

    // the time interval before next pickup is spawned
    public float spawnInterval;



    private void Awake()
    {
        if (spawnController == null)
        {
            spawnController = this;
        }

        else if (spawnController != this)
        {
            Destroy(this);
        }
    }


    private void Start()
    {
        Initialise();
    }


    private void Update()
    {
        CheckPlayerHealth();
    }


    private void Initialise()
    {
        spawnedObjectsList = new  List<GameObject>();

        canSpawn = false;
    }


    private void CheckPlayerHealth()
    {
        // if the game is running
        if (!GameController.gameController.gameOver)
        {
            // check if we can spawm a pickup
            if (canSpawn)
            {
                // if not, then return
                return;
            }

            // otherwise
            else
            {
                // check until player's health or ammo values are less than their maximum values
                if (Player1Controller.player1Controller.currentHealth < GameController.MAXIMUM_HEALTH ||

                    Player1Controller.player1Controller.currentAmmo < GameController.MAXIMUM_AMMO     ||

                    Player2Controller.player2Controller.currentHealth < GameController.MAXIMUM_HEALTH ||

                    Player2Controller.player2Controller.currentAmmo < GameController.MAXIMUM_AMMO)
                {
                    // then start spawning
                    InvokeRepeating(nameof(SpawnPickup), spawnDelay, spawnInterval);

                    // set the can spawn flag
                    canSpawn = true;
                }
            }
        }
    }


    private void SpawnPickup()
    {
        // get a random position along the 'x' axis
        float randomLocationX = Random.Range(BoundsController.boundsController.leftBoundary.position.x, BoundsController.boundsController.rightBoundary.position.x);

        // get a random position along the 'z' axis
        float randomLocationZ = Random.Range(BoundsController.boundsController.upperBoundary.position.z, BoundsController.boundsController.lowerBoundary.position.z);

        // set the new random position
        Vector3 spawnPosition = new Vector3(randomLocationX, 0.25f, randomLocationZ);

        // selected a random object to spawn
        healthPickupIndex = (Random.Range(0, healthPickup.Length));

        // spawn the object
        GameObject instantitatedObject = Instantiate(healthPickup[healthPickupIndex], spawnPosition, Quaternion.Euler(0f, Random.Range(0, 360f), 0f));

        // add the object to the spawned objects list
        spawnedObjectsList.Add(instantitatedObject);
    }


    public void DestroySpawnedObjects()
    {
        // destroy all spawned objects on game restart
        foreach (GameObject spawnedOject in spawnedObjectsList)
        {
            Destroy(spawnedOject);
        }

        Initialise();
    }


} // end of class
