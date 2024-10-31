using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Yarn : Items, ICollectables
{
    public static event Action OnYarnTouched;
    public override void Collect()
    {
        //need to track score?
        //TBD yarn(score)++
        OnYarnTouched?.Invoke();
        Debug.Log("Yarn Touched"); //debug, needs removed
        AudioManager.Instance.PlayFX("Yarn");
        Destroy(gameObject);
    }
}

//public class Yarn : MonoBehaviour, ICollectables
//{
//    public static event Action OnYarnTouched;
//    public void Collect()
//    {
//        //need to track score
//        //TBD yarn(score)++, 
//        OnYarnTouched?.Invoke();
//        Debug.Log("Yarn Touched");
//        AudioManager.Instance.PlayFX("Yarn");
//        Destroy(gameObject);
//    }
//}

