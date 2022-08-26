using Graf.Pool;
using Graf.Properties;
using Graf.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;
public class GameState : UIState
{
    private GameView gameView;

    private WeightController weightController;
    private MapGenerator mapGenerator;
    private ResourceLoaderService<GameObject> resourceLoaderService;

    private PauseMenuState pauseMenuState;
    private ADSMenuState adsMenuState;

    private EventManager eventManager;
    private InputManager inputManager;
    private ScoreManager scoreManager;
    private MoneyManager moneyManager;
    private Camera camera;

    private UnityAction OnPauseMenuButton;
    protected override IUIShowableHidable ShowableHidable { get; set; }

    

    [Inject]
    public GameState(ResourceLoaderService<GameObject> resourceLoaderService,
                    [Inject(Id = "UIViewContainer")] Transform container,
                    WeightController weightController,
                    MapGenerator mapGenerator,
                    EventManager eventManager,
                    ScoreManager scoreManager,
                    MoneyManager moneyManager,
                    PauseMenuState pauseMenuState,
                    ADSMenuState adsMenuState
                    )
    {
       
        gameView = GameObject.Instantiate(resourceLoaderService.Prefabs.GAME_VIEW, container).GetComponent<GameView>();
        gameView.Init();
        ShowableHidable = gameView;
        ShowableHidable.Instantiate();

        this.pauseMenuState = pauseMenuState;
        this.adsMenuState = adsMenuState;
        this.moneyManager = moneyManager;
        this.scoreManager = scoreManager;
        this.eventManager = eventManager;
        this.resourceLoaderService = resourceLoaderService;
        this.mapGenerator = mapGenerator;
        this.weightController = weightController;

        this.pauseMenuState.Init(resourceLoaderService, gameView.transform);
        this.adsMenuState.Init(resourceLoaderService, gameView.transform, eventManager);
        moneyManager.Init(eventManager, gameView.moneyText);
        scoreManager.Init(eventManager, gameView.scoreText); 

        camera = Camera.main.GetComponent<Camera>();
        InitActions();
    }


    private void InitActions()
    {
        OnPauseMenuButton += pauseMenuState.OpenPauseMenu;
        eventManager.OnGameOver.AddListener(() => MainMenuManager.menuManager.GoToScreenOfType<GameOverState>());
        eventManager.OnOpenADSMenu.AddListener(adsMenuState.OpenADSMenu);
    }


    protected override void Enter(params object[] parameters)
    {
        adsMenuState.CloseADSMenu(); 
        gameView.OnPauseMenuButton += this.OnPauseMenuButton;
        GameBegin();
    }

    protected override void Exit()
    {
       gameView.OnPauseMenuButton -= this.OnPauseMenuButton;
       ClearGameBoard();
    }
    private void GameBegin()
    {
        camera.transform.position = new Vector3(0f, 0f, -10f); //TO-DO
        weightController.GameBegin();
        mapGenerator.GameBegin();
        moneyManager.GameBegin();
        CreateInputManager(eventManager);
    }


    private void CreateInputManager(EventManager eventManager)
    {
        if(inputManager == null)
        {
            inputManager = GameObject.Instantiate(resourceLoaderService.Prefabs.INPUT_MANAGER).GetComponent<InputManager>();
            inputManager.Init(eventManager);
        }
        else
        {
            inputManager.ActiveElement();
        }
       
    }

    private void ClearGameBoard() // TempFunction
    {
        inputManager.InActiveElement();
        weightController.DestroyElement();
        eventManager.SendRecordChangeEvent();
        foreach (var pool in mapGenerator.poolObjectViewList)
        {
            if (pool.GetComponent<MainStayView>())
                pool.GetComponent<Collider2D>().enabled = true; // TO-DO temp function
            
            pool.GetComponent<PoolObject>().Return();
        }
       
    }
}
