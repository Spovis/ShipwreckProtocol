using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private float _runTime = 0;
    private float _score = 0;
    public float RunTime { get { return _runTime; } }
    public float Score { get { return _score; } set { _score = value; } }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Update()
    {
        _runTime += Time.deltaTime;
    }
}
