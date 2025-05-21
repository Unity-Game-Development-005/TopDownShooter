
using UnityEngine;


public class AnimalController : MonoBehaviour
{
    public BoundsController boundsController;

    [SerializeField] private float animalSpeed;

    private Vector3 animalDirection;

    private float flip;

    public int animalIndex;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Initialise();
    }


    // Update is called once per frame
    void Update()
    {
        MoveAnimal();
    }


    private void Initialise()
    {
        animalDirection = Vector3.forward;

        flip = 180f;
    }


    private void MoveAnimal()
    {
        transform.Translate(animalSpeed * Time.deltaTime * animalDirection);

        if (transform.position.z > boundsController.upperBoundary.position.z)
        {
            transform.Rotate(0f, flip, 0f);
        }

        if (transform.position.z < boundsController.lowerBoundary.position.z)
        {
            transform.Rotate(0f, flip, 0f);
        }
    }


} // end of class
