using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CommonMonoController : MonoBehaviour
{
    private event UnityAction updateEvent;
    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    private void Update()
    {
        if (updateEvent != null)
        {
            updateEvent.Invoke();
        }
    }
    public void AddUpdate(UnityAction func)
    {
        updateEvent += func;
    }
    public void RemoveUpdate(UnityAction func)
    {
        updateEvent -= func;
    }

    public void DestroyObject(GameObject obj)
    {
        Destroy(obj);
    }
}
