
using UnityEngine;


public class Player1Controller : MonoBehaviour
{
    public BoundsController boundsController;

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

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Instantiate(ammoPrefab, ammoLauncher.position, transform.rotation);
        }

        MovePlayer();
    }


    private void Initialise()
    {
        flip = 180f;

        // moves player along the 'z' axis
        playerHorizontalDirection = Vector3.forward;

        // moves player along the 'x' axis
        playerVerticalDirection = Vector3.left;

        facingRight = true;
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
        if (transform.position.z > boundsController.upperBoundary.position.z)
        {
            // then set the player to the upper boundary's 'z' position
            transform.position = new Vector3(transform.position.x, transform.position.y, boundsController.upperBoundary.position.z);
        }

        // if the player's 'z' position is less than the 'z' position of the lower boundary
        if (transform.position.z < boundsController.lowerBoundary.position.z)
        {
            // then set the player to the lower boundary's 'z' position
            transform.position = new Vector3(transform.position.x, transform.position.y, boundsController.lowerBoundary.position.z);
        }


        // if the player's 'x' position is greater than the 'x' position of the right boundary
        if (transform.position.x > boundsController.rightBoundary.position.x)
        {
            // then set the player to the right boundary's 'x' position
            transform.position = new Vector3(boundsController.rightBoundary.position.x, transform.position.y, transform.position.z);
        }

        // if the player's 'x' position is less than the 'x' position of the left boundary
        if (transform.position.x < boundsController.leftBoundary.position.x)
        {
            // then set the player to the left boundary's 'x' position
            transform.position = new Vector3(boundsController.leftBoundary.position.x, transform.position.y, transform.position.z);
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


} // end of class
