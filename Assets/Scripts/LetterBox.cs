using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LetterBox : MonoBehaviour
{

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI m_text;
    [SerializeField] private Image m_background;

    private bool hasLetter = false;
    private string currentLetter;

    private void Start()
    {
        m_text.SetText("");
    }

    public bool SetLetter(string letter)
    {
        if(hasLetter == true)
        {
            return false;
        }
        currentLetter = letter;
        m_text.SetText(currentLetter);
        hasLetter = true;
        return true;
    }

    public void SetColor(Color color)
    {
        m_background.color = color;
    }

    public bool DeleteLetter()
    {
        if(hasLetter == false)
        {
            return false;
        }

        currentLetter = "";
        m_text.SetText(currentLetter);
        hasLetter = false;
        return true;
    }

    public string GetLetter()
    {
        return currentLetter;
    }
}
