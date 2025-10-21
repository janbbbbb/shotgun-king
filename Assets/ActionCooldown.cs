using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionCooldown : MonoBehaviour
{
    private int nextAvailableTurn = 1;
    private int cooldown;

    public ActionCooldown(int cooldownTurns)
    {
        cooldown = cooldownTurns;
    }

    public bool CanUse()
    {
        return GameManager.Instance.turnCounter >= nextAvailableTurn;
    }

    public void Use()
    {
        nextAvailableTurn = GameManager.Instance.turnCounter + cooldown;
    }
}
