using System;
using System.Collections.Generic;

public interface IEventType
{
    
}
//�޲��޷��ص�ί��
public class EventType_1 : IEventType
{
    public Action actions;
}
//��һ��������ί��
public class EventType_2<T> : IEventType
{
    public Action<T> actions;
}
//��һ������ֵ��ί��
public class EventType_3<T> : IEventType
{
    public Func<T> funcs;
}
//������������ί��
public class EventType_4<T1,T2> : IEventType
{
    public Action<T1,T2> actions;
}

public class EventSystem : Singleton<EventSystem>
{
    public Dictionary<string, IEventType> eventDic= new Dictionary<string, IEventType>();
    public string name;
    #region ���ί��
    //�޲��޷��ص����ί��
    public void AddEvent(string eventName,Action eventIn)
    {
        if (!eventDic.ContainsKey(eventName))
        {
            eventDic.Add(eventName, new EventType_1());
        }
        (eventDic[eventName] as EventType_1).actions += eventIn;
    }
    //��һ�����������ί��
    public void AddEvent<T>(string eventName, Action<T> eventIn)
    {
        if (!eventDic.ContainsKey(eventName))
        {
            eventDic.Add(eventName, new EventType_2<T>());
        }
        (eventDic[eventName] as EventType_2<T>).actions += eventIn;
    }
    //��һ������ֵ�����ί��
    public void AddEvent<T>(string eventName, Func<T> eventIn)
    {
        if (!eventDic.ContainsKey(eventName))
        {
            eventDic.Add(eventName, new EventType_3<T>());
        }
        (eventDic[eventName] as EventType_3<T>).funcs += eventIn;
    }
    //���������������ί��
    public void AddEvent<T1,T2>(string eventName, Action<T1,T2> eventIn)
    {
        if (!eventDic.ContainsKey(eventName))
        {
            eventDic.Add(eventName, new EventType_4<T1,T2>());
        }
        (eventDic[eventName] as EventType_4<T1,T2>).actions += eventIn;
    }
    #endregion

    #region �Ƴ�ί��
    //�޲��޷��ص��Ƴ�ί��
    public void RemoveEvent(string eventName, Action eventIn)
    {
        if (eventDic.ContainsKey(eventName))
        {
            (eventDic[eventName] as EventType_1).actions -= eventIn;
        }
    }
    //��һ���������Ƴ�ί��
    public void RemoveEvent<T>(string eventName, Action<T> eventIn)
    {
        if (eventDic.ContainsKey(eventName))
        {
            (eventDic[eventName] as EventType_2<T>).actions -= eventIn;
        }
    }
    //��һ������ֵ���Ƴ�ί��
    public void RemoveEvent<T>(string eventName, Func<T> eventIn)
    {
        if (eventDic.ContainsKey(eventName))
        {
            (eventDic[eventName] as EventType_3<T>).funcs -= eventIn;
        }
    }
    //�������������Ƴ�ί��
    public void RemoveEvent<T1,T2>(string eventName, Action<T1,T2> eventIn)
    {
        if (eventDic.ContainsKey(eventName))
        {
            (eventDic[eventName] as EventType_4<T1,T2>).actions -= eventIn;
        }
    }
    #endregion

    #region ����ί��
    //�޲��޷��صĴ���ί��
    public void TriggerEvent(string eventName)
    {
        if (eventDic.ContainsKey(eventName))
        {
            (eventDic[eventName] as EventType_1).actions?.Invoke();
        }
    }
    //��һ�������Ĵ���ί��
    public void TriggerEvent<T>(string eventName,T t)
    {
        if (eventDic.ContainsKey(eventName))
        {
            (eventDic[eventName] as EventType_2<T>).actions?.Invoke(t);
        }
    }
    //��һ������ֵ�Ĵ���ί��
    public T TriggerEvent<T>(string eventName)
    {
        if (eventDic.ContainsKey(eventName))
        {
            return (eventDic[eventName] as EventType_3<T>).funcs.Invoke();
        }
        return default(T);
    }
    //�����������Ĵ���ί��
    public void TriggerEvent<T1,T2>(string eventName,T1 t1,T2 t2)
    {
        if (eventDic.ContainsKey(eventName))
        {
            (eventDic[eventName] as EventType_4<T1,T2>).actions?.Invoke(t1,t2);
        }
    }
    #endregion
    #region ���ί��
    public void Clear()
    {
        eventDic.Clear();
    }
    #endregion
}
