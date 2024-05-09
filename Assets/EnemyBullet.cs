using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour, IPoolable
{
    float lifetime = 2f;
    Vector3 direction;
    Vector3 GetPlayerPosition()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        return player.transform.position;
    }

    public void CalculateDirection()
    {
        direction = (GetPlayerPosition() - transform.position).normalized;
    }

    public void Reset()
    {
        lifetime = 2f;
    }

    void Update()
    {
        lifetime -= Time.deltaTime;
        if (lifetime < 0)
        {
            gameObject.SetActive(false);
        }
        transform.position += direction * 5f * Time.deltaTime;
    }
}
