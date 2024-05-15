using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardLayer : MonoBehaviour
{
    public Action<KeyboardLayout> onPlayerType;

    [SerializeField] private KeyButton[] keyButtons;

    private void Start()
    {
        foreach (var button in keyButtons)
        {
            button.onClick += PressKey;
        }
    }

    public void PressKey(KeyboardLayout keyButton)
    {
        Debug.Log(keyButton.ToString());
        onPlayerType?.Invoke(keyButton);
    }

}
