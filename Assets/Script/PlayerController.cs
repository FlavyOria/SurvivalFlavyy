using UnityEngine;

public class Player : MonoBehaviour
{
    public int hp = 5; // Points de vie initiaux du joueur
    [SerializeField] float movespeed;
    [SerializeField] GameObject scythePrefab;
    [SerializeField] float scytheTimer = 1;
    float currentScytheTimer;
    Rigidbody2D rb;
    Animator animator;
    XPBarController xpBarController; // Reference to the XPBarController script
    public HealthSlider healthSlider; // R�f�rence au script HealthSlider dans l'inspecteur pour mettre � jour le slider de sant�

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
            for (int i = 0; i < 12; i++)
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
        if (collision.CompareTag("Merman")) 
        {
            hp--; // D�cr�mente les points de vie du joueur
            healthSlider.UpdateHealthSlider(hp, 5); // Met � jour le slider de sant� avec les nouveaux points de vie
            if (hp <= 0)
            {
                // Si les points de vie sont �puis�s, d�clenche la d�faite
                HandleDefeat();
            }
        }
        else if (collision.CompareTag("CrystalExperience"))
        {
            // Gain XP
            xpBarController.GainXP(20); 

            // Destroy the CrystalExperience object
            Destroy(collision.gameObject);
        }
    }

    void HandleDefeat()
    {
        
        Debug.Log("Game Over - Skills issues !");

        // stop mouvement ca DEAD
        rb.velocity = Vector2.zero; // Arr�te le mouvement du joueur
        this.enabled = false; // D�sactive le script Player

        
        Invoke("ResetLevel", 3f); // Appelle la m�thode ResetLevel apr�s 3 secondes
    }

    void ResetLevel()
    {
       // reload
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}