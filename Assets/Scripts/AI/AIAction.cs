using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIAction : MonoBehaviour
{
    public AIBrain Brain { get; private set; }
    public abstract void Execute();
    public abstract void OnEnterState();
    public abstract void OnExitState();

    public virtual void SetBrain(AIBrain aIBrain)
    {
        Brain = aIBrain;
    }
}
