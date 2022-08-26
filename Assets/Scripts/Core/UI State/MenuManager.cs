using System;

public class MenuManager : IMenuService
{
    private readonly StateSwitcher stateSwitcher;

    public MenuManager()
    {
        stateSwitcher = new StateSwitcher();
    }

    public MenuManager(params UIState[] states) : this()
    {
        foreach (var item in states)
        {
            stateSwitcher.AddState(item);
        }
    }



    public void GoToScreenOfType<T>(params object[] parameters) where T : UIState
    {
        stateSwitcher.GoToState<T>(parameters);
    }

    public void GoToScreenOfType(Type type, params object[] parameters)
    {
        stateSwitcher.GoToState(type, parameters);
    }

    public void GoToPreviousScreen()
    {
        stateSwitcher.GoToPreviousState();
    }

    public void ClearUndoStack()
    {
        stateSwitcher.ClearUndoStack();
    }
}