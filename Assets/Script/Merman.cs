using UnityEngine;

public class Merman : MonoBehaviour
{
    SoundPlayer soundPlayer;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        SoundPlayer.GetInstance().PlayDeathAudio();
        Destroy(gameObject);
    }
}