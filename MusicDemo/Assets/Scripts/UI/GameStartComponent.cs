using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartComponent : MonoBehaviour
{
    public string SceneAName;
    public string SceneBName;
    // public string CurrSceneName;
    private bool test =false;
    

    public void HandleLoadScene(bool isA)
    {
        StartCoroutine(LoadScenesCoroutine(SceneAName,SceneBName,isA));
        // HandleChangeScene(isA);
    }
    
    
    private IEnumerator LoadScenesCoroutine(string sceneA, string sceneB,bool isA)
    {
        // 获取当前场景名称
        string currentSceneName = SceneManager.GetActiveScene().name;

        // 开始异步加载两个场景
        AsyncOperation loadOperationA = SceneManager.LoadSceneAsync(sceneA, LoadSceneMode.Additive);
        //loadOperationA.allowSceneActivation = false; 
        AsyncOperation loadOperationB = SceneManager.LoadSceneAsync(sceneB, LoadSceneMode.Additive);
        //loadOperationA.allowSceneActivation = false; 
        
        // 等待两个场景加载完成
        while ((!loadOperationA.isDone || !loadOperationB.isDone) && !test)
        {
            yield return null; // 等待下一帧
        }
        test = true;
        

        // 隐藏当前场景
        AsyncOperation unloadOperation = SceneManager.UnloadSceneAsync(currentSceneName);

        SceneManager.UnloadSceneAsync(isA ? SceneBName : SceneAName);

        // 等待当前场景卸载完成
        while (!unloadOperation.isDone)
        {
            yield return null; // 等待下一帧
        }
    }




    private void HandleChangeScene(bool isA)
    {
        SceneManager.LoadScene(isA ? SceneAName : SceneBName);
    }


}
