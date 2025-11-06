using System;
using System.Collections.Generic;

namespace UnityDemoA
{
    public class StateMachine
    {
        private StateNode current;
        private Dictionary<Type, StateNode> nodes = new();
        private HashSet<ITransition> anyTransitions = new();

        public void Update()
        {
            var transition = GetTransition();
            if (transition != null)
            {
                ChangeState(transition.TargetState);
            }
            
            current.State?.OnUpdate();
        }
        
        public void FixedUpdate()
        {
            current.State?.OnFixedUpdate();
        }

        public void SetState(IState state)
        {
            current = nodes[state.GetType()];
            current.State?.OnEnter();
        }
        
        public void AddTransition(IState sourceState, IState targetState, IPredicate condition)
        {
            GetOrAddNode(sourceState).AddTransition(GetOrAddNode(targetState).State, condition);
        }
        
        public void AddAnyTransition(IState targetState, IPredicate condition)
        {
            anyTransitions.Add(new Transition(GetOrAddNode(targetState).State, condition));
        }

        private StateNode GetOrAddNode(IState state)
        {
            var node = nodes.GetValueOrDefault(state.GetType());
            
            if (node == null)
            {
                node = new StateNode(state);
                nodes.Add(state.GetType(), node);
            }
            
            return node;
        }

        private void ChangeState(IState state)
        {
            if (state == current.State) { return; }
            
            var previousState = current.State;
            var nextState = nodes[state.GetType()].State;
            
            previousState?.OnExit();
            nextState?.OnEnter();
            
            current = nodes[state.GetType()];
        }
        
        private ITransition GetTransition()
        {
            foreach (var transition in anyTransitions)
            {
                if (transition.Condition.Evaluate())
                {
                    return transition;
                }
            }

            foreach (var transition in current.Transitions)
            {
                if (transition.Condition.Evaluate())
                {
                    return transition;
                }
            }

            return null;
        }

        private class StateNode
        {
            public IState State { get; }
            public HashSet<ITransition> Transitions { get; }
            
            public StateNode(IState state)
            {
                State = state;
                Transitions = new HashSet<ITransition>();
            }

            public void AddTransition(IState targetState, IPredicate condition)
            {
                Transitions.Add(new Transition(targetState, condition));
            }
        }
    }
}