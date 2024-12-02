using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFader : MonoBehaviour
{
    public static ScreenFader Instance { get; private set; }

    private Image _fadeImage;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        _fadeImage = GetComponentInChildren<Image>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //SetScreenColor(Color.black);
        //FadeIn().SetDelay(1);
    }

    /// <summary>
    /// Sets the overlay color of the screen
    /// </summary>
    /// <param name="color"></param>
    public static void SetScreenColor(Color color)
    {
        if (Instance == null) return;

        Instance._fadeImage.color = color;
    }

    /// <summary>
    /// Fades the screen to a solid color over a duration
    /// </summary>
    /// <param name="duration"></param>
    /// <returns></returns>
    public static Tween FadeOut(float duration = 1f) => FadeOut(duration, Color.black);
    /// <summary>
    /// Fades the screen to a solid color over a duration
    /// </summary>
    /// <param name="duration"></param>
    /// <returns></returns>
    public static Tween FadeOut(float duration, Color fadeColor)
    {
        if (Instance == null) return null;

        return Instance._fadeImage.DOColor(fadeColor, duration);
    }

    /// <summary>
    /// Clears the solid color on the screen over a duration
    /// </summary>
    /// <param name="duration"></param>
    /// <returns></returns>
    public static Tween FadeIn(float duration = 1f)
    {
        if (Instance == null) return null;

        return Instance._fadeImage.DOColor(Color.clear, duration);
    }
}
