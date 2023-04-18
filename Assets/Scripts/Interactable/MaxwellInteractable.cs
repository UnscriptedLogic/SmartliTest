using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxwellInteractable : Interactable
{
    [Header("Coin Settings")]
    [SerializeField] private int value = 1;

    [Header("Animation Settings")]
    [SerializeField] private float animationSpeed = 1;
    [SerializeField] private Animator animator;

    private void Start()
    {
        animator.speed = animationSpeed;
    }

    public override void Interact(ICanInteract collector)
    {
        Player player = collector as Player;
        player.CollectableHandler.Modify(ModifyType.Add, value);

        Destroy(gameObject);
    }
}
