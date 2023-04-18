using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinInteractable : Interactable
{
    [SerializeField] private float amount = 1f;

    public override void Interact(ICanInteract collector)
    {
        if (collector as Player)
        {
            Player player = collector as Player;
            player.CollectableHandler.Modify(ModifyType.Add, amount);

            Destroy(gameObject);
        }

        if (collector as Player2D != null)
        {
            Player2D player2D = collector as Player2D;
            player2D.CollectableHandler.Modify(ModifyType.Add, amount);

            Destroy(gameObject);
        }
    }
}
