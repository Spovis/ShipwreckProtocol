using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroScene : MonoBehaviour
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
        ScreenWriter.InitializeDialogue(_introText, StartGame);
    }

    private void StartGame()
    {
        SceneManager.LoadScene("Game");
        ScreenFader.FadeIn(1f).SetDelay(1f);
    }
}
