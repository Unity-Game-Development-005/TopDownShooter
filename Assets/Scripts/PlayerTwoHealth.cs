
using UnityEngine;
using UnityEngine.UI;


public class PlayerTwoHealth : MonoBehaviour
{
    // get a reference to the slider component
    public Slider healthBarSlider;



    // set player's maximum health
    public void SetMaximumHealth(int health)
    {
        healthBarSlider.maxValue = health;

        healthBarSlider.value = health;
    }


    // adjust the value of the health bar
    public void SetHealthValue(int health)
    {
        healthBarSlider.value = health;
    }


} // end of class
