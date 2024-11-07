using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuNavigation : MonoBehaviour
{
    [SerializeField] GameObject firstMenu;
    [SerializeField] GameObject firstMenuFirstSelected;
    bool firstStarted = true;
    [SerializeField] GameObject secondMenu;
    [SerializeField] GameObject secondMenuFirstSelected;
    bool secondStarted = true;
    [SerializeField] GameObject thirdMenu;
    [SerializeField] GameObject thirdMenuFirstSelected;
    bool thirdStarted = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(firstMenu.activeInHierarchy && firstStarted)
        {
            firstStarted = false;
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(firstMenuFirstSelected);
        }
        else if (secondMenu.activeInHierarchy && secondStarted)
        {
            secondStarted = false;
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(secondMenuFirstSelected);

        }
        else if (thirdMenu.activeInHierarchy && thirdStarted)
        {
            thirdStarted = false;
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(thirdMenuFirstSelected);
        }
        if(!firstMenu.activeInHierarchy && !firstStarted)
        {
            firstStarted = true;
        }
        if (!secondMenu.activeInHierarchy && !secondStarted)
        {
            secondStarted = true;
        }
        if (!thirdMenu.activeInHierarchy && !thirdStarted)
        {
            thirdStarted = true;
        }
    }
}
