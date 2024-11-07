using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour, IObserver
{
    //will observe player object put here
    [SerializeField] Subject player;
    Image healthUI;
    [SerializeField] float maxHealth = 5;
    [SerializeField] float health = 4;
    [SerializeField] string reloadSceneName;
    private bool BCMode = false;

    public void Update()
    {
        if (health == 0)
        {
            StartCoroutine(deathTimer());
        }
    }

    public void Start()
    {
        BCMode = PlayerPrefs.GetInt("BCMode", 0) == 1;
        healthUI.fillAmount = health/maxHealth;
    }
    //when receiving a notification from subject
    public void OnNotify(PlayerActions action)
    {
        //switch case to perform different actions based off name
        switch(action)
        {
            case (PlayerActions.Hurt):
                if(BCMode)
                {
                    return;
                }
                healthUI.fillAmount = healthUI.fillAmount - .2f;
                health--;
                Debug.Log("Hurt received");
                return;
            case (PlayerActions.Heal):
                healthUI.fillAmount = healthUI.fillAmount + .2f;
                health++;
                Debug.Log("Heal received");
                return;
            case (PlayerActions.SetHealth):
                healthUI.fillAmount = health/maxHealth;
                return;
        }
    }

    //adds object as observer when enable
    private void OnEnable()
    {
        player.addObserver(this);
        healthUI = transform.Find("HealthFill").GetComponent<Image>();
    }
    
    //removes object as observer when disabled to avoid unecessary signals
    private void OnDisable()
    {
        player.removeObserver(this);
    }

    private IEnumerator deathTimer()
    {
        PlayerInput.Instance.CanInput = false;
        yield return new WaitForSecondsRealtime(3);
        SceneManager.LoadScene(reloadSceneName);
        PlayerInput.Instance.CanInput = true;
    }

    public float getHealth()
    {
        return health;
    }

    public void setBCMode()
    {
        BCMode = !BCMode;
    }
}
