using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Parent class that holds basic features of HP
/// </summary>
abstract public class CharacterHealth : MonoBehaviour
{
    [Tooltip("Max health of this game character")]
    public int MaxHealth;

    [Tooltip("Current health points of the character")]
    public int Health;

    /// <summary>
    /// Decrements the health and triggers death if appropriate
    /// </summary>
    /// <param name="damageValue">How much take to take</param>
    public void TakeDamage(int damageValue)
    {
        Health -= damageValue;

        if (Health <= 0) Die();
    }

    /// <summary>
    /// Increments the health up to max health
    /// </summary>
    /// <param name="healValue"></param>
    public void Heal(int healValue)
    {
        Health = Mathf.Min(Health + healValue, MaxHealth);
    }

    /// <summary>
    /// Called by TakeDamage when Health reaches 0 or less
    /// </summary>
    abstract public void Die();
}
