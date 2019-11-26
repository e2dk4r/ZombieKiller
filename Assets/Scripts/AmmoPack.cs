using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPack : MonoBehaviour
{
    public int ammoRounds = 5;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.gameObject.GetComponent<Player>();

        if (player != null)
            if (player.Ammo < player.maxAmmo)
            {
                player.Ammo += ammoRounds;
                Destroy(gameObject);
            }


    }
}