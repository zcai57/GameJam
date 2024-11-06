using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ScenesManager : Singleton<ScenesManager>
{
    //切换场景 同步加载
    public void LoadScene(string sceneName,UnityAction func)
    {
        SceneManager.LoadScene(sceneName);
        func.Invoke();
    }
    //切换场景 异步加载
    public void LoadSceneAsync(string sceneName,UnityAction func)
    {
        CommonMono.Instance.StartCoroutine(ReallyLoadSceneAsync(sceneName,func));
    }
    private IEnumerator ReallyLoadSceneAsync(string sceneName,UnityAction func)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(sceneName);
        while(!ao.isDone)
        {
            //更新进度条
            EventSystem.Instance.TriggerEvent<float>("进度更新", ao.progress);
            yield return ao.progress;
        }
        func.Invoke();
    }
}
