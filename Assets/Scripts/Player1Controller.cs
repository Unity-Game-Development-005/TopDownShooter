
using UnityEngine;


public class Player1Controller : MonoBehaviour
{
    // create a singleto for the player 1 controller
    public static Player1Controller player1Controller;



    public PlayerOneHealth playerOneHealthBar;

    public PlayerOneAmmo playerOneAmmoBar;


    public Transform ammoLauncher;

    public GameObject ammoPrefab;


    [SerializeField] private float playerSpeed;

    private float playerVerticalInput;

    private float playerHorizontalInput;

    private Vector3 playerHorizontalDirection;

    private Vector3 playerVerticalDirection;


    private float flip;

    private bool facingRight;


    //[HideInInspector] public int maximumHealth;

    [HideInInspector] public int currentHealth;


    //[HideInInspector] public int maximumAmmo;

    [HideInInspector] public int currentAmmo;


    [HideInInspector] public int player1Score;



    private void Awake()
    {
        if (player1Controller == null)
        {
            player1Controller = this;
        }

        else if (player1Controller != this)
        {
            Destroy(this);
        }
    }



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Initialise();
    }


    // Update is called once per frame
    void Update()
    {
        GetPlayerInput();
    }


    private void GetPlayerInput()
    {
        playerHorizontalInput = Input.GetAxis("Horizontal1");

        playerVerticalInput = Input.GetAxis("Vertical1");

        FireAmmo();

        MovePlayer();
    }


    private void FireAmmo()
    {
        // if we have ammo
        if (currentAmmo > 0)
        {
            // and we have pressed the fire button
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                // then shoot
                Instantiate(ammoPrefab, ammoLauncher.position, transform.rotation);

                // subtract one from ammo
                ExpendAmmo();
            }
        }
    }


    private void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // update the health bar slider
        playerOneHealthBar.SetHealthValue(currentHealth);

        // if we have no more health
        if (currentHealth <= GameController.IS_DEAD)
        {
            // then the game is over
            GameController.gameController.GameOver();
        }
    }


    private void ExpendAmmo()
    {
        currentAmmo -= GameController.AMMO;

        // update the ammo bar slider
        playerOneAmmoBar.SetAmmoValue(currentAmmo);
    }


    private void AddAmmo()
    {
        currentAmmo += GameController.AMMO_PICKUP;

        if (currentAmmo > GameController.MAXIMUM_AMMO)
        {
            currentAmmo = GameController.MAXIMUM_AMMO;
        }

        // update the ammo bar slider
        playerOneAmmoBar.SetAmmoValue(currentAmmo);
    }


    private void AddHealth()
    {
        // add one health point to player's health
        currentHealth += GameController.HEALTH_PICKUP;

        // if player's current health is greater
        if (currentHealth > GameController.MAXIMUM_HEALTH)
        {
            currentHealth = GameController.MAXIMUM_HEALTH;
        }

        // update the health bar slider
        playerOneHealthBar.SetHealthValue(currentHealth);
    }


    public void Initialise()
    {
        flip = 180f;

        // moves player along the 'z' axis
        playerHorizontalDirection = Vector3.forward;

        // moves player along the 'x' axis
        playerVerticalDirection = Vector3.left;


        // reset player start position and rotation
        transform.position = new Vector3(-10f, 0f, 10f);

        facingRight = true;


        // player's score
        player1Score = 0;


        // player's maximum health
        //maximumHealth = GameController.MAXIMUM_HEALTH;

        // player's current health
        currentHealth = GameController.MAXIMUM_HEALTH;

        // initialise the health bar
        playerOneHealthBar.SetMaximumHealth(GameController.MAXIMUM_HEALTH);


        // player's maximum ammo
        //maximumAmmo = GameController.MAXIMUM_AMMO;

        // player's current ammo
        currentAmmo = GameController.MAXIMUM_AMMO;

        // initialise the ammo bar
        playerOneAmmoBar.SetMaximumAmmo(GameController.MAXIMUM_AMMO);
    }


    private void MovePlayer()
    {
        // move the player up along the 'z' axis
        transform.Translate(playerSpeed * Time.deltaTime * playerVerticalInput * playerVerticalDirection);


        if (playerHorizontalInput > 0f)
        {
            FlipCharacterRight();

            transform.Translate(playerSpeed * playerHorizontalInput * Time.deltaTime * playerHorizontalDirection);
        }

        if (playerHorizontalInput < 0f)
        {
            FlipCharacterLeft();

            transform.Translate(playerSpeed * playerHorizontalInput * Time.deltaTime * playerHorizontalDirection);
        }


        // if the player's 'z' position is greater than the 'z' position of the upper boundary
        if (transform.position.z > BoundsController.boundsController.upperBoundary.position.z)
        {
            // then set the player to the upper boundary's 'z' position
            transform.position = new Vector3(transform.position.x, transform.position.y, BoundsController.boundsController.upperBoundary.position.z);
        }

        // if the player's 'z' position is less than the 'z' position of the lower boundary
        if (transform.position.z < BoundsController.boundsController.lowerBoundary.position.z)
        {
            // then set the player to the lower boundary's 'z' position
            transform.position = new Vector3(transform.position.x, transform.position.y, BoundsController.boundsController.lowerBoundary.position.z);
        }


        // if the player's 'x' position is greater than the 'x' position of the right boundary
        if (transform.position.x > BoundsController.boundsController.rightBoundary.position.x)
        {
            // then set the player to the right boundary's 'x' position
            transform.position = new Vector3(BoundsController.boundsController.rightBoundary.position.x, transform.position.y, transform.position.z);
        }

        // if the player's 'x' position is less than the 'x' position of the left boundary
        if (transform.position.x < BoundsController.boundsController.leftBoundary.position.x)
        {
            // then set the player to the left boundary's 'x' position
            transform.position = new Vector3(BoundsController.boundsController.leftBoundary.position.x, transform.position.y, transform.position.z);
        }
    }


    private void FlipCharacterLeft()
    {
        // if the player is facing right
        if (facingRight)
        {
            FlipPlayer();

            facingRight = false;
        }
    }


    private void FlipCharacterRight()
    {
        if (!facingRight)
        {
            FlipPlayer();

            facingRight = true;
        }
    }


    private void FlipPlayer()
    {
        // flip the player 180
        transform.Rotate(0f, flip, 0f);

        playerHorizontalDirection = -playerHorizontalDirection;

        playerVerticalDirection = -playerVerticalDirection;
    }


    private void OnTriggerEnter(Collider collidingObject)
    {
        if (collidingObject.CompareTag("Ammo"))
        {
            Destroy(collidingObject.gameObject);

            TakeDamage(GameController.DAMAGE);

            GameController.gameController.UpdatePlayer2Score();
        }

        if (collidingObject.CompareTag("Ammo Pickup"))
        {
            AddAmmo();

            Destroy(collidingObject.gameObject);
        }

        if (collidingObject.CompareTag("Health Pickup"))
        {
            AddHealth();

            Destroy(collidingObject.gameObject);
        }

        if (collidingObject.CompareTag("Horse"))
        {
            TakeDamage(GameController.MAXIMUM_DAMAGE);
        }
    }


} // end of class
