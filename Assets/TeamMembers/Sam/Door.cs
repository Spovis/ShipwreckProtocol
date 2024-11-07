using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;


public class Door : MonoBehaviour
{
    public Animator dooranimator;
    public string levelToLoad;
    private bool isPlayerInDoor;

private void Update()
{
    if (isPlayerInDoor && PlayerInput.Instance.IsInteractPressed)
    {
        Debug.Log("is interacted");
        dooranimator.SetTrigger("DoorOpen");
        StartCoroutine(LoadScene());
    }
}

private void OnTriggerEnter2D(Collider2D collision)
{
    if (collision.CompareTag("Player"))
    {
        isPlayerInDoor = true;
        Debug.Log("entered");
    }
}

private void OnTriggerExit2D(Collider2D collision)
{        
    if (collision.CompareTag("Player"))
    {
        isPlayerInDoor = false;
        Debug.Log("exited");
    }
}

private IEnumerator LoadScene()
{
  yield return new WaitForSeconds(1);
  SceneManager.LoadScene(levelToLoad);
}

}
