using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideMe : MonoBehaviour
{
    [SerializeField] private float delay = 1f;
    private void OnEnable()
    {
        Invoke("Hide", delay);
    }
    private void Hide()
    {
        ObjectPool.Instance.SendBack(this.name, this.gameObject);
    }
}
