using System.Collections;
using System.Collections.Generic;
using ImprovedTimers;
using UnityEngine;

namespace UnityDemoA
{
    public class ComboAbilityContext : AbilityContext
    {
        private readonly ComboAbilityDefinition _comboDefinition;
        private readonly CountdownTimer _resetTimer;
        private readonly CountdownTimer _cooldownTimer;

        private readonly Queue<SingleAbilityDefinition>  _comboQueue = new();
        private int _comboCount = 0;

        public ComboAbilityContext(ComboAbilityDefinition comboDefinition)
        {
            _comboDefinition = comboDefinition;

            _resetTimer = new CountdownTimer(comboDefinition.resetTime);
            _resetTimer.OnTimerStop += ResetCombo;

            _cooldownTimer = new CountdownTimer(comboDefinition.cooldownTime);
            _cooldownTimer.OnTimerStop += ResetCombo;
        }

        protected override SingleAbilityDefinition GetAbilityDefinition() =>
            _comboQueue.TryPeek(out var abilityDefinition) ? abilityDefinition : null;

        public override void CastAbility(TargetingManager targetingManager)
        {
            if (_comboCount >= _comboDefinition.comboAbilityList.Count || _cooldownTimer.IsRunning) { return; }

            _comboQueue.Enqueue(_comboDefinition.comboAbilityList[_comboCount]);
            _comboCount++;

            if (_resetTimer.IsRunning) { CancelResetTimer(); }

            if (!IsCasting) { targetingManager.StartCoroutine(Cast(targetingManager)); }
        }

        private IEnumerator Cast(TargetingManager targetingManager)
        {
            while (_comboQueue.Count > 0)
            {
                PerformCast(targetingManager);
                yield return new WaitUntil(() => !IsCasting);
                _comboQueue.Dequeue();
            }
        }

        protected override void OnCastComplete()
        {
            ClearCoroutine();
            if (_comboCount < _comboDefinition.comboAbilityList.Count) { _resetTimer.Start(); }
            else { _cooldownTimer.Start(); }
        }

        protected override void OnCastCancelled()
        {
            base.OnCastCancelled();
            ResetCombo();
        }

        private void ResetCombo()
        {
            _comboQueue.Clear();
            _comboCount = 0;
        }

        private void CancelResetTimer()
        {
            _resetTimer.OnTimerStop -= ResetCombo;
            _resetTimer.Stop();
            _resetTimer.OnTimerStop += ResetCombo;
        }
    }
}