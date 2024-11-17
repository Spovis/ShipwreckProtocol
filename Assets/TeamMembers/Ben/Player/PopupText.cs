using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PopupText : MonoBehaviour
{
    public static PopupText Instance { get; private set; }

    private TMP_Text _text;
    private Vector3 startPos;

    private Tween _fadeOutTween;

    private Coroutine _displayCoroutine;

    // Start is called before the first frame update
    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        _text = transform.Find("PopupText").GetComponent<TMP_Text>();
        startPos = _text.transform.localPosition;
    }

    private void DisplayText(bool doShake = false)
    {
        _text.color = Color.white;

        if(doShake) _text.transform.DOShakePosition(0.2f, 0.25f, 50);

        _fadeOutTween?.Kill();
        if (_displayCoroutine != null) StopCoroutine(_displayCoroutine);

        _text.transform.localPosition = startPos;

        _displayCoroutine = StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(1.5f);
        _fadeOutTween = DOVirtual.Color(Color.white, Color.clear, 1.5f, (value) =>
        {
            _text.color = value;
        });
    }

    /// <summary>
    /// Display text above the player for a short period of time
    /// </summary>
    /// <param name="text">Text to display</param>
    /// <param name="shakeText">If the text should shake a bit, signalling some kind of error or fail</param>
    public static void Show(string text, bool shakeText = false)
    {
        if (Instance == null || Instance._text == null) return;

        Instance._text.text = text;
        Instance.DisplayText(shakeText);
    }
}
