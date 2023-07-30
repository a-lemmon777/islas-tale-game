using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
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

    void Awake()
    {
        LevelEvents.OpenTutorial.AddListener(() =>
        {
            Time.timeScale = 0;
            TutorialScreen.SetActive(true);
            MermaidHUDCanvas.SetActive(false);
        });

        LevelEvents.CloseTutorial.AddListener(() =>
        {
            Time.timeScale = 1;
            TutorialScreen.SetActive(false);
            MermaidHUDCanvas.SetActive(true);
            LevelEvents.Instance.Start.Invoke();
        });

        LevelEvents.Pause.AddListener(() =>
        {
            PauseSound.Play();
            Time.timeScale = 0;
            PauseMenu.gameObject.SetActive(true);
        });

        LevelEvents.Resume.AddListener(() =>
        {
            Time.timeScale = 1;
            PauseSound.Play();
            PauseMenu.gameObject.SetActive(false);
        });
    }

    private void Start()
    {

        if (StartWithTutorial)
            LevelEvents.OpenTutorial.Invoke();
        else
            LevelEvents.Start.Invoke();
    }
}
