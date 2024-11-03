using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject settingsMenu;
    [SerializeField] GameObject confirmationMenu;

    // Start is called before the first frame update
    void Start()
    {
        settingsMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //pauses game when pause button pressed
        if (PlayerInput.Instance.IsPausePressed)
        {
            if(pauseMenu.activeInHierarchy == false && !settingsMenu.activeInHierarchy && !confirmationMenu.activeInHierarchy)
            {
                PlayerInput.Instance.IsPausePressed = false;
                PlayerInput.Instance.CanInput = false;
                pauseMenu.SetActive(true);
                Time.timeScale = 0f;
            }
            else if (settingsMenu.activeInHierarchy || confirmationMenu.activeInHierarchy)
            {
                PlayerInput.Instance.IsPausePressed = false;
                pauseMenu.SetActive(true);
                settingsMenu.SetActive(false);
                confirmationMenu.SetActive(false);
            }
            else
            {
                Debug.Log("Unpause");
                PlayerInput.Instance.CanInput = true;
                pauseMenu.SetActive(false);
                Time.timeScale = 1f;
            }

        }
    }

    //function for closing menu
    public void CloseMenu(GameObject go)
    {
        PlayerInput.Instance.CanInput = true;
        go.SetActive(false);
        Time.timeScale = 1f;
    }

    public void SetMenuActive(GameObject setActive)
    {  
        setActive.SetActive(true);
    }

    public void SetMenuInactive(GameObject setInactive)
    {
        setInactive.SetActive(false);
    }

    public void SwitchScene(string scene)
    {
        SceneManager.LoadScene(scene);
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
