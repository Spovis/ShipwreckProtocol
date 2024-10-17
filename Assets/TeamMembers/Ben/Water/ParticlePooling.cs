using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePooling : MonoBehaviour
{
    private void OnParticleSystemStopped()
    {
        ObjectPoolManager.ReturnObjectToPool(gameObject);
    }
}
