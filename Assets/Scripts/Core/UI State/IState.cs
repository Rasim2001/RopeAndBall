
public interface IState
{
    void OnEnter(params object[] parameters);
    void OnExit();
}
