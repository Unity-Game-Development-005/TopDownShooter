
using UnityEngine;
using UnityEngine.UI;


public class PlayerTwoAmmo : MonoBehaviour
{
    // get a reference to the slider component
    public Slider ammoBarSlider;



    // set player's maximum ammo
    public void SetMaximumAmmo(int ammo)
    {
        ammoBarSlider.maxValue = ammo;

        ammoBarSlider.value = ammo;
    }


    // adjust the value of the ammo bar
    public void SetAmmoValue(int ammo)
    {
        ammoBarSlider.value = ammo;
    }


} // end of class
