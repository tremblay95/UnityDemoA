using System;

namespace Utilities
{
    public abstract class Timer
    {
        protected float initialTime;
        protected float ElapsedTime { get; set; }
        public bool IsRunning { get; protected set; }
        
        public float Progress => ElapsedTime / initialTime;
        
        public Action OnStart = delegate { };
        public Action OnStop = delegate { };

        protected Timer(float value)
        {
            initialTime = value;
            IsRunning = false;
        }
        
        public void Start()
        {
            ElapsedTime = initialTime;
            if (!IsRunning)
            {
                IsRunning = true;
                OnStart();
            }
        }
        
        public void Stop()
        {
            if (IsRunning)
            {
                IsRunning = false;
                OnStop();
            }
        }
        
        public void Suspend() => IsRunning = false;
        public void Resume() => IsRunning = true;
        
        public abstract void Tick(float deltaTime);
    }

    public class CountdownTimer : Timer
    {
        public CountdownTimer(float value) : base(value) { }

        public override void Tick(float deltaTime)
        {
            if (IsRunning && ElapsedTime > 0)
            {
                ElapsedTime -= deltaTime;
            }
            
            if (IsRunning && ElapsedTime <= 0)
            {
                Stop();
            }
        }
        
        public bool IsFinished => ElapsedTime <= 0;
        
        public void Reset() => ElapsedTime = initialTime;
        public void Reset(float value)
        {
            initialTime = value;
            Reset();
        }
    }

    public class StopwatchTimer : Timer
    {
        public StopwatchTimer(float value) : base(value) { }
        
        public override void Tick(float deltaTime)
        {
            if (IsRunning)
            {
                ElapsedTime += deltaTime;
            }
        }
        
        public void Reset() => ElapsedTime = 0;
        public float GetTime() => ElapsedTime;
    }
}
