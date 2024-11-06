using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ScenesManager : Singleton<ScenesManager>
{
    //�л����� ͬ������
    public void LoadScene(string sceneName,UnityAction func)
    {
        SceneManager.LoadScene(sceneName);
        func.Invoke();
    }
    //�л����� �첽����
    public void LoadSceneAsync(string sceneName,UnityAction func)
    {
        CommonMono.Instance.StartCoroutine(ReallyLoadSceneAsync(sceneName,func));
    }
    private IEnumerator ReallyLoadSceneAsync(string sceneName,UnityAction func)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(sceneName);
        while(!ao.isDone)
        {
            //���½�����
            EventSystem.Instance.TriggerEvent<float>("���ȸ���", ao.progress);
            yield return ao.progress;
        }
        func.Invoke();
    }
}
