using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float movespeed;
    [SerializeField] GameObject scythePrefab;
    [SerializeField] float scytheTimer = 1;
    float currentScytheTimer;
    Rigidbody2D rb;
    Animator animator;
    XPBarController xpBarController; // Reference to the XPBarController script

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        xpBarController = FindObjectOfType<XPBarController>(); // Find the XPBarController in the scene
    }

    private void Update()
    {
        currentScytheTimer -= Time.deltaTime;
        if (currentScytheTimer <= 0)
        {
            // Spawn the scythe
            for (int i = 0; i < 9; i++)
            {
                Quaternion rot = Quaternion.Euler(0, 0, Random.Range(0, 360f));

                //Instantiate(scythePrefab, transform.position, rot);
                GameObject scythe = ObjectPool.GetInstance().GetPooledObject();
                scythe.transform.SetPositionAndRotation(transform.position, rot);
                scythe.SetActive(true);
            }
            currentScytheTimer += scytheTimer;
        }

        // Handle player movement
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        rb.velocity = new Vector2(x, y) * movespeed;

        // Flip player sprite based on movement direction
        if (x != 0)
        {
            int a;
            if (x > 0)
                a = 1;
            else
                a = -1;

            transform.localScale = new Vector3(a, 1, 1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CrystalExperience"))
        {
            // Gain XP
            xpBarController.GainXP(20); // Assuming GainXP is a method in your XPBarController script

            // Destroy the CrystalExperience object
            Destroy(collision.gameObject);
        }
    }
}
