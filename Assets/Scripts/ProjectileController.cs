
using UnityEngine;


public class ProjectileController : MonoBehaviour
{
    // left and right boundary limits
    private float leftBoundary;

    private float rightBoundary;


    // set the speed of the projectile
    public float projectileSpeed = 30f;



    private void Start()
    {
        Initialise();
    }


    void Update()
    {
        MoveProjectile();
    }


    private void Initialise()
    {
        leftBoundary = -13f;

        rightBoundary = 13f;
    }


    private void MoveProjectile()
    {
        // move the projectile
        transform.Translate(projectileSpeed * Time.deltaTime * Vector3.forward);


        // check to see if projectile has gone off screen
        if (transform.position.x < leftBoundary)
        {
            Destroy(gameObject);
        }

        if (transform.position.x > rightBoundary)
        {
            Destroy(gameObject);
        }
    }


} // end of class
