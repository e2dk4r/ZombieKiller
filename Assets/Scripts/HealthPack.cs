using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.gameObject.GetComponent<Player>();

        if (player != null)
            if (player.Health < player.maxHealth)
            {
                player.Health++;
                Destroy(gameObject);
            }

        
    }
}
