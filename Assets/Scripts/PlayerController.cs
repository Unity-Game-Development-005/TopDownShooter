
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform upperBoundary;

    [SerializeField] private Transform lowerBoundary;

    private float verticalInput1;

    private float verticalInput2;

    private float playerSpeed;

    public int playerIndex;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Initialise();
    }

    // Update is called once per frame
    void Update()
    {
        verticalInput1 = Input.GetAxis("Vertical");

        verticalInput2 = Input.GetAxis("Vertical1");

        MovePlayer1();

        MovePlayer2();
    }


    private void Initialise()
    {
        playerSpeed = 6f;
    }


    private void MovePlayer1()
    {
        transform.Translate(playerSpeed * Time.deltaTime * verticalInput1 * Vector3.left);

        if (transform.position.z > upperBoundary.position.z)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, upperBoundary.position.z);
        }

        if (transform.position.z < lowerBoundary.position.z)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, lowerBoundary.position.z);
        }
    }


    private void MovePlayer2()
    {
        transform.Translate(playerSpeed * Time.deltaTime * verticalInput1 * Vector3.left);

        if (transform.position.z > upperBoundary.position.z)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, upperBoundary.position.z);
        }

        if (transform.position.z < lowerBoundary.position.z)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, lowerBoundary.position.z);
        }
    }


} // end of class
