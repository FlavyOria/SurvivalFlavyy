using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    // Une instance private et static comme variable de membre
    private static SoundPlayer instance;

    [SerializeField] AudioSource deathAudio;

    // Accès public à l’instance à travers une méthode static
    public static SoundPlayer GetInstance() => instance;

    private void Awake()
    {
        instance = this;
    }

    public void PlayDeathAudio()
    {
        deathAudio.Play();
    }
}