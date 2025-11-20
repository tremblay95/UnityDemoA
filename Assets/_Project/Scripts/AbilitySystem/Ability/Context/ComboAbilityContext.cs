using System.Collections;
using System.Collections.Generic;
using ImprovedTimers;
using UnityEngine;

namespace UnityDemoA
{
    public class ComboAbilityContext : AbilityContext
    {
        private readonly ComboDefinition _comboDefinition;
        private readonly CountdownTimer _resetTimer;
        private readonly CountdownTimer _cooldownTimer;
        
        private Queue<AbilityDefinition>  _comboQueue = new();
        private int _comboCount = 0;

        public ComboAbilityContext(ComboDefinition comboDefinition)
        {
            _comboDefinition = comboDefinition;
            _resetTimer = new CountdownTimer(comboDefinition.resetTime);
            _cooldownTimer = new CountdownTimer(comboDefinition.cooldownTime);
        }
        
        public override void CastAbility(TargetingManager targetingManager)
        {
            
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
        
        protected override AbilityDefinition GetAbilityDefinition()
        {
            return _comboQueue.Count > 0 ? _comboQueue.Peek() : null;
        }

        protected override void OnAbilityCastComplete()
        {
            _resetTimer.Start();
            if (_comboCount >= _comboDefinition.comboAbilityList.Count)
            {
                _comboCount = 0;
                _cooldownTimer.Stop();
            }
        }
    }
}