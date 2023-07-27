using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KevinCastejon.FiniteStateMachine;

/// <summary>
/// A finite state machine to control the level. Otherwise, a bunch of booleans are needed.
/// 
/// From an asset on the store.
/// </summary>
public class LevelStateMachine : AbstractFiniteStateMachine
{
    [Tooltip("Reference to the scriptable object for level events")]
    public LevelEvents LevelEvents;

    [Tooltip("Reference to the pause menu")]
    public PauseMenu PauseMenu;

    public enum LevelState
    {
        IN_BATTLE,
        PAUSED
    }

    /// <summary>
    /// Register the states in the machine
    /// </summary>
    private void Awake()
    {
        Init(LevelState.IN_BATTLE,
            AbstractState.Create<InBattleState, LevelState>(LevelState.IN_BATTLE, this),
            AbstractState.Create<PausedState, LevelState>(LevelState.PAUSED, this)
        );
    }

    /// <summary>
    /// For testing purposes
    /// </summary>
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
            LevelEvents.Victory.Invoke();
    }

    public class InBattleState : AbstractState
    {
        public InBattleState()
        {
            LevelEvents.Instance.Pause.AddListener(() => TransitionToState(LevelState.PAUSED));
        }
        public override void OnEnter()
        {
            Time.timeScale = 1;
        }
        public override void OnUpdate()
        {
        }
        public override void OnExit()
        {
        }
    }
    public class PausedState : AbstractState
    {
        public PausedState()
        {
            LevelEvents.Instance.Resume.AddListener(() => TransitionToState(LevelState.IN_BATTLE));
        }

        public override void OnEnter()
        {
            Time.timeScale = 0;
            (this._parentStateMachine as LevelStateMachine).PauseMenu.gameObject.SetActive(true);
        }
        public override void OnUpdate()
        {
        }
        public override void OnExit()
        {
            (this._parentStateMachine as LevelStateMachine).PauseMenu.gameObject.SetActive(false);
        }
    }
}
