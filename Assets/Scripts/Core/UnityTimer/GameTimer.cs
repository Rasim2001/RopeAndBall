using System;

namespace Graf.Timer
{
    public class GameTimer
    {
        public event Action OnTimerEnd;
        public event Action<int, int> OnTimerTick;

        private int maxTime;

        private int time;
        public int Time
        {
            get => time;
            private set
            {
                time = value;
                var minutes = (int)TimeSpan.FromSeconds(time).TotalMinutes;
                var seconds = (int)TimeSpan.FromSeconds(time).TotalSeconds - 60 * minutes;

                OnTimerTick?.Invoke(minutes, seconds);

                if (time == 0)
                {
                    OnTimerEnd?.Invoke();
                }
            }
        }

        protected Timer timer;
        protected void Timer_Tick()
        {
            Time--;
        }

        public GameTimer(int maxTime)
        {
            this.maxTime = maxTime;
            Time = maxTime;
            timer = Timer.Register(1f, Timer_Tick, isLooped: true);
        }

        public void Cancel()
        {
            timer.Cancel();
        }

        public void Pause()
        {
            timer.Pause();
        }

        public void Resume()
        {
            timer.Resume();
        }

        public int GetRemainingTime()
        {
            return Time;
        }

        public int GetElapsedTime()
        {
            return maxTime - Time;
        }
    }
}