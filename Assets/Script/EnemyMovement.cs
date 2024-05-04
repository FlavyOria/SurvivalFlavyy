using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 2f; // Adjust this to control the speed of enemy movement

    // Move the enemy towards the target direction
    public void MoveTowards(Vector3 direction)
    {
        // Calculate the movement amount based on speed and time
        Vector3 movement = direction * speed * Time.deltaTime;

        // Move the enemy
        transform.Translate(movement);
    }
}
