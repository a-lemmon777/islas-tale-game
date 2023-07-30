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

    [Tooltip("Reference to the pause sound effect")]
    public AudioSource PauseSound;

    [Tooltip("Reference to the mermaid status canvas")]
    public GameObject MermaidHUDCanvas;

    [Tooltip("Reference to the tutorial prefab root, disabled by default")]
    public GameObject TutorialScreen;

    [Tooltip("Whether to start the game with the tutorial")]
    public bool StartWithTutorial;

    public enum LevelState
    {
        IN_BATTLE,
        PAUSED,
        IN_TUTORIAL
    }

    /// <summary>
    /// Register the states in the machine
    /// </summary>
    private void Awake()
    {
        Init(LevelState.IN_BATTLE,
            AbstractState.Create<InBattleState, LevelState>(LevelState.IN_BATTLE, this),
            AbstractState.Create<PausedState, LevelState>(LevelState.PAUSED, this),
            AbstractState.Create<InTutorialState, LevelState>(LevelState.IN_TUTORIAL, this)
        );
    }

    private void Start()
    {
        if (StartWithTutorial)
            LevelEvents.OpenTutorial.Invoke();
        else
            LevelEvents.Start.Invoke();
    }

    public class InBattleState : AbstractState
    {

        public InBattleState()
        {
            LevelEvents.Instance.Pause.AddListener(() => TransitionToState(LevelState.PAUSED));
            LevelEvents.Instance.OpenTutorial.AddListener(() => TransitionToState(LevelState.IN_TUTORIAL));
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
            (this._parentStateMachine as LevelStateMachine).PauseSound.Play();
            Time.timeScale = 0;
            (this._parentStateMachine as LevelStateMachine).PauseMenu.gameObject.SetActive(true);
        }
        public override void OnUpdate()
        {
        }
        public override void OnExit()
        {
            Time.timeScale = 1;
            (this._parentStateMachine as LevelStateMachine).PauseSound.Play();
            (this._parentStateMachine as LevelStateMachine).PauseMenu.gameObject.SetActive(false);
        }
    }

    public class InTutorialState : AbstractState
    {

        public InTutorialState()
        {
            LevelEvents.Instance.CloseTutorial.AddListener(() => TransitionToState(LevelState.IN_BATTLE));
        }

        public override void OnEnter()
        {
            Time.timeScale = 0;
            (this._parentStateMachine as LevelStateMachine).TutorialScreen.SetActive(true);
            (this._parentStateMachine as LevelStateMachine).MermaidHUDCanvas.SetActive(false);
        }

        public override void OnExit()
        {
            Time.timeScale = 1;
            (this._parentStateMachine as LevelStateMachine).TutorialScreen.SetActive(false);
            (this._parentStateMachine as LevelStateMachine).MermaidHUDCanvas.SetActive(true);
            LevelEvents.Instance.Start.Invoke();
        }
    }
}
