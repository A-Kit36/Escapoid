using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIDecision : MonoBehaviour
{
    public AIBrain Brain { get; private set; }
    public abstract bool Evaluate();
    public abstract void OnEnterState();
    public abstract void OnExitState();

    public virtual void SetBrain(AIBrain aIBrain)
    {
        Brain = aIBrain;
    }

}
