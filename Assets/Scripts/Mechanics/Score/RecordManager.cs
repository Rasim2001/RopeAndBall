using Graf.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;
public class RecordManager 
{
    private TextMeshProUGUI recordMainMenuText;
    private TextMeshProUGUI recordGameOverText;
    private ScoreManager scoreManager;
    private EventManager eventManager;

    private FloatHashed floatHashed = new FloatHashed("Record");

    public bool IsRecord;
    [Inject]
    public RecordManager(ScoreManager scoreManager, EventManager eventManager)
    {
        this.scoreManager = scoreManager;
        this.eventManager = eventManager;

        InitAction();
    }

    public void InitMainMenu(TextMeshProUGUI recordText)
    {
        recordMainMenuText = recordText;
        recordMainMenuText.text = floatHashed.Value.ToString();
    }

    public void InitGameOverMenu(TextMeshProUGUI recordText)
    {
        recordGameOverText = recordText;
        recordGameOverText.text = floatHashed.Value.ToString();
    }

    private void InitAction()
    {
        eventManager.OnRecordChangeEvent.AddListener(ChangeRecord);
    }

    private void ChangeRecord()
    {
        if (floatHashed.Value < scoreManager.score)
        {
            floatHashed.Value = scoreManager.score;
            IsRecord = true;
        }
        recordMainMenuText.text = floatHashed.Value.ToString();
        recordGameOverText.text = floatHashed.Value.ToString();
    }
}
