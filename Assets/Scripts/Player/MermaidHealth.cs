using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MermaidHealth : CharacterHealth
{

    [Tooltip("Reference to the mermaid's animation state machine")]
    public MermaidAnimator AnimationController;

    public override void Die()
    {
        this.AnimationController.HandleDeath();
    }


}
