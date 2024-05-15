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
    // Update is called once per frame

    private bool hasLetter = false;

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

        m_text.SetText(letter);
        hasLetter = true;
        return true;
    }

    public void ChangeColor(Color color)
    {
        m_background.color = color;
    }

    public bool DeleteLetter()
    {
        if(hasLetter == false)
        {
            return false;
        }

        m_text.SetText("");
        hasLetter = false;
        return true;
    }
}
