using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectType
{
    public GameObject fatherObj;
    public List<GameObject> objList;
    public ObjectType(GameObject obj, GameObject poolObj)
    {
        fatherObj= new GameObject(obj.name+"_Pool");
        fatherObj.transform.parent = poolObj.transform;
        objList = new List<GameObject>() { obj };
        obj.transform.parent = fatherObj.transform;
        obj.SetActive(false);
    }
    public void PushObject(GameObject obj)
    {
        objList.Add(obj);
        obj.transform.parent = fatherObj.transform;
        obj.SetActive(false);
    }
    public GameObject GetObject()
    {
        GameObject obj;
        obj = objList[0];
        objList.RemoveAt(0);
        obj.SetActive(true);
        obj.transform.parent = null;
        return obj;
    }
}
public class ObjectPool : Singleton<ObjectPool>
{
    private Dictionary<string, ObjectType> poolDic = new Dictionary<string, ObjectType>();
    private GameObject poolObj;
    //ȡ����(Prefabs/"name")
    public void GetObject(string name,UnityAction<GameObject> func)
    {        
        if (poolDic.ContainsKey(name) && poolDic[name].objList.Count > 0)
        {
            func.Invoke(poolDic[name].GetObject());
        }
        else
        {
            //obj = Instantiate(Resources.Load<GameObject>(name));
            ResourcesManager.Instance.ResourceLoadAsync<GameObject>("Prefabs/"+name, (o) => 
            {
                o.name = name; 
                func(o); 
            });
        }
    }
    //�Ŷ���
    public void SendBack(string name,GameObject obj)
    {
        if (poolObj == null)
        {
            poolObj = new GameObject("ObjectPool");
        }
        if (poolDic.ContainsKey(name))
        {
            poolDic[name].PushObject(obj);
        }
        else
        {
            poolDic.Add(name,new ObjectType(obj,poolObj));
        }
    }
    //���
    public void Clear()
    {
        poolDic.Clear();
        poolObj = null;
    }
}
