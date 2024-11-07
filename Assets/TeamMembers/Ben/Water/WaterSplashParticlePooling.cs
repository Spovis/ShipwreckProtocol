using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSplashParticlePooling : MonoBehaviour
{
    private void OnParticleSystemStopped()
    {
        ObjectPoolManager.ReturnObjectToPool(gameObject);
    }
}
