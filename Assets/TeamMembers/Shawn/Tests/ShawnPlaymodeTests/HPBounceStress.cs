using System.Collections;
using UnityEngine;

public class HPBounceStress : MonoBehaviour
{
    // Variables to control speed and height
    public float speed = 5f;

    void Update()
    {
        // Bouncing logic
        Vector3 pos = transform.position;
        float newY = Mathf.Sin(Time.time * speed);
        transform.position = new Vector3(pos.x, newY, pos.z);
    }

    // Stress test function to simulate increasing speed and height
    // This is now public so it can be accessed from the test script
    public IEnumerator StressTest()
    {
        // Start with initial values
        float currentSpeed = speed;

        // Loop for a fixed number of steps or until a condition is met
        for (int i = 0; i < 100; i++) // Example: 100 iterations
        {
            currentSpeed += 1f;    // Increase speed

            // Set the new values
            speed = currentSpeed;

            // Log the current state (optional, helps with debugging)
            Debug.Log($"Iteration {i}: Speed = {currentSpeed}");

            // Wait for one frame
            yield return null;
        }
    }
}
