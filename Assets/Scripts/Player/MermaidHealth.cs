using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MermaidHealth : CharacterHealth
{

    [Tooltip("Reference to the mermaid's animation state machine")]
    public MermaidAnimator AnimationController;

    [Tooltip("Reference to the player status UI script component")]
    public MermaidHUD MermaidHUD;

    [Tooltip("Reference to the hurt sound effect")]
    public AudioSource HurtSound;

    [Tooltip("Reference to the die sound effect")]
    public AudioSource DieSound;

    /// <summary>
    /// Handles all the callbacks when the player dies
    /// </summary>
    public override void Die()
    {
        GameObject.Find("MusicManager").GetComponent<AudioSource>().Pause();
        DieSound.Play();
        this.AnimationController.HandleDeath();
    }

    public void TakeDamage(int damageValue, float source)
    {
        // Only play hurt sound if mermaid will not die.
        if (this.Health > damageValue)
        {
            HurtSound.Play();
        }

        base.TakeDamage(damageValue);
        this.AnimationController.HandleDamage(source);

        this.MermaidHUD.ChangePlayerHealth(-damageValue);
    }
}
