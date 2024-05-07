using UnityEngine;
using UnityEngine.UI;

public class HealthSlider : MonoBehaviour
{
    public Slider hpSlider; 

    
    public void UpdateHealthSlider(int currentHealth, int maxHealth)
    {
        if (hpSlider != null)
        {
           
            if (hpSlider.maxValue != maxHealth)
            {
                hpSlider.maxValue = maxHealth;
            }
            hpSlider.value = currentHealth;
        }
    }
}
