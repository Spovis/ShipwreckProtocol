using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor;
using UnityEditor.SceneManagement;

public class SamEditmodeTest {

    

    [UnitySetUp]
    public IEnumerator Setup()  
    {
        
        yield return null;
    }



    [UnityTest]
    public IEnumerator TestRunRoom() {

       EditorSceneManager.OpenScene("Assets/TeamMembers/Sam/Tests/SamEditmodeTests/TestSampleScene.unity");

        yield return null;
    }

    [UnityTest]
    public IEnumerator TestRunLargeRoom() {

       EditorSceneManager.OpenScene("Assets/TeamMembers/Sam/Tests/SamEditmodeTests/TestLargeScene.unity");

        yield return null;
    }
}