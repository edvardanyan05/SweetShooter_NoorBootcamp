using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class SceneTransition : MonoBehaviour
{
    public static SceneTransition Instance;
    private Image fadeImage;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SetupUI();
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void SetupUI()
    {
        Canvas canvas = gameObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 999;
        gameObject.AddComponent<CanvasScaler>();
        gameObject.AddComponent<GraphicRaycaster>();

        GameObject imgObj = new GameObject("FadeImage");
        imgObj.transform.SetParent(transform, false);

        fadeImage = imgObj.AddComponent<Image>();
        fadeImage.color = new Color(250f / 255f, 133f / 255f, 158f / 255f, 0f); 
        fadeImage.raycastTarget = false;

        fadeImage.rectTransform.anchorMin = Vector2.zero;
        fadeImage.rectTransform.anchorMax = Vector2.one;
        fadeImage.rectTransform.offsetMin = Vector2.zero;
        fadeImage.rectTransform.offsetMax = Vector2.zero;
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(Transition(sceneName));
    }

    IEnumerator Transition(string sceneName)
    {
        fadeImage.raycastTarget = true;

        yield return fadeImage.DOFade(1f, 0.2f).WaitForCompletion();

        SceneManager.LoadScene(sceneName);

        yield return new WaitForEndOfFrame();

        yield return fadeImage.DOFade(0f, 0.2f).WaitForCompletion();

        fadeImage.raycastTarget = false;
    }
}