using UnityEngine;

public class CrystalExperience : MonoBehaviour
{
    public int xpAmount = 10; // Define the amount of XP gained when the crystal is picked up
    private XPBarController xpBarController;

    private void Start()
    {
        xpBarController = FindObjectOfType<XPBarController>(); // Find the XPBarController in the scene
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            xpBarController.GainXP(xpAmount);
            Destroy(gameObject);
          
        }
    }
}
