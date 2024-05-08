using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Scythe : MonoBehaviour
{
    float lifetime = 2f;

    void Update()
    {
        lifetime -= Time.deltaTime;
        if (lifetime < 0)
        {
            gameObject.SetActive(false);
        }
        transform.position += transform.right * 5f * Time.deltaTime;
    }
}
