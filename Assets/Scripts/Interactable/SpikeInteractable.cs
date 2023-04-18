using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeInteractable : Interactable
{
    [SerializeField] private float damage = 10f;

    public override void Interact(ICanInteract collector)
    {
        if (collector as Player != null)
        {
            Player player = collector as Player;
            player.HealthHandler.Modify(ModifyType.Subtract, damage);
        }

        if (collector as Player2D != null)
        {
            Player2D player2D = collector as Player2D;
            player2D.HealthHandler.Modify(ModifyType.Subtract, damage);
        }
    }
}
