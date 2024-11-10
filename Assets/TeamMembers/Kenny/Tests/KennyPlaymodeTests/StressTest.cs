using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class FpsPerformanceTest
{
    // Parameters for the test
    private int objectIncrement = 50;
    private int maxObjectCount = 10000;
    private float minAcceptableFps = 30f;
    public GameObject bossPrefab;

    // List to keep track of objects added (for cleanup)
    private readonly List<GameObject> spawnedObjects = new List<GameObject>();

    [UnitySetUp]
    public IEnumerator OneTimeSetUp()
    {
        SceneManager.LoadScene("Scenes/SampleScene");
        for (int i = 0; i < 100; i++) {
            // wait for 0.1 s
            yield return new WaitForSeconds(0.1f);

            Debug.Log("Waiting for boss prefab...");
            bossPrefab = GameObject.Find("Boss");
        }
        yield return null;
    }

    [UnityTest]
    public IEnumerator TrackFpsAsObjectsAreAdded()
    {
        for (int currentObjectCount = 0; currentObjectCount <= maxObjectCount; currentObjectCount += objectIncrement)
        {
            // Wait a few frames to allow for stabilization
            yield return new WaitForSeconds(0.5f);

            // Add objects to the scene
            Debug.Log("TEST: before");
            AddObjects(objectIncrement);
            Debug.Log("TEST: after");

            // Calculate FPS
            float fps = 1.0f / Time.deltaTime;
            Debug.Log($"Objects: {currentObjectCount}, FPS: {fps}");

            // If we slow down too much, stop the test and pass
            if (fps < 40) {
                Debug.Log($"Current FPS: {fps}");
                Debug.Log($"Current Object Count: {currentObjectCount}");
                Assert.Pass("FPS has dropped below 40");
                ClearObjects();
                break;
            } else {
                Debug.Log($"Current FPS: {fps}");
                Debug.Log($"Current Object Count: {currentObjectCount}");
            }
        }
    }

    private void AddObjects(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject obj = UnityEngine.Object.Instantiate(bossPrefab);
            obj.transform.position = Random.insideUnitSphere * 10;
            spawnedObjects.Add(obj);
        }
    }

    private void ClearObjects()
    {
        foreach (var obj in spawnedObjects)
        {
            UnityEngine.Object.Destroy(obj);
        }
        spawnedObjects.Clear();
    }
}
