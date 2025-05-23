
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

    // store player's health and ammo values
    private int playerOneCurrentHealth;

    private int playerOneCurrentAmmo;

    private int playerTwoCurrentHealth;

    private int playerTwoCurrentAmmo;

    private int playerOneMaximumHealth;

    private int playerOneMaximumAmmo;

    private int playerTwoMaximumHealth;

    private int playerTwoMaximumAmmo;



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
        playerOneCurrentHealth = Player1Controller.playerOneController.currentHealth;

        playerOneCurrentAmmo = Player1Controller.playerOneController.currentAmmo;

        playerOneMaximumHealth = Player1Controller.playerOneController.maximumAmmo;

        playerOneMaximumAmmo = Player1Controller.playerOneController.maximumAmmo;


        playerTwoCurrentHealth = Player2Controller.playerTwoController.currentHealth;

        playerTwoCurrentAmmo = Player2Controller.playerTwoController.currentAmmo;

        playerTwoMaximumHealth = Player2Controller.playerTwoController.maximumHealth;

        playerTwoMaximumAmmo = Player2Controller.playerTwoController.maximumAmmo;


        canSpawn = false;
    }


    private void CheckPlayerHealth()
    {
        // if we can already spawm a pickup
        if (canSpawn)
        {
            // then return
            return;
        }

        // otherwise
        {
            // check until player's health or ammo values are less than their maximum values
            if (playerOneCurrentHealth < playerOneMaximumHealth ||
                playerOneCurrentAmmo < playerOneMaximumAmmo ||
                playerTwoCurrentHealth < playerTwoMaximumHealth ||
                playerTwoCurrentAmmo < playerTwoMaximumAmmo)
            {
                canSpawn = true;
                Debug.Log("can spawn");
                // then start spawning
                //InvokeRepeating(nameof(SpawnPickup), spawnDelay, spawnInterval);
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
