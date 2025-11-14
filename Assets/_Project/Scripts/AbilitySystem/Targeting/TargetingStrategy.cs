namespace UnityDemoA
{
    public abstract class TargetingStrategy
    {
        protected TargetingManager _targetingManager;
        protected bool _isTargeting = false;
        public bool IsTargeting => _isTargeting;

        public void BeginTargeting(TargetingManager targetingManager)
        {
            targetingManager.ClearTargets();
            targetingManager.Reset();
            _targetingManager = targetingManager;
            
            Start();
        }
        protected abstract void Start();
        public virtual void Update() { }
        public virtual void Cancel() { }
    }
}