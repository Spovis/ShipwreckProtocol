using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class SamEditmodeTestTemplate {


    [UnitySetUp]
    public IEnumerator Setup()  // Ensure this method is IEnumerator for coroutines
    {
        myLoadedAssetBundle = AssetBundle.LoadFromFile("Assets/TeamMembers/Sam/Tests/SamEditmodeTests/TestSampleScene.unity");
        yield return null;
    }



    [UnityTest]
    public IEnumerator TestRunRoom() {

        global::System.Object value = UnityEditor.SceneManagement.EditorSceneManager.LoadSceneInPlayMode(TestSampleScene);


        yield return null;
    }
}