using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour, IInteractable
{
    public virtual void Interact(ICanInteract collector)
    {
        //Override in sub classes
    }

    private void OnTriggerEnter(Collider other)
    {
        ICanInteract interactor = other.GetComponent<ICanInteract>();
        if (interactor != null)
        {
            interactor.Interact(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ICanInteract collecter = collision.gameObject.GetComponent<ICanInteract>();
        if (collecter != null)
        {
            collecter.Interact(this);
        }
    }
}
