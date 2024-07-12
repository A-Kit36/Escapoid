using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AIState
{
    public string StateName;
    public List<AIAction> StateActions;
    public List<AITransition> StateTransitions;

    protected AIBrain _ownerBrain;

    public virtual void SetBrain(AIBrain aIBrain)
    {
        _ownerBrain = aIBrain;
        foreach(AIAction action in StateActions)
        {
            action.SetBrain(aIBrain);
        }
        foreach (AITransition transition in StateTransitions)
        {
            if (transition.AIDecision != null) 
            { 
                transition.AIDecision.SetBrain(aIBrain);
            }
        }
    }

    public virtual void StateStart()
    {
        foreach (AIAction action in StateActions)
        {
            action.OnEnterState();
        }
        foreach (AITransition transition in StateTransitions)
        {
            if (transition.AIDecision != null)
            {
                transition.AIDecision.OnEnterState();
            }
        }
    }

    public virtual void StateEnd()
    {
        foreach (AIAction action in StateActions)
        {
            action.OnExitState();
        }
        foreach (AITransition transition in StateTransitions)
        {
            if (transition.AIDecision != null)
            {
                transition.AIDecision.OnExitState();
            }
        }
    }

    public virtual void PerformActions()
    {
        foreach (var action in StateActions) 
        {
            action.Execute();
        }
    }
    public virtual void EvaluateTransitions() 
    {
        foreach (var transition in StateTransitions) 
        {
            string stateTransName = transition.AIDecision.Evaluate() ? transition.TrueState : transition.FalseState;
            if (stateTransName != null && stateTransName != "") 
            {
                _ownerBrain.ChangeBrainState(stateTransName);
            }
        }
    }

}
    

