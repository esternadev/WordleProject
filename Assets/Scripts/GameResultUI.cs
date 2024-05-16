using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameResultUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_resultText;
    [SerializeField] private TextMeshProUGUI m_answerText;
    [SerializeField] private Button m_restartButton;

    private void Start()
    {
        m_restartButton.onClick.AddListener(() => {
            
            GameManager.Instance.StartGame();
            HideDialog();
        });
    }

    public void SetResult(string text, string word)
    {
        SetResultText(text);
        SetAnswerText(word);
        ShowDialog();
    }

    private void SetResultText(string text)
    {
        m_resultText.SetText(text);
    }

    private void SetAnswerText(string word)
    {
        m_answerText.SetText(word);
    }

    public void ShowDialog()
    {
        gameObject.SetActive(true);
    }

    public void HideDialog()
    {
        gameObject.SetActive(false);
    }

}
