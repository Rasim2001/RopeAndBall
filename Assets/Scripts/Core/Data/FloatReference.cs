using System;
using UnityEngine;

[Serializable]
public class FloatReference
{
    public Action OnChange;
    public float MaxValue;
    public float MinValue;
    public bool UseRange = false;
    public FloatVariable Variable;

    public float Value
    {
        get => Variable.Value;
        set
        {
            if (UseRange)
            {
                if (value > MaxValue)
                    Variable.Value = MaxValue;
                else if (value < MinValue)
                    Variable.Value = MinValue;
                else
                    Variable.Value = value;
            }
            else
            {
                Variable.Value = value;
            }

            OnChange?.Invoke();
        }
    }
}
