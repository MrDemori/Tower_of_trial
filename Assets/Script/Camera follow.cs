using UnityEngine;

public class Camerafollow : MonoBehaviour
{
    [SerializeField] private PlayerMovement p;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = p.transform.position + new Vector3(0, 0, -10);
    }
}
