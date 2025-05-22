
using UnityEngine;


public class BoundsController : MonoBehaviour
{
    // create a singleton for the bounds controller
    public static BoundsController boundsController;


    // define the four boundaries
    public Transform upperBoundary;

    public Transform lowerBoundary;

    public Transform leftBoundary;

    public Transform rightBoundary;



    private void Awake()
    {
        // if the singleton doesn't already exist
        if (boundsController == null)
        {
            // then create it
            boundsController = this;
        }

        // otherwise
        else if (boundsController != this)
        {
            // destroy the it
            Destroy(this);
        }
    }


} // end of class
