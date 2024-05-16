using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WordleBoardUI : MonoBehaviour
{
    [SerializeField] private bool showAnswer;
    [SerializeField] private bool showSurrender;
    [SerializeField] private WordleLineViewport[] wordleLines;
    [SerializeField] private TextMeshProUGUI answerText;
    [SerializeField] private TextMeshProUGUI statusText;
    [SerializeField] private GameObject statusBackground;
    [SerializeField] private Button surrenderBtn;

    private int index = 0;

    private void Start()
    {
        answerText.gameObject.SetActive(showAnswer);
        surrenderBtn.gameObject.SetActive(showSurrender);
        statusBackground.gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        surrenderBtn.onClick.AddListener(() => GameManager.Instance.EndGame("You Surrender"));
        GameManager.Instance.KeyboardUI.onPlayerType += OnPlayerTyped;
    }

    public int GetTotalLineCount()
    {
        return wordleLines.Length;
    }

    public void ResetWordleBoard()
    {
        foreach (var line in wordleLines)
        {
            line.ResetLetterInBox();
        }
    }

    public void SetAnswerText(string answer)
    {
        answerText.SetText(answer);
    }

    public IEnumerator SetStatusText(string text)
    {
        statusBackground.gameObject.SetActive(true);
        yield return new WaitForEndOfFrame();

        statusText.SetText(text);

        yield return new WaitForSeconds(2f);
        statusText.SetText("");
        statusBackground.gameObject.SetActive(false);
    }

    private void OnPlayerTyped(KeyboardLayout word)
    {
        switch (word)
        {
            case KeyboardLayout.ENTER:
                EnterWord();
                break;

            case KeyboardLayout.DELETE:
                DeleteCharacter();
                break;
            default:
                wordleLines[index].InputLetterHandler(word);
                break;

        }
    }

    private void EnterWord()
    {
        bool success = wordleLines[index].SubmitWord(out string word);
        if (success == true)
        {
            if(GameManager.Instance.CheckResult(word, index) == false)
            {
                StartCoroutine(SetStatusText("this word is exist but something wrong!!"));
                index++;
            }
            else
            {
                StartCoroutine(SetStatusText("correct!!"));
            }
        }
        else
        {
            Debug.Log("Try New Word");
            StartCoroutine(SetStatusText("this word not exist in list!!"));
        }
    }

    private void DeleteCharacter()
    {
        wordleLines[index].DeleteLetter();
    }

    private void OnDisable()
    {
        GameManager.Instance.KeyboardUI.onPlayerType -= OnPlayerTyped;
        surrenderBtn.onClick.RemoveAllListeners();
    }
}
