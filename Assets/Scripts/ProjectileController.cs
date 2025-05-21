
using UnityEngine;


public class ProjectileController : MonoBehaviour
{
    // get a reference to the bounds controller script
    private BoundsController boundsController;

    // set the speed of the projectile
    public float projectileSpeed = 30f;



    void Update()
    {
        // move the projectile
        transform.Translate(projectileSpeed * Time.deltaTime * Vector3.forward);
    }


} // end of class
