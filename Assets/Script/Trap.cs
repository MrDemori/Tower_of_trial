using System.Collections;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public GameObject spikes;
    public float delayBeforeActivate = 1.5f;
    public float activeDuration = 1f;

    private bool isTriggered = false;

    void Start()
    {
        spikes.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isTriggered)
        {
            StartCoroutine(HandleSpikeTrap());
        }
    }

    IEnumerator HandleSpikeTrap()
    {
        isTriggered = true;

        yield return new WaitForSeconds(delayBeforeActivate);

        spikes.SetActive(true);

        yield return new WaitForSeconds(activeDuration);

        spikes.SetActive(false);
        isTriggered = false;
    }
}
