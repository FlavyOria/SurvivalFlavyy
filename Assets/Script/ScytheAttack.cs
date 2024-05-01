using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScytheAttack : MonoBehaviour
{
    [SerializeField] private float speed = 10f;


    void Update()
    {
        transform.position += Vector3.right * speed * Time.deltaTime;


    }
}
