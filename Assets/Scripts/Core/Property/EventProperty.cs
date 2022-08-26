using System;

namespace Graf.Properties
{
    public class EventProperty<T>
    {
        T property;
        public event Action<T> OnPropertyChange;

        public T Value
        {
            get
            {
                return property;
            }
            set
            {
                property = value;
                OnPropertyChange?.Invoke(property);
            }
        }
    }
}