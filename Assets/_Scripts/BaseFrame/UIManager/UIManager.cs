using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public enum E_UI_Layer
{
    Bot,
    Mid,
    Top,
    System
}

public class UIManager : Singleton<UIManager>
{
    public Dictionary<string, BasePanel> panelDic = new Dictionary<string, BasePanel>();
    public RectTransform canvas;
    private Transform bot;
    private Transform top;
    private Transform mid;
    private Transform system;
    public UIManager()
    {
        GameObject obj= ResourcesManager.Instance.ResourceLoad<GameObject>("Prefabs/UI/Canvas");
        canvas = obj.transform as RectTransform;
        GameObject.DontDestroyOnLoad(obj);
        bot= canvas.Find("Bot");
        top = canvas.Find("Top");
        mid = canvas.Find("Mid");
        system = canvas.Find("System");
        obj=ResourcesManager.Instance.ResourceLoad<GameObject>("Prefabs/UI/EventSystem");
        GameObject.DontDestroyOnLoad(obj);

    }
    public Transform GetLayerFather(E_UI_Layer layer)
    {
        switch (layer)
        {
            case E_UI_Layer.Bot:
                return bot;
            case E_UI_Layer.Mid:
                return mid;
            case E_UI_Layer.Top:
                return top;
            case E_UI_Layer.System:
                return system;
        }
        return null;
    }
    //��ʾ���(panelName�������)
    //UnityAction<T> func�Ǵ�������߼�
    public void ShowPanel<T>(string panelName,E_UI_Layer layer = E_UI_Layer.Mid,UnityAction<T> func=null) where T:BasePanel
    {
        if(panelDic.ContainsKey(panelName))
        {
            panelDic[panelName].ShowMe();
            func(panelDic[panelName] as T);
            return;
        }
        ResourcesManager.Instance.ResourceLoadAsync<GameObject>("Prefabs/UI/" + panelName, (obj) =>
        {
            obj.name = panelName;
            Transform father = bot;
            switch (layer)
            {
                case E_UI_Layer.Mid:
                    father = mid;
                    break;
                case E_UI_Layer.Top:
                    father = top;
                    break;
                case E_UI_Layer.System:
                    father = system;
                    break;
            }
            obj.transform.SetParent(father);
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localScale = Vector3.one;

            (obj.transform as RectTransform).offsetMax = Vector2.zero;
            (obj.transform as RectTransform).offsetMin = Vector2.zero;

            T panel = obj.GetComponent<T>();
            if(func!=null)
                func(panel);
            panel.ShowMe();
            panelDic.Add(panelName, panel);
        }); 
    }
    //�������
    public void HidePanel(string panelName)
    {
        if(panelDic.ContainsKey(panelName))
        {
            panelDic[panelName].HideMe();
            GameObject.Destroy(panelDic[panelName].gameObject);
            panelDic.Remove(panelName);
        }
    }
    //��ȡ���
    public T GetPanel<T>(string panelName) where T:BasePanel
    {
        if(panelDic.ContainsKey(panelName))
        {
            return panelDic[panelName] as T;
        }
        return null;
    }
    //����Զ����¼�
    public void AddCustomEventListener(UIBehaviour control,EventTriggerType type,UnityAction<BaseEventData> func)
    {
        EventTrigger trigger=control.GetComponent<EventTrigger>();
        if(trigger==null)
        {
            trigger = control.gameObject.AddComponent<EventTrigger>();
        }
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = type;
        entry.callback.AddListener(func);
        trigger.triggers.Add(entry);
    }
}
