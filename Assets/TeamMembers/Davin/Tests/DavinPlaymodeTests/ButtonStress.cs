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
    public GameObject player;

    private List<GameObject> buttonPool = new List<GameObject>(); // Object pool of buttons
    public float currentFPS;
    private Queue<float> fpsHistory = new Queue<float>(); // Queue to store FPS history
    private const int fpsSampleSize = 10; // Number of frames to average over

    [UnitySetUp]
    public IEnumerator OneTimeSetUp()
    {
        SceneManager.LoadScene("Scenes/SampleScene");
        yield return null;
    }

    [UnityTest]
    public IEnumerator StressTestUI()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        bool startup = false;
        int poolSize = 100000;
        float randomX, randomY;

        // ** Warm-up phase to let Unity stabilize FPS **
        int warmupFrames = 200;
        for (int w = 0; w < warmupFrames; w++)
        {
            yield return null;  // Wait for several frames to allow FPS to stabilize
        }

        // Start the actual test after the warm-up phase
        int consecutiveFramesAbove200 = 0; // Track consecutive frames with FPS above 200

        for (int i = 0; i < poolSize; i++)
        {
            // Generate separate random values for X and Y axes
            randomX = Random.Range(-30.0f, 30.0f);
            randomY = Random.Range(-30.0f, 30.0f);

            GameObject newButton = new GameObject("Button " + i);
            newButton.transform.SetParent(parentPanel);

            // Set the button's position to a random location around the player
            newButton.transform.position = new Vector3(player.transform.position.x + randomX, player.transform.position.y + randomY, 0);

            Button buttonComponent = newButton.AddComponent<Button>();
            TextMesh buttonText = newButton.AddComponent<TextMesh>();
            Image image = newButton.AddComponent<Image>();
            buttonText.text = "Button " + i;
            buttonPool.Add(newButton);

            // Calculate the current FPS using Time.deltaTime
            currentFPS = 1.0f / Time.deltaTime;

            // Add current FPS to history
            fpsHistory.Enqueue(currentFPS);
            if (fpsHistory.Count > fpsSampleSize)
            {
                fpsHistory.Dequeue(); // Remove the oldest frame to keep the queue size constant
            }

            // Calculate the average FPS from the fpsHistory queue
            float averageFPS = 0f;
            foreach (float fps in fpsHistory)
            {
                averageFPS += fps;
            }
            averageFPS /= fpsHistory.Count;

            // ** Smooth FPS calculation over multiple frames **
            if (averageFPS > 200)
            {
                consecutiveFramesAbove200++;
            }
            else
            {
                consecutiveFramesAbove200 = 0;  // Reset if average FPS drops below 200
            }

            // Consider it "startup" only if we've been above 200 FPS for a few frames
            if (consecutiveFramesAbove200 >= 10)
            {
                startup = true;  // Now begin tracking when FPS drops
            }

            // Break the loop if average FPS drops below 60 after the startup phase
            if (startup && averageFPS < 60)
            {
                Debug.Log(averageFPS + " average fps");
                Assert.Pass(i + " buttons drop average fps lower than 60");
                yield break;  // Exit the coroutine
            }

            // Yield control to allow Unity to render frames and avoid blocking the engine
            yield return null;
        }

        // If we exit the loop without the FPS dropping below 200, fail the test
        Assert.Fail(currentFPS + " fps is higher than expected after testing all buttons.");
    }
}
