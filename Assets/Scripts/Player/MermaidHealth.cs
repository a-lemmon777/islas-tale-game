using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MermaidHealth : CharacterHealth
{

    [Tooltip("Reference to the mermaid's animation state machine")]
    public MermaidAnimator AnimationController;

    [Tooltip("Reference to the player status UI script component")]
    public MermaidHUD MermaidHUD;

    /// <summary>
    /// Handles all the callbacks when the player dies
    /// </summary>
    public override void Die()
    {
        this.AnimationController.HandleDeath();
    }

    public override void TakeDamage(int damageValue)
    {
        base.TakeDamage(damageValue);

        this.MermaidHUD.ChangePlayerHealth(-damageValue);
    }
}
