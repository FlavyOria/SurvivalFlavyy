using UnityEngine;

public class CrystalExperience : MonoBehaviour
{
    public int xpAmount = 10; 
    private XPBarController xpBarController;

    private void Start()
    {
        xpBarController = FindObjectOfType<XPBarController>(); 
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
