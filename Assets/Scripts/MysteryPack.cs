using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Item
{
    Ammo,
    Health
}
public class MysteryPack : MonoBehaviour
{
    public float ammoChance = 0.7f;
    public int ammoRounds = 5;

    private float seed = 0f;

    private void Start()
    {
        seed = Random.Range(0f, 1f);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.gameObject.GetComponent<Player>();

        if (player != null)
        {
            var kind = getKind();
            if (kind == Item.Health)
            {
                if (player.Health < player.maxHealth)
                {
                    player.Health++;
                    Destroy(gameObject);
                }
            } else
            {
                if (player.Ammo < player.maxAmmo)
                {
                    player.Ammo += ammoRounds;
                    Destroy(gameObject);
                }
            }
        }


    }
    private Item getKind()
    {
        if (0 <=seed && seed <= ammoChance) {
            return Item.Ammo;
        } else {
            return Item.Health;
        }
    }
}
