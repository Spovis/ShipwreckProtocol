using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    //function for closing menu
    public void CloseMenu(GameObject go)
    {
        go.SetActive(false);
        Time.timeScale = 1f;
    }
}
