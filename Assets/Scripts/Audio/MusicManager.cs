using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    public AudioClip TitleAndCreditsMusic;
    public AudioClip LevelMusic;
    public AudioClip GameOverMusic;
    public AudioClip VictoryMusic;

    public AudioSource MusicSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if ((scene.name == "MainMenu" || scene.name == "Credits") && MusicSource.clip != TitleAndCreditsMusic)
        {
            MusicSource.clip = TitleAndCreditsMusic;
            MusicSource.loop = true;
            MusicSource.Play();
        }
        if (scene.name == "Player Mermaid")
        {
            MusicSource.clip = LevelMusic;
            MusicSource.loop = true;
            MusicSource.Play();
        }
        if (scene.name == "GameOver")
        {
            MusicSource.clip = GameOverMusic;
            MusicSource.loop = false;
            MusicSource.Play();
        }
        if (scene.name == "Victory")
        {
            MusicSource.clip = VictoryMusic;
            MusicSource.loop = false;
            MusicSource.Play();
        }
    }
}
