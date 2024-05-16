using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class KeyboardUI : MonoBehaviour
{
    public event Action<KeyboardLayout> onPlayerType;

    [Header("Color")]
    [SerializeField] private Color correctColor;
    [SerializeField] private Color incorrectColor;
    [SerializeField] private Color notFoundColor;
    [SerializeField] private Color normalColor;

    [SerializeField] private KeyButton[] keyButtons;

    private void Awake()
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

    public void UpdateKeyStatus(char letter, WordleStatus status)
    {
        KeyboardLayout layout = Enum.Parse<KeyboardLayout>(letter.ToString().ToUpper());
        KeyButton key = keyButtons.FirstOrDefault(k => k.Layout == layout);

        Color color;
        if(status == WordleStatus.CORRECT)
        {
            color = correctColor;
        }
        else if(status == WordleStatus.INCORRECT)
        {
            color = incorrectColor;
        }
        else
        {
            color = notFoundColor;
        }

        key.SetColor(color);
    }

    public void ResetKeyboard()
    {
        foreach(var button in keyButtons)
        {
            button.SetColor(normalColor);
        }
    }

}
