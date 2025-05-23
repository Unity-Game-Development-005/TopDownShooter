
using UnityEngine;


public class SpawnController : MonoBehaviour
{
    // create an array of health pickup objects
    public GameObject[] healthPickup;

    // create a health pickup index to allow objects to be randomly spawned
    private int healthPickupIndex;

    // check to see if we can spawn
    private bool canSpawn;

    // elapsed time before pickups are spawned
    public float spawnDelay;

    // the time interval before next pickup is spawned
    public float spawnInterval;



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
        canSpawn = false;
    }


    private void CheckPlayerHealth()
    {
        // if the game is running
        if (!GameController.gameController.gameOver)
        {
            // if we can already spawm a pickup
            if (canSpawn)
            {
                // then return
                return;
            }

            // otherwise
            else
            {
                // check until player's health or ammo values are less than their maximum values
                if (Player1Controller.playerOneController.currentHealth < Player1Controller.playerOneController.maximumHealth ||

                    Player1Controller.playerOneController.currentAmmo < Player1Controller.playerOneController.maximumAmmo     ||

                    Player2Controller.playerTwoController.currentHealth < Player2Controller.playerTwoController.maximumHealth ||

                    Player2Controller.playerTwoController.currentAmmo < Player2Controller.playerTwoController.maximumAmmo)
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
        Instantiate(healthPickup[healthPickupIndex], spawnPosition, Quaternion.Euler(0f, Random.Range(0, 360f), 0f));
    }


} // end of class
