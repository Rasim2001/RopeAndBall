public interface IMenuService
{
    void GoToScreenOfType<T>(params object[] parameters) where T : UIState;
    void GoToPreviousScreen();
    void ClearUndoStack();
}
