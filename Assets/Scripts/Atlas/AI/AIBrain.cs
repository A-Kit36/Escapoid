using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBrain : MonoBehaviour
{
    public List<AIState> BrainStates;
    public AIState CurrentBrainState;
    public float TimeInThisState;
    public Transform Target;
    public Character Character { get; private set; }

    [Header("State")]
    /// whether or not this brain is active
    public bool BrainActive = true;
    public bool ResetBrainOnStart = true;
    public bool ResetBrainOnEnable = false;

    [Header("Frequencies")]
    /// the frequency (in seconds) at which to perform actions (lower values : higher frequency, high values : lower frequency but better performance)
    public float ActionsFrequency = 0f;
    /// the frequency (in seconds) at which to evaluate decisions
    public float DecisionFrequency = 0f;

    protected AIDecision[] _decisions;
    protected AIAction[] _actions;
    protected float _lastActionsUpdate = 0f;
    protected float _lastDecisionsUpdate = 0f;
    protected AIState _initialState;

    private void Awake()
    {
        foreach (AIState state in BrainStates)
        {
            state.SetBrain(this);
        }
    }

    private void Start()
    {
        CurrentBrainState = BrainStates[0];
        CurrentBrainState.StateStart();
    }
    private void Update()
    {
        if (CurrentBrainState != null)
        {
            TimeInThisState += Time.deltaTime;
            _lastActionsUpdate += Time.deltaTime;
            _lastDecisionsUpdate += Time.deltaTime;

            if (_lastActionsUpdate >= ActionsFrequency)
            {
                if (DialogueBoxController.Instance.dialogueActive)
                {
                    return;
                }
                CurrentBrainState.PerformActions();
                _lastActionsUpdate = 0f;
            }
            if (_lastDecisionsUpdate >= DecisionFrequency)
            {
                CurrentBrainState.EvaluateTransitions();
                _lastDecisionsUpdate = 0f;
            }
        }
    }
    public virtual void ChangeBrainState(string stateTransName)
    {
        CurrentBrainState.StateEnd();
        CurrentBrainState = BrainStates.Find(p => p.StateName == stateTransName);
        CurrentBrainState.StateStart();
        TimeInThisState = 0f;
    }

    public void SetCharacter(Character dummyCharacter)
    {
        Character = dummyCharacter;
    }
}
