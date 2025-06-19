using UnityEngine;

public class Camerafollow : MonoBehaviour
{
    [SerializeField] private Player p;

    void Update()
    {
        transform.position = p.transform.position + new Vector3(0, 0, -10);
    }
}
