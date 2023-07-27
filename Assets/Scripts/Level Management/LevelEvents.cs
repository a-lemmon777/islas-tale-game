using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Game/Level Events", fileName = "Level Events")]
public class LevelEvents : ScriptableObject
{
    public UnityEvent Pause;

    public UnityEvent Resume;

    public UnityEvent GameOver;

    public UnityEvent Victory;

    public static LevelEvents Instance;

    private void OnEnable()
    {
        Instance = this;

        GameOver.AddListener(() => SceneManager.LoadScene("GameOver"));
        Victory.AddListener(() => SceneManager.LoadScene("Victory"));
    }

    private void OnDisable()
    {
        Pause.RemoveAllListeners();
        Resume.RemoveAllListeners();
        GameOver.RemoveAllListeners();
    }
}
