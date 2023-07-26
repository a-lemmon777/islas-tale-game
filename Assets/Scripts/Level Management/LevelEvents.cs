using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Game/Level Events", fileName = "Level Events")]
public class LevelEvents : ScriptableObject
{
    public UnityEvent Pause;

    public UnityEvent Resume;

    public UnityEvent GameOver;

    public static LevelEvents Instance;

    private void OnEnable()
    {
        Instance = this;

        GameOver.AddListener(() => SceneManager.LoadScene("GameOver"));
    }

    private void OnDisable()
    {
        Pause.RemoveAllListeners();
        Resume.RemoveAllListeners();
        GameOver.RemoveAllListeners();
    }
}
