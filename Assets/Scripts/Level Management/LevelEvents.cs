using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Game/Level Events", fileName = "Level Events")]
public class LevelEvents : ScriptableObject
{
    public UnityEvent Pause;

    public UnityEvent Resume;

    public static LevelEvents Instance;

    private void OnEnable()
    {
        Instance = this;
    }

    private void OnDisable()
    {
        Pause.RemoveAllListeners();
        Resume.RemoveAllListeners();
    }
}
