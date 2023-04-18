using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetToStartTrigger : MonoBehaviour
{
    [SerializeField] private Transform resetPosition;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.position = resetPosition.position;
        }
    }
}
