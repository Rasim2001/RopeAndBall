using System;
using System.Collections.Generic;

public class StateSwitcher
{
    private IState currentState;
    private readonly List<IState> registeredStates;
    private readonly Stack<StateSwitchCommand> switchingHistory;
    private StateSwitchCommand previousStateSwitchCommand;

    public StateSwitcher()
    {
        registeredStates = new List<IState>();
        switchingHistory = new Stack<StateSwitchCommand>();
    }

    public void ClearUndoStack()
    {
        switchingHistory.Clear();
    }

    public void AddState(IState state)
    {
        if (registeredStates.Contains(state)) return;
        registeredStates.Add(state);
    }


    public void GoToState<T>(params object[] parameters)
    {
        GoToState(typeof(T), parameters);
    }

    public void GoToState(Type type, params object[] parameters)
    {
        Type targetType = type;
        if (currentState != null)
            if (currentState.GetType() == targetType) return;
        foreach (var item in registeredStates)
        {
            if (item.GetType() != targetType) continue;
            if (currentState != null)
                currentState.OnExit();
            currentState = item;
            currentState.OnEnter(parameters);
            RegStateSwitching(targetType, parameters);
        }
    }

    public void GoToPreviousState()
    {
        if (switchingHistory.Count < 1) return;
        StateSwitchCommand destination = switchingHistory.Pop();
        previousStateSwitchCommand = null;
        GoToState(destination.stateType, destination.parameters);
    }

    private void RegStateSwitching(Type type, params object[] parameters)
    {
        if (previousStateSwitchCommand != null)
            switchingHistory.Push(previousStateSwitchCommand);
        previousStateSwitchCommand = new StateSwitchCommand(type, parameters);
    }

    private class StateSwitchCommand
    {
        public StateSwitchCommand(Type type, params object[] parameters)
        {
            stateType = type;
            this.parameters = parameters;
        }
        public readonly Type stateType;
        public readonly object[] parameters;
    }
}
