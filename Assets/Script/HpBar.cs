using UnityEngine;
using UnityEngine.UI;

public class HealthSlider : MonoBehaviour
{
    public Slider hpSlider;

    // Method to set the initial health of the slider
    public void SetInitialHealth(int maxHealth)
    {
        // Set the max value of the slider to the maximum health
        hpSlider.maxValue = maxHealth;
        // Set the value of the slider to the maximum health
        hpSlider.value = maxHealth;
    }

    // Method to update the health slider with current health
    public void UpdateHealthSlider(int currentHealth)
    {
        if (hpSlider != null)
        {
            // Update the value of the slider to the current health
            hpSlider.value = currentHealth;
        }
    }
}
