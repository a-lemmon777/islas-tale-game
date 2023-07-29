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

    /// <summary>
    /// Handles all the callbacks when the player dies
    /// </summary>
    public override void Die()
    {
        this.AnimationController.HandleDeath();
    }

    public void TakeDamage(int damageValue, float source)
    {
        HurtSound.Play();
        base.TakeDamage(damageValue);
        this.AnimationController.HandleDamage(source);

        this.MermaidHUD.ChangePlayerHealth(-damageValue);
    }
}
