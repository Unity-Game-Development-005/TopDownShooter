
using UnityEngine;


public class AnimalController : MonoBehaviour
{
    // get a reference to the bounds controller script
    //private GameController boundsController;

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

        // if the horse reaches a play area boundary 
        if (transform.position.z > BoundsController.boundsController.upperBoundary.position.z)
        {
            // turn the horse around
            transform.Rotate(0f, flip, 0f);
        }

        if (transform.position.z < BoundsController.boundsController.lowerBoundary.position.z)
        {
            transform.Rotate(0f, flip, 0f);
        }
    }


    private void OnTriggerEnter(Collider collidingObject)
    {
        // if a player shoots the horse
        if (collidingObject.CompareTag("Ammo")        ||

            // or the horse eats an ammo pickup
            collidingObject.CompareTag("Ammo Pickup") ||

            // or the horse eats a health pickup
            collidingObject.CompareTag("Ammo Pickup"))
        {
            // simply destroy the object
            Destroy(collidingObject.gameObject);
        }
    }


} // end of class
