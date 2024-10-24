using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //pauses game when pause button pressed
        if(PlayerInput.Instance.IsPausePressed)
        {
            Debug.Log("pressed");
            if(pauseMenu.activeInHierarchy == false)
            {
                PlayerInput.Instance.IsPausePressed = false;
                Debug.Log("Paused");
                PlayerInput.Instance.CanInput = false;
                pauseMenu.SetActive(true);
                Time.timeScale = 0f;
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
