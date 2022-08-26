using Graf.Properties;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyManager
{
    public TextMeshProUGUI moneyTextShop;

    private EventManager eventManager;
    private TextMeshProUGUI moneyText;
    private FloatHashed floatHashed = new FloatHashed("Money");

    public float CurrentMoney = 0;
    public void Init(EventManager eventManager, TextMeshProUGUI moneyText)
    {
        this.eventManager = eventManager;
        this.moneyText = moneyText;
        InitAction();
        moneyText.text = floatHashed.Value.ToString();
    }
    private void InitAction()
    {
        eventManager.OnMoneyChangeEvent.AddListener(ChangeMoney);
    }

    private void ChangeMoney(float money)
    {
        CurrentMoney += money;
        floatHashed.Value += money;
        PrintMoney();
    }
    public void GameBegin()
    {
        CurrentMoney = 0;
    }
    public void PrintMoney()
    {
        moneyText.text = floatHashed.Value.ToString();
        moneyTextShop.text = floatHashed.Value.ToString();
    }
}
