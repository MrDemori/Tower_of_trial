using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Finish : MonoBehaviour
{
    private Player moveScript;

    private float startTime;
    private bool finished = false;

    void Start()
    {
        startTime = Time.time;
        moveScript = GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !finished)
        {
            EndLevelLogic stats = FindObjectOfType<EndLevelLogic>();
            if (stats != null)
            {
                stats.LevelCompleted();
            }
            finished = true;
            Player moveScript = collision.GetComponent<Player>();
            if (moveScript != null)
                moveScript.canMove = false;
            StartCoroutine(ShowStatsAfterDelay());
        }
    }

    IEnumerator ShowStatsAfterDelay()
    {
        yield return new WaitForSeconds(2f);
    }
}
