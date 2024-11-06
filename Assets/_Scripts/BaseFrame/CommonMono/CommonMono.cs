using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CommonMono : Singleton<CommonMono>
{
    public CommonMonoController controller;
    public CommonMono()
    {
        GameObject obj = new GameObject("CommonMonoController");
        controller = obj.AddComponent<CommonMonoController>();
    }
    //����޲��޷����¼��ķ���
    public void AddUpdate(UnityAction func)
    {
        controller.AddUpdate(func);
    }
    //�Ƴ��޲��޷����¼��ķ���
    public void RemoveUpdate(UnityAction func)
    {
        controller.RemoveUpdate(func);
    }
    //����Э�̵ķ���
    public Coroutine StartCoroutine(IEnumerator routine)
    {
        return controller.StartCoroutine(routine);
    }
    //�ر�Э�̵ķ���
    public void StopCoroutine(IEnumerator routine)
    {
        controller.StopCoroutine(routine);
    }

    public void DestroyObject(GameObject obj)
    {
        controller.DestroyObject(obj);
    }
}
