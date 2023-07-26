using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MermaidHUD : MonoBehaviour
{

    public enum HealthStatus
    {
        FULL, // when more than 60%
        HALF, // when between 60% and 30%
        LOW, // when under 30%
    }

    [Tooltip("Mermaid status picture component")]
    public Image MermaidStatus;

    [Tooltip("Health bar slider component")]
    public Slider HealthBar;

    [Tooltip("Mermaid health component")]
    public MermaidHealth PlayerHealth;

    [Tooltip("Sprite on disk for full health image")]
    public Sprite FullHealth;

    [Tooltip("Sprite on disk for half health image")]
    public Sprite HalfHealth;

    [Tooltip("Sprite on disk for low health image")]
    public Sprite LowHealth;

    [Tooltip("Rect transform position of the status icon")]
    public Vector2 StatusPos;

    [Tooltip("Rect transform position of the hp bar")]
    public Vector2 HPPos;

    public RectTransform Status;

    public RectTransform HPBar;

    // Start is called before the first frame update
    void Start()
    {
        this.HealthBar.maxValue = this.PlayerHealth.MaxHealth;
        this.HealthBar.value = this.PlayerHealth.Health;

        this.Status.anchoredPosition = StatusPos;
        this.HPBar.anchoredPosition = HPPos;
    }

    /// <summary>
    /// Change the mermaid status sprite
    /// </summary>
    /// <param name="status">Full (>60%), half(30 to 60%), or low (<30%)</param>
    private void ChangeHealthStatus(HealthStatus status)
    {
        switch (status)
        {
            case HealthStatus.FULL:
                this.MermaidStatus.sprite = this.FullHealth;
                break;

            case HealthStatus.HALF:
                this.MermaidStatus.sprite = this.HalfHealth;
                break;

            case HealthStatus.LOW:
                this.MermaidStatus.sprite = this.LowHealth;
                break;

            default:
                break;
        }
    }

    /// <summary>
    /// Update the UI according to the change in hp
    /// </summary>
    /// <param name="change"></param>
    public void ChangePlayerHealth(int change)
    {
        var maxHP = this.PlayerHealth.MaxHealth;
        var newHealth = this.HealthBar.value + change;

        if (newHealth < 0) newHealth = 0;

        switch (newHealth / maxHP)
        {
            case < 0.30f:
                ChangeHealthStatus(HealthStatus.LOW);
                break;

            case > 0.60f:
                ChangeHealthStatus(HealthStatus.FULL);
                break;

            default:
                ChangeHealthStatus(HealthStatus.HALF);
                break;
        }

        this.HealthBar.value = newHealth;
    }
}
