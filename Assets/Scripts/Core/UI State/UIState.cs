public abstract class UIState : IState
{
    protected abstract IUIShowableHidable ShowableHidable { get; set; }
    protected abstract void Enter(params object[] parameters);
    protected abstract void Exit();

    public virtual void OnExit()
    {
        ShowableHidable.HideUI();
        Exit();
    }

    public virtual void OnEnter(params object[] parameters)
    {
        ShowableHidable.ShowUI();
        Enter(parameters);
    }
}