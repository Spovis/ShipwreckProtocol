using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;


public class ButtonStress
{
    public GameObject buttonPrefab; // Reference to a button prefab (assigned in Editor)
    public Transform parentPanel; // Reference to the parent panel where buttons will be placed

    private List<GameObject> buttonPool = new List<GameObject>(); // Object pool of buttons
    private bool isStressing = false;
    public float current;

    bool sceneLoaded = false;
    [UnitySetUp]
    public IEnumerator OneTimeSetUp()
    {
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        SceneManager.LoadScene("Scenes/SampleScene");
        yield return null;
    }

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        sceneLoaded = true;
    }

    [UnityTest]
    public IEnumerator StressTestUI()
    {
        bool startup = false;
        // Pre-create and pool a large number of buttons
        int poolSize = 10000;
        for (int i = 0; i < poolSize; i++)
        {
            // Instead of using Instantiate during the test, we can set up the pool
            GameObject newButton = new GameObject("Button " + i);
            newButton.transform.SetParent(parentPanel);
            Button buttonComponent = newButton.AddComponent<Button>();
            TextMesh buttonText = newButton.AddComponent<TextMesh>();
            Image image = newButton.AddComponent<Image>();
            buttonText.text = "Button " + i;
            buttonPool.Add(newButton);
            current = (int)(Time.frameCount / Time.time);
            yield return null;
            yield return null;
            yield return null;
            if(current > 200)
            {
                startup = true;
            }
            if (startup == true && current < 200)
            {
                Debug.Log(current + " current fps");
                Assert.Pass(current + " fps is lower than expected");
            }
        }
        
            Assert.Fail(current + " fps is higher than expected");

        // Yield to let Unity handle the frame and avoid locking the engine
        yield return null;
        }
    }
