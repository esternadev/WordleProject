using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WordleLineViewport : MonoBehaviour
{
    [Header("Color")]
    [SerializeField] private Color correctColor;
    [SerializeField] private Color incorrectColor;
    [SerializeField] private Color notFoundColor;
    [SerializeField] private Color normalColor;

    [SerializeField] private LetterBox[] letterBoxArrays;

    private int maxLetter = 5;
    private bool isCompleted = false;

    public void InputLetterHandler(KeyboardLayout character)
    {

        SetLetterInBox(character.ToString(), 0);
    }

    public void SetLetterInBox(string character, int index)
    {
        if (index >= maxLetter)
        {
            return;
        }
        bool success = letterBoxArrays[index].SetLetter(character);
        if (success == true)
        {
            Debug.Log("Set Letter Success: " + character);
        }
        else
        {
            SetLetterInBox(character, index + 1);
        }
    }

    public void DeleteLetter()
    {
        DeleteLetterInBox(maxLetter - 1);
    }

    public bool SubmitWord(out string word)
    {
        word = "";
        word = string.Join("", letterBoxArrays.Select(l => l.GetLetter()).ToArray());
        word = word.ToLower();
        Debug.Log($"Submit : {word}");
        if (GameManager.Instance.CheckWordHasExist(word))
        {
            UpdateLetterInBoxStatus(GameManager.Instance.CheckWordCorrect(word));
            isCompleted = true;
        }
        else
        {
            Debug.Log("Not found this word in list!!");
            isCompleted = false;
            
        }
        return isCompleted;
    }

    private void DeleteLetterInBox(int index)
    {
        if (index < 0)
        {
            return;
        }

        bool success = letterBoxArrays[index].DeleteLetter();
        if(success == false)
        {
            DeleteLetterInBox(index - 1);
        }


    }

    public void ResetLetterInBox()
    {
        foreach (var letter in letterBoxArrays)
        {
            letter.DeleteLetter();
            letter.SetColor(normalColor);
        }
    }


    private void UpdateLetterInBoxStatus(WordleStatus[] status)
    {
        for (int i = 0; i < status.Length; i++)
        {
            Color color = notFoundColor;
            if (status[i] == WordleStatus.CORRECT)
            {
                color = correctColor;
            }
            else if (status[i] == WordleStatus.INCORRECT)
            {
                color = incorrectColor;
            }

            
            letterBoxArrays[i].SetColor(color);

        }
    }
}
