using UnityEngine;

public class ExperienceCrystal : MonoBehaviour, IPoolable
{
    public void Reset()
    {
        // Reset the state of the experience crystal
        gameObject.SetActive(true);
        // You can add additional reset logic here if needed
    }
}
