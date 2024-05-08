using UnityEngine;

public class Player : MonoBehaviour
{
    public int maxHealth = 5; // Maximum health of the player
    private int currentHealth; // Current health of the player

    [SerializeField] float movespeed;
    [SerializeField] GameObject scythePrefab;
    [SerializeField] float scytheTimer = 1;
    float currentScytheTimer;
    Rigidbody2D rb;
    Animator animator;
    XPBarController xpBarController; // Reference to the XPBarController script
    public HealthSlider healthSlider; // Reference to the HealthSlider script for updating health UI

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        xpBarController = FindObjectOfType<XPBarController>();
        currentHealth = maxHealth; // Set current health to max health initially

        healthSlider.SetInitialHealth(maxHealth);
    }

    private void Update()
    {
        currentScytheTimer -= Time.deltaTime;
        if (currentScytheTimer <= 0)
        {
            // Spawn the scythe
            for (int i = 0; i < 12; i++)
            {
                Quaternion rot = Quaternion.Euler(0, 0, Random.Range(0, 360f));
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
            int a = x > 0 ? 1 : -1;
            transform.localScale = new Vector3(a, 1, 1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Merman"))
        {
            // Decrease player's health by 1
            TakeDamage(1);
        }
        else if (collision.CompareTag("CrystalExperience"))
        {
            // Gain XP
            xpBarController.GainXP(20);

            // Destroy the CrystalExperience object
            Destroy(collision.gameObject);
        }
    }


    private void TakeDamage(int damage)
    {
        currentHealth -= damage;
        // Ensure health stays within bounds
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        healthSlider.UpdateHealthSlider(currentHealth);
        if (currentHealth <= 0)
        {

            HandleDefeat();
        }
    }


    // Player Deafeat 
    private void HandleDefeat()
    {
        Debug.Log("Game Over - Skills issues !");
        rb.velocity = Vector2.zero; // Stop player movement
        this.enabled = false; // Disable the Player script
        Invoke("ResetLevel", 3f); // Reload scene after 3 seconds
    }

    // Method to reset the level
    private void ResetLevel()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}