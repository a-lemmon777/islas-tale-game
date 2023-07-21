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
    [Tooltip("Reference to the pause menu")]
    public GameObject PauseMenuCanvas;

    public enum LevelState
    {
        IN_BATTLE,
        PAUSED
    }
    private void Awake()
    {
        Init(LevelState.IN_BATTLE,
            AbstractState.Create<InBattleState, LevelState>(LevelState.IN_BATTLE, this),
            AbstractState.Create<PausedState, LevelState>(LevelState.PAUSED, this)
        );
    }
    public class InBattleState : AbstractState
    {
        public override void OnEnter()
        {
            Time.timeScale = 1;
            (this._parentStateMachine as LevelStateMachine).PauseMenuCanvas.SetActive(false);
        }
        public override void OnUpdate()
        {
            if (Input.GetKeyDown(KeyCode.Escape)) TransitionToState(LevelState.PAUSED);
        }
        public override void OnExit()
        {
        }
    }
    public class PausedState : AbstractState
    {
        public override void OnEnter()
        {
            Time.timeScale = 0;
            (this._parentStateMachine as LevelStateMachine).PauseMenuCanvas.SetActive(true);

        }
        public override void OnUpdate()
        {
            if (Input.GetKeyDown(KeyCode.Escape)) TransitionToState(LevelState.IN_BATTLE);
        }
        public override void OnExit()
        {
        }
    }
}
