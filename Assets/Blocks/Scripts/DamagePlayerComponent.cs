using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DamagePlayerComponent : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.TryGetComponent(out PlayerHitbox playerHitbox))
        {
            playerHitbox.PlayerManager.ReduceHealth_F();
        }
    }
}
