using Graf.Properties;
using Graf.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;
public class GameOverState : UIState
{
    private ResourceLoaderService<GameObject> resourceLoaderService;
    private GameOverView gameOverView;
    private EventManager eventManager;
    private MoneyManager moneyManager;
    private ScoreManager scoreManager;
    private RecordManager recordManager;


    private UnityAction OnRestartButton;
    private UnityAction OnMainMenuButton;
    private UnityAction OnShopButton;
    protected override IUIShowableHidable ShowableHidable { get; set ; }

    private FloatHashed floatHashedRecord = new FloatHashed("Record");

    [Inject]
    public GameOverState(ResourceLoaderService<GameObject> resourceLoaderService,
                         [Inject(Id = "UIViewContainer")] Transform container,
                         EventManager eventManager,
                         RecordManager recordManager,
                         MoneyManager moneyManager,
                         ScoreManager scoreManager
                         )
    {
        gameOverView = GameObject.Instantiate(resourceLoaderService.Prefabs.GAME_OVER_VIEW.GetComponent<GameOverView>(), container);
        gameOverView.Init();
        ShowableHidable = gameOverView;
        ShowableHidable.Instantiate();

        this.moneyManager = moneyManager;
        this.eventManager = eventManager;
        this.scoreManager = scoreManager;
        this.recordManager = recordManager;
        this.recordManager.InitGameOverMenu(gameOverView.recordText);
        InitAction();
    }

    private void InitAction()
    {
        OnRestartButton = () => MainMenuManager.menuManager.GoToScreenOfType<GameState>();
        OnMainMenuButton = () => MainMenuManager.menuManager.GoToScreenOfType<MenuState>();
        OnShopButton = () => MainMenuManager.menuManager.GoToScreenOfType<ShopState>();
    }

    protected override void Enter(params object[] parameters)
    {
        gameOverView.PrintMoney(moneyManager.CurrentMoney);
        gameOverView.PrintScore(scoreManager.score);
        gameOverView.PrintHighScoreText(recordManager.IsRecord);

        gameOverView.OnRestartButton += this.OnRestartButton;
        gameOverView.OnMainMenuButton += this.OnMainMenuButton;
        gameOverView.OnShopButton += this.OnShopButton;

    }

    protected override void Exit()
    {
        recordManager.IsRecord = false;
        eventManager.SendScoreChangeEvent(0);
        gameOverView.HighScorePanel.SetActive(false);

        gameOverView.OnRestartButton -= this.OnRestartButton;
        gameOverView.OnMainMenuButton -= this.OnMainMenuButton;
        gameOverView.OnShopButton -= this.OnShopButton;
    }
}
