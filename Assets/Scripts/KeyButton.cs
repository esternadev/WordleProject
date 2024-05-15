using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyButton : MonoBehaviour
{
    public Action<KeyboardLayout> onClick;
    [SerializeField] private KeyboardLayout layout;
    [SerializeField] private Button m_keyButton;
   
    void Start()
    {
        m_keyButton = GetComponent<Button>();
        m_keyButton.onClick.AddListener(() => onClick?.Invoke(layout));
    }


}
