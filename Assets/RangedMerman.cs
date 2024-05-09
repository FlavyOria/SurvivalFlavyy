using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedMerman : MonoBehaviour
{
    public float speed = 1f;
    public int hp = 2; // Points de vie initiaux du Merman
    SoundPlayer soundPlayer;
    [SerializeField] GameObject XPCrystal;


    [SerializeField] GameObject scythePrefab;
    [SerializeField] float scytheTimer = 1;
    float currentScytheTimer;
    void Update()
    {
        currentScytheTimer -= Time.deltaTime;
        if (currentScytheTimer <= 0)
        {
            // Spawn the scythe
            for (int i = 0; i < 1; i++)
            {
                Quaternion rot = Quaternion.Euler(0, 0, Random.Range(0, 360f));
                GameObject scythe = ObjectPoolBullet.GetInstance().GetPooledObject();
                EnemyBullet bullet = scythe.GetComponent<EnemyBullet>();
                bullet.CalculateDirection();
                scythe.transform.SetPositionAndRotation(transform.position, Quaternion.identity);
                scythe.SetActive(true);
            }
            currentScytheTimer += scytheTimer;
        }

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
