using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Threading.Tasks;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    public KeyboardUI KeyboardUI => keyboardUI;

    [SerializeField] private KeyboardUI keyboardUI;
    [SerializeField] private WordleBoardUI wordleBoardUI;
    [SerializeField] private GameResultUI gameResultUI;

    [SerializeField] private List<string> words = new List<string>();

    private string resultAnswer;

    private void Start()
    {
        gameResultUI.HideDialog();
        StartGame();
    }

    public void StartGame()
    {
        LoadWords();
        keyboardUI.ResetKeyboard();
        wordleBoardUI.ResetWordleBoard();
        resultAnswer = RandomAnswer();
        wordleBoardUI.SetAnswerText(resultAnswer);
    }


    public void LoadWords()
    {
        TextAsset textAsset = Resources.Load<TextAsset>("word");
        words = JsonConvert.DeserializeObject<List<string>>(textAsset.text);
    }
    public string RandomAnswer()
    {
        int index = Random.Range(0, words.Count);
        string word = words[index];
        char[] wordChars = word.ToCharArray();

        for (int i = 0;i< wordChars.Length;i++)
        {
            int numbers = wordChars.Count(x => x == wordChars[i]);
            if(numbers > 1)
            {
                return RandomAnswer();
            }
        }

        return word;
    }

    public WordleStatus[] CheckWordCorrect(string word)
    {
        WordleStatus[] wordles = new WordleStatus[5];

        char[] resultChar = resultAnswer.ToCharArray();
        char[] answerChar = word.ToCharArray();

        for (int i = 0; i < answerChar.Length; i++)
        {
            WordleStatus wordle = answerChar[i] == resultChar[i] ? WordleStatus.CORRECT : WordleStatus.NOTFOUND;

            if (wordle != WordleStatus.CORRECT)
            {
                wordle = resultAnswer.Contains(answerChar[i]) ? WordleStatus.INCORRECT : WordleStatus.NOTFOUND;
            }

            wordles[i] = wordle;

            if (wordles[i] == WordleStatus.NOTFOUND)
            {
                keyboardUI.UpdateKeyStatus(answerChar[i], wordles[i]);
            }
            else
            {
                keyboardUI.UpdateKeyStatus(answerChar[i], wordles[i]);
            }
        }

        return wordles;

    }

    public bool CheckWordHasExist(string word)
    {
        return words.Contains(word);
    }


    public bool CheckResult(string word, int lineIndex)
    {
        if(lineIndex >= wordleBoardUI.GetTotalLineCount() - 1)
        {
            //Game End
            EndGame("You Lose");
            return true;
        }
        else
        {
            bool correct = word == resultAnswer;
            if (correct == true)
            {
                EndGame("You Win");
            }
            return correct;
        }
    }

    public async void EndGame(string text)
    {
        await Task.Delay(1500);
        gameResultUI.SetResult(text, resultAnswer);
    }
}

public enum WordleStatus
{
    NOTFOUND, // not have char in word
    INCORRECT, // have char but wrong index
    CORRECT // have char and correct index
}
