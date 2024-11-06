using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    private bool isStartGame;
    public InputManager()
    {
        CommonMono.Instance.AddUpdate(Update);
        StartOrEndInput(false);
    }
    public void StartOrEndInput(bool isRun)
    {
        isStartGame = isRun;
    }

    private void InputKey(KeyCode key)
    {
        if (Input.GetKeyDown(key))
        {
            EventSystem.Instance.TriggerEvent<KeyCode>("keyDown", key);
        }
        if (Input.GetKeyUp(key))
        {
            EventSystem.Instance.TriggerEvent<KeyCode>("keyUp", key);
        }
    }

    private void InputMouse(int key)
    {
        if (Input.GetMouseButtonDown(key))
        {
            EventSystem.Instance.TriggerEvent<int>("mouseDown", key);
        }
        if (Input.GetMouseButtonUp(key))
        {
            EventSystem.Instance.TriggerEvent<int>("mouseUp", key);
        }
    }
    private void Update()
    {
        if (!isStartGame) return;
        InputKey(KeyCode.W);
    }
}
