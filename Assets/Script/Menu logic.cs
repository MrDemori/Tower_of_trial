using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [Header("Панелі")]
    public RectTransform mainMenuPanel;
    public RectTransform optionsPanel;
    public RectTransform gamePanel;

    [Header("Анімація")]
    public float animationDuration = 0.5f;

    private Vector2 center = Vector2.zero;
    private float screenWidth;

    private void Start()
    {
        screenWidth = Screen.width;

        // Початкові позиції
        mainMenuPanel.anchoredPosition = center;
        optionsPanel.anchoredPosition = new Vector2(-screenWidth, 0); // ліворуч
        gamePanel.anchoredPosition = new Vector2(screenWidth, 0);     // праворуч
    }

    public void OnOptionsPressed()
    {
        StopAllCoroutines();
        StartCoroutine(SlidePanel(mainMenuPanel, new Vector2(screenWidth, 0)));    // Main → вправо
        StartCoroutine(SlidePanel(optionsPanel, center));                          // Options → центр
    }

    public void OnBackFromOptions()
    {
        StopAllCoroutines();
        StartCoroutine(SlidePanel(mainMenuPanel, center));                         // Main → центр
        StartCoroutine(SlidePanel(optionsPanel, new Vector2(-screenWidth, 0)));    // Options → вліво
    }

    public void OnStartPressed()
    {
        StopAllCoroutines();
        StartCoroutine(SlidePanel(mainMenuPanel, new Vector2(-screenWidth, 0)));   // Main → вліво
        StartCoroutine(SlidePanel(gamePanel, center));                             // Game → центр
    }

    public void OnBackFromGame()
    {
        StopAllCoroutines();
        StartCoroutine(SlidePanel(mainMenuPanel, center));                         // Main → центр
        StartCoroutine(SlidePanel(gamePanel, new Vector2(screenWidth, 0)));       // Game → вправо
    }

    private IEnumerator SlidePanel(RectTransform panel, Vector2 target)
    {
        Vector2 start = panel.anchoredPosition;
        float elapsed = 0f;

        while (elapsed < animationDuration)
        {
            panel.anchoredPosition = Vector2.Lerp(start, target, elapsed / animationDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        panel.anchoredPosition = target;
    }

    public void LoadLevelByIndex(int index)
    {
        SceneManager.LoadScene(index);
    }

}
