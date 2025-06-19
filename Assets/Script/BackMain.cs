using UnityEngine;
using UnityEngine.SceneManagement;

public class BackMain : MonoBehaviour
{
    public void LoadLevelByIndex()
    {
        SceneManager.LoadScene(0);
    }
}
