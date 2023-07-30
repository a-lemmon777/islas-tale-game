using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    public GameObject Object;

    void Awake()
    {
        DontDestroyOnLoad(Object);
    }
}
