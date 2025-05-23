
using UnityEngine;


public class Player2Controller : MonoBehaviour
{
    // create a singleton for the player 2 controller
    public static Player2Controller playerTwoController;


    public PlayerTwoHealth playerTwoHealthBar;

    public PlayerTwoAmmo playerTwoAmmoBar;

    //private BoundsController boundsController;


    public Transform ammoLauncher;

    public GameObject ammoPrefab;


    [SerializeField] private float playerSpeed;

    private float playerVerticalInput;

    private float playerHorizontalInput;

    private Vector3 playerHorizontalDirection;

    private Vector3 playerVerticalDirection;


    private float flip;

    public int playerIndex;

    private bool facingRight;


    public int maximumHealth;

    public int currentHealth;


    public int maximumAmmo;

    public int currentAmmo;



    private void Awake()
    {
        if (playerTwoController == null)
        {
            playerTwoController = this;
        }

        else if (playerTwoController != this)
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
        // get the player's movement inputs
        playerVerticalInput = Input.GetAxis("Vertical" + playerIndex);

        playerHorizontalInput = Input.GetAxis("Horizontal" + playerIndex);

        // see if player is firing
        FireAmmo();

        // move player
        MovePlayer();
    }


    // see if player is firing
    private void FireAmmo()
    {
        // check that we still have some ammo
        if (currentAmmo > 0)
        {
            // if we do, then check if the fire button has been pressed
            if (Input.GetKeyDown(KeyCode.RightControl))
            {
                // if it has, then fire
                Instantiate(ammoPrefab, ammoLauncher.position, transform.rotation);

                // and subtract one from current ammo
                ExpendAmmo();
            }
        }
    }


    private void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // update the health bar slider
        playerTwoHealthBar.SetHealthValue(currentHealth);

        // if we have no more health
        if (currentHealth == GameController.IS_DEAD)
        {
            // then the game is over
            GameController.gameController.GameOver();
        }
    }


    private void ExpendAmmo()
    {
        currentAmmo -= GameController.AMMO;

        // update the ammo bar slider
        playerTwoAmmoBar.SetAmmoValue(currentAmmo);
    }


    private void AddAmmo()
    {
        currentAmmo += GameController.AMMO_PICKUP;

        // update the ammo bar slider
        playerTwoAmmoBar.SetAmmoValue(currentAmmo);
    }


    private void AddHealth()
    {
        currentHealth += GameController.HEALTH_PICKUP;

        // update the health bar slider
        playerTwoHealthBar.SetHealthValue(currentHealth);
    }


    private void Initialise()
    {
        flip = 180f;

        // moves player along the 'z' axis
        playerHorizontalDirection = Vector3.forward;

        // moves player along the 'x' axis
        playerVerticalDirection = Vector3.left;

        facingRight = true;


        // player's maximum health
        maximumHealth = GameController.MAXIMUM_HEALTH;

        // player's current health
        currentHealth = maximumHealth;

        // initialise the health bar
        playerTwoHealthBar.SetMaximumHealth(maximumHealth);


        // player's maximum ammo
        maximumAmmo = 6;

        // player's current ammo
        currentAmmo = maximumAmmo;

        // initialise the ammo bar
        playerTwoAmmoBar.SetMaximumAmmo(maximumAmmo);
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
        if (transform.position.z > BoundsController.boundsController.upperBoundary.position.z) //.position.z)
        {
            // then set the player to the upper boundaries 'z' position
            transform.position = new Vector3(transform.position.x, transform.position.y, BoundsController.boundsController.upperBoundary.position.z); //.position.z);
        }

        // if the player's 'z' position is less than the 'z' position of the lower boundary
        if (transform.position.z < BoundsController.boundsController.lowerBoundary.position.z) //.position.z)
        {
            // then set the player to the lower boundaries 'z' position
            transform.position = new Vector3(transform.position.x, transform.position.y, BoundsController.boundsController.lowerBoundary.position.z);  //.position.z);
        }


        if (transform.position.x > BoundsController.boundsController.rightBoundary.position.x) //; .position.x)
        {
            //transform.position = new Vector3(boundsController.rightBoundary.position.x, transform.position.y, transform.position.z);
            transform.position = new Vector3(BoundsController.boundsController.rightBoundary.position.x, transform.position.y, transform.position.z);
        }

        if (transform.position.x < BoundsController.boundsController.leftBoundary.position.x) //.position.x)
        {
            //transform.position = new Vector3(boundsController.leftBoundary.position.x, transform.position.y, transform.position.z);
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

            GameController.gameController.UpdatePlayer1Score();
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
    }


} // end of class
