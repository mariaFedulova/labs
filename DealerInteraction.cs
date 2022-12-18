using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DealerInteraction : MonoBehaviour
{
    public event Action OnPlayerDetectEvent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            OnPlayerDetectEvent();

    }
}

