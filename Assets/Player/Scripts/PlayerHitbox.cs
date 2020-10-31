using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PlayerHitbox : MonoBehaviour
{
    public PlayerManager PlayerManager;

    private void Awake()
    {
        PlayerManager = GetComponentInParent<PlayerManager>();
    }
}
