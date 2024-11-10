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
        yield return null;
    }

    [UnityTest]
    public IEnumerator TrackFpsAsObjectsAreAdded()
    {
        for (int currentObjectCount = 0; currentObjectCount <= maxObjectCount; currentObjectCount += objectIncrement)
        {
            // Add objects to the scene
            AddObjects(objectIncrement);

            // Wait a few frames to allow for stabilization
            yield return new WaitForSeconds(0.5f);

            // Calculate FPS
            float fps = 1.0f / Time.deltaTime;
            Debug.Log($"Objects: {currentObjectCount}, FPS: {fps}");

            // If we slow down too much, stop the test and pass
            if (fps < 100) {
                Assert.Pass("FPS has dropped below 100");
                ClearObjects();
                break;
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
