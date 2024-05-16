using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyButton : MonoBehaviour
{
    public Action<KeyboardLayout> onClick;
    public KeyboardLayout Layout => layout;

    [SerializeField] private KeyboardLayout layout;
    [SerializeField] private Button keyButton;

    [SerializeField] private Image keyImage;
   
    void Awake()
    {
        keyButton = GetComponent<Button>();
        keyImage = GetComponent<Image>();
        keyButton.onClick.AddListener(() => onClick?.Invoke(layout));
    }



    public void SetColor(Color color)
    {
        keyImage.color = color;
    }

    private void OnDisable()
    {
        keyButton.onClick.RemoveAllListeners();
    }

}
