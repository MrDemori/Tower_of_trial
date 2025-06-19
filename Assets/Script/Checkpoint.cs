using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public ParticleSystem effect;

    private bool triggered = false;

    void Start()
    {
        if (effect != null) effect.Stop();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (triggered) return;
        if (collision.CompareTag("Player"))
        {
            triggered = true;
            if (effect != null)
                effect.Play();
        }
    }
}
