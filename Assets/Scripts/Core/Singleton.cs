namespace Graf.Utils
{
    class Singlton<T> where T : Singlton<T>
    {
        public static T Instance;

        public Singlton()
        {
            if (Instance == null)
            {
                Instance = (T)this;
            }
        }
    }
}