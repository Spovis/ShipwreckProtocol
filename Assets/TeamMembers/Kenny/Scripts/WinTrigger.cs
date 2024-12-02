using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinTrigger : MonoBehaviour
{
  private void OnTriggerEnter2D(Collider2D collision)
    {
      Debug.Log("TRIGGER: " + collision.name);
      if (collision.CompareTag("Player"))
      {
        // Load the win screen
        SceneManager.LoadScene("WinScreen");
        ScreenFader.FadeIn(1f).SetDelay(1f);
      }
    }
}
