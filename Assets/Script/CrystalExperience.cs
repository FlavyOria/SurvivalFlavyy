using UnityEngine;

public class CrystalExperience : MonoBehaviour, IPoolable
{
    public int xpAmount = 10;
    private XPBarController xpBarController;
    float lifetime = 60f;

    public void Reset()
    {
        lifetime = 60f;
    }
    void Update()
    {
        lifetime -= Time.deltaTime;
        if (lifetime < 0)
        {
            gameObject.SetActive(false);
        }
    }


    private void Start()
    {
        xpBarController = FindObjectOfType<XPBarController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            xpBarController.GainXP(xpAmount);
            gameObject.SetActive(false);
        }
    }
}