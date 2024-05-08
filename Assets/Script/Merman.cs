using UnityEngine;

public class Merman : MonoBehaviour
{
    public float speed = 1f;
    public int hp = 1; // Points de vie initiaux du Merman
    SoundPlayer soundPlayer;
    [SerializeField] GameObject XPCrystal;

    void Update()
    {
        // Move the merman towards the player if the player exists
        if (PlayerExists())
        {
            Vector3 playerPosition = GetPlayerPosition();
            Vector3 direction = (playerPosition - transform.position).normalized;
            MoveTowards(direction);
        }
    }

    void MoveTowards(Vector3 direction)
    {
        // Calculate the movement amount based on speed and time
        Vector3 movement = direction * speed * Time.deltaTime;

        // Move the merman
        transform.Translate(movement);
    }

    bool PlayerExists()
    {
        return GameObject.FindGameObjectWithTag("Player") != null;
    }

    Vector3 GetPlayerPosition()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        return player.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Scythe"))
        {
            hp--;
            if (hp <= 0)
            {
                Die(transform.position); // Call Die() with the Merman's position
            }
            else
            {
                // Effet visuel ou changement couleur 
            }
        }
    }

    void Die(Vector3 deathPosition)
    {

        SoundPlayer.GetInstance().PlayDeathAudio();

        // Spawn experience crystal from the object pool
        GameObject CrystalExperience = ObjectPool.GetInstance().GetPooledObject();


        if (CrystalExperience != null)
        {

            CrystalExperience.GetComponent<IPoolable>().Reset();


            CrystalExperience.transform.position = deathPosition;
        }


        Instantiate(XPCrystal, deathPosition, Quaternion.identity);

        // Destroy the Merman GameObject
        Destroy(gameObject);
    }
}