using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordleLineViewport : MonoBehaviour
{
    [Header("Color")]
    [SerializeField] private Color correctColor;
    [SerializeField] private Color foundColor;
    [SerializeField] private Color incorrectColor;

    [SerializeField] private LetterBox[] letterBoxArrays;

    private int maxLetter = 5;

    private void Start()
    {
        GameManager.Instance.KeyboardLayer.onPlayerType += InputLetterHandler;
    }

    public void InputLetterHandler(KeyboardLayout character)
    {
        
        SetLetterInBox(character.ToString(),0);
    }

    public void SetLetterInBox(string character, int index)
    {
        if (index >= maxLetter)
        {
            return;
        }
            bool success = letterBoxArrays[index].SetLetter(character);
        if(success == true)
        {
            Debug.Log("Set Letter Success: "+character);
        }
        else
        {
            SetLetterInBox(character, index + 1);
        }
    }
}
