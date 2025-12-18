using System.Collections.Generic;

namespace UnityDemoA
{
    public abstract class TargetingStrategy
    {
        protected TargetingManager _targetingManager;

        public bool Begin(TargetingManager targetingManager)
        {
            if (!targetingManager) { return false; }
            _targetingManager = targetingManager;

            return Begin();
        }
        protected abstract bool Begin();
        public virtual IEnumerable<ITargetable> Update() { return null; }
        public virtual void End() { }
    }
}
