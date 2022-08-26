using Zenject;

public class MainMenuManager
{
    public static MenuManager menuManager;

    [Inject]
    public MainMenuManager(GameState gameState, 
                           MenuState menuState, 
                           SettingsState settingsState, 
                           ShopState shopState, 
                           GameOverState gameOverState)
    {
        menuManager = new MenuManager(gameState, menuState, settingsState , shopState, gameOverState);
        menuManager.GoToScreenOfType<MenuState>();
    }
}

