using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
//�ҵ����пؼ����ṩ��ʾ�����صķ���
public class BasePanel : MonoBehaviour
{
    private Dictionary<string,List<UIBehaviour>> controlDic = new Dictionary<string, List<UIBehaviour>>();

    protected virtual void Awake()
    {
        FindChildController<Button>();
        FindChildController<TMP_Text>();
        FindChildController<Image>();
        FindChildController<Toggle>();
        FindChildController<Slider>();
        FindChildController<ScrollRect>();
        FindChildController<TMP_InputField>();
        FindChildController<TMP_Dropdown>();
    }
    //��ȡ����ϵĿؼ�(name��UI�����)
    protected T GetController<T>(string name) where T: UIBehaviour
    {
        if (controlDic.ContainsKey(name))
        {
            for (int i = 0; i < controlDic[name].Count; i++)
            {
                if (controlDic[name][i] is T)
                {
                    return controlDic[name][i] as T;
                }
            }
        }
        return null;
    }
    protected virtual void OnClick(string butName)
    {
        
    }
    protected virtual void OnValueChanged(string toggleName,bool value)
    {
        
    }
    protected virtual void OnValueChanged(string sliderName, float value)
    {

    }
    protected virtual void OnValueChanged(string inputFieldName, string value)
    {

    }
    protected virtual void OnValueChanged(string scrollRectName, Vector2 value)
    {

    }
    protected virtual void OnValueChanged(string dropDownName, int value)
    {

    }
    //��ʾ
    public virtual void ShowMe()
    {
        
    }
    //����
    public virtual void HideMe()
    {
        
    }
    private void FindChildController<T>() where T : UIBehaviour
    {
        T[] ts = this.GetComponentsInChildren<T>();
        
        for (int i = 0; i < ts.Length; i++)
        {
            string objName = ts[i].gameObject.name;
            if (controlDic.ContainsKey(objName))
            {
                controlDic[objName].Add(ts[i]);
            }
            else
            {
                controlDic.Add(objName, new List<UIBehaviour>() { ts[i] });
            }
            if (ts[i] is Button)
            {
                (ts[i] as Button).onClick.AddListener(() =>
                {
                    OnClick(objName);
                });
            }
            else if (ts[i] is Toggle)
            {
                (ts[i] as Toggle).onValueChanged.AddListener((value) =>
                {
                    OnValueChanged(objName, value);
                });
            }
            else if (ts[i] is Slider)
            {
                (ts[i] as Slider).onValueChanged.AddListener((value) =>
                {
                    OnValueChanged(objName, value);
                });
            }
            else if (ts[i].gameObject.GetComponent<TMP_InputField>() is TMP_InputField)
            {
                (ts[i].gameObject.GetComponent<TMP_InputField>() as TMP_InputField).onValueChanged.AddListener((value) =>
                {
                    OnValueChanged(objName, value);
                });
            }
            else if (ts[i] is ScrollRect)
            {
                
                (ts[i] as ScrollRect).onValueChanged.AddListener((value) =>
                {
                    OnValueChanged(objName, value);
                });
            }
            else if(ts[i] is TMP_Dropdown)
            {
                (ts[i].gameObject.GetComponent<TMP_Dropdown>() as TMP_Dropdown).onValueChanged.AddListener((value) =>
                {
                    OnValueChanged(objName, value);
                });
            }
        }        
    }
}
