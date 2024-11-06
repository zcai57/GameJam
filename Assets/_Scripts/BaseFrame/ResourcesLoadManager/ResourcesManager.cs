using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ResourcesManager : Singleton<ResourcesManager>
{
    //ͬ�����أ�T�Ƿ��ص���Դ���ͣ�
    public T ResourceLoad<T>(string name)where T: Object
    {
        T resource = Resources.Load<T>(name);
        if(resource is GameObject)
        {
            //ʵ����gameobject��
            return GameObject.Instantiate(resource);
        }
        else
        {
            return resource;
        }
    }
    //�첽���� name��Resources�ļ�������Դ·����func�������ɺ���Ҫ��ɵ��¼���
    //������lambda���ʽ��Ҳ������ί�У�()=>{}��
    public void ResourceLoadAsync<T>(string name,UnityAction<T> func)where T: Object
    {
        CommonMono.Instance.StartCoroutine(RealResourceLoadAsync(name,func));
    }
    private IEnumerator RealResourceLoadAsync<T>(string name, UnityAction<T> func)where T: Object
    {
        ResourceRequest rr = Resources.LoadAsync(name);
        yield return rr;
        if(rr.asset is GameObject)
        {
            //ʵ����gameobject��
            func.Invoke(GameObject.Instantiate(rr.asset) as T);
        }
        else
        {
            func.Invoke(rr.asset as T);
        }
    }
}
