using UnityEngine;

public class Merman : MonoBehaviour
{
    public float speed = 1f; // Adjust this to control the speed of the merman movement
    SoundPlayer soundPlayer;

    void Update()
    {
        // Move the merman towards the player if the player exists
        if (PlayerExists())
        {
            Vector3 playerPosition = GetPlayerPosition();
            Vector3 direction = (playerPosition - transform.position).normalized;
            Debug.Log("Direction: " + direction);
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
        if (collision.CompareTag("Scythe"))
        {
            Debug.Log("Merman OnTriggerEnter2D triggered by scythe collider");

            // Play death audio
            SoundPlayer.GetInstance().PlayDeathAudio();

            // Spawn experience crystals from the object pool
            for (int i = 0; i < 3; i++)
            {
                GameObject CrystalExperience = ObjectPool.GetInstance().GetPooledObject();

                // Check if the CrystalExperience prefab is valid
                if (CrystalExperience != null)
                {
                    // Reset the state of the CrystalExperience prefab
                    CrystalExperience.GetComponent<IPoolable>().Reset();

                    // Set a random offset position from the Merman's position
                    Vector3 offset = Random.insideUnitCircle * 0.5f; // Adjust the offset as needed
                    CrystalExperience.transform.position = transform.position + offset;
                }
            }

            // Destroy the Merman GameObject
            Destroy(gameObject);
        }
    }
}