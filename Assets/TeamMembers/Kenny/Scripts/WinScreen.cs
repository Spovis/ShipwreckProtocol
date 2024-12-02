using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{
    private ScreenFader screenFade;
    private ScreenWriter screenWriter;

    [SerializeField] private string[] _introText;

    // Start is called before the first frame update
    void Awake()
    {
        GameObject overlays = GameObject.Find("ScreenOverlays");
        screenFade = overlays.GetComponentInChildren<ScreenFader>();
        screenWriter = overlays.GetComponentInChildren<ScreenWriter>();
    }

    private void Start()
    {
        ScreenFader.SetScreenColor(Color.black);
        ScreenWriter.InitializeDialogue(_introText, BackToMenu);
    }

    private void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
        ScreenFader.FadeIn(1f).SetDelay(1f);
    }
}
