using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartComponent : MonoBehaviour
{
    public string SceneAName;
    public string SceneBName;
    private AsyncOperation asyncSceneA;
    private AsyncOperation asyncSceneB;

    public void HandleLoadScene(bool isA)
    {
        // StartCoroutine(LoadScenes(isA));
        HandleChangeScene(isA);
    }

    // todo add this back
    private IEnumerator LoadScenes(bool value)
    {
        asyncSceneA = SceneManager.LoadSceneAsync(SceneAName, LoadSceneMode.Additive);
        asyncSceneA.allowSceneActivation = false;
        
        // asyncSceneB = SceneManager.LoadSceneAsync(SceneBName, LoadSceneMode.Additive);
        // asyncSceneB.allowSceneActivation = false;

        // while (!asyncSceneA.isDone || !asyncSceneB.isDone)
        // {
        //     if (asyncSceneA.progress >= 0.9f && asyncSceneB.progress >= 0.9f) break;
        //     yield return null;
        // }
        
        while (!asyncSceneA.isDone)
        {
            if (asyncSceneA.progress >= 0.9f) break;
            yield return null;
        }
        
        if(value)asyncSceneA.allowSceneActivation = true;
        else asyncSceneB.allowSceneActivation = true;
        
        SceneManager.UnloadSceneAsync("StartScene");
    }

    private void HandleChangeScene(bool isA)
    {
        SceneManager.LoadScene(isA ? SceneAName : SceneBName);
    }


}
