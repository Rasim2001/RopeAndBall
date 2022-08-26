using Graf.Properties;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameOverView : UIShowableHidable
{
    [SerializeField]
    private Button RestartButton;

    [SerializeField]
    private Button MainMenuButton;

    [SerializeField]
    private Button ShopButton;

    [SerializeField]
    private TextMeshProUGUI HighScoreText;
    [SerializeField]
    private TextMeshProUGUI moneyCollectedText;
    [SerializeField]
    private TextMeshProUGUI moneyAllText;
    [SerializeField]
    private TextMeshProUGUI scoreText;

    public GameObject HighScorePanel;
    public TextMeshProUGUI recordText;

    public UnityAction OnRestartButton;
    public UnityAction OnMainMenuButton;
    public UnityAction OnShopButton;

    private FloatHashed floatHashed = new FloatHashed("Money");
    public void Init()
    {
        RestartButton.onClick.AddListener(() => OnRestartButton?.Invoke());
        MainMenuButton.onClick.AddListener(() => OnMainMenuButton?.Invoke());
        ShopButton.onClick.AddListener(() => OnShopButton?.Invoke());
    }

    public void PrintMoney(float collectedMoney)
    {
        StartCoroutine(TextAnimCollectedMoney(collectedMoney));
        moneyAllText.text = floatHashed.Value.ToString();
    }

    public void PrintScore(float score)
    {
        scoreText.text = score.ToString();
    }
    public void PrintHighScoreText(bool isRecord)
    {
        if (isRecord)
            StartCoroutine(HighScoreAnimText());
    }

    private IEnumerator HighScoreAnimText()
    {
        HighScorePanel.SetActive(true);
        HighScoreText.text = "";
        string temp = "New High Score";
        for(int i = 0; i < temp.Length; i++)
        {
            HighScoreText.text += temp[i];
            yield return new WaitForSeconds(0.15f);
        }
    }

    private IEnumerator TextAnimCollectedMoney(float collectedMoney)
    {
       if(collectedMoney == 0)
            moneyCollectedText.text = "0000";
        
       for(int i = 0; i <= collectedMoney; i++)
        {
            moneyCollectedText.text = i.ToString("D4");
            yield return new WaitForEndOfFrame();
        }
    }

}
