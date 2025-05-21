
using UnityEngine;


public class Player2Controller : MonoBehaviour
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
        playerVerticalInput = Input.GetAxis("Vertical" + playerIndex);

        playerHorizontalInput = Input.GetAxis("Horizontal" + playerIndex);

        if (Input.GetKeyDown(KeyCode.RightControl))
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
            // then set the player to the upper boundaries 'z' position
            transform.position = new Vector3(transform.position.x, transform.position.y, boundsController.upperBoundary.position.z);
        }

        // if the player's 'z' position is less than the 'z' position of the lower boundary
        if (transform.position.z < boundsController.lowerBoundary.position.z)
        {
            // then set the player to the lower boundaries 'z' position
            transform.position = new Vector3(transform.position.x, transform.position.y, boundsController.lowerBoundary.position.z);
        }


        if (transform.position.x > boundsController.rightBoundary.position.x)
        {
            transform.position = new Vector3(boundsController.rightBoundary.position.x, transform.position.y, transform.position.z);
        }

        if (transform.position.x < boundsController.leftBoundary.position.x)
        {
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
