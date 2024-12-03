using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class ScreenWriter : MonoBehaviour
{
    public static ScreenWriter Instance { get; private set; }

    private TMP_Text _screenText;
    private TMP_Text _continueText;

    //private string[] _testDialgue =
    //{
    //    "First line of dialogue",
    //    "Second line of dialogue",
    //    "Third line of dialogue",
    //    "Fourth line of dialogue",
    //    "Fifth line of dialogue",
    //    "Sixth line of dialogue",
    //};

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

        _screenText = GetComponentInChildren<TMP_Text>();
        _continueText = _screenText.transform.GetChild(0).GetComponent<TMP_Text>();
        _continueText.text = "Press anything to continue...";
        _continueText.enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        //InitializeDialogue(_testDialgue, () => Debug.Log("Dialogue finished!"));
    }

    private IEnumerator WriteWord(string word)
    {
        float writeTime = 0.06f;
        foreach (char letter in word)
        {
            // This doesn't really work since it has to be at the exact frame
            //if (PlayerInput.Instance.IsAnyButtonPressed) writeTime = 0.02f;

            _screenText.text += letter;
            AudioManager.Instance.PlayFXRandomizePitch("Text");
            yield return new WaitForSeconds(writeTime);
        }
    }

    private IEnumerator DisplayText(string[] textToDisplay, Action onComplete)
    {
        if (Instance == null) yield break;

        Instance._screenText.text = "";
        foreach (string line in textToDisplay)
        {
            yield return WriteWord(line);
            _continueText.enabled = true;
            //yield return new WaitUntil(() => PlayerInput.Instance.IsAnyButtonPressed);
            yield return new WaitUntil(() => Keyboard.current.anyKey.wasPressedThisFrame || Pointer.current.press.wasPressedThisFrame);
            _continueText.enabled = false;
            yield return null; // Wait a frame to make sure the button press is not carried over to the next line.
            Instance._screenText.text = "";
        }

        onComplete?.Invoke();
    }

    public static void InitializeDialogue(string[] textToDisplay) => InitializeDialogue(textToDisplay, null);
    public static void InitializeDialogue(string[] textToDisplay, Action onComplete)
    {
        if (Instance == null) return;

        Instance.StartCoroutine(Instance.DisplayText(textToDisplay, onComplete));
    }

    public static void CancelDialogue()
    {
        if (Instance == null) return;

        Instance.StopAllCoroutines();
        Instance._screenText.text = "";
    }
}
