using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Shop/ShopItem")]
public class ShopItem : ScriptableObject
{
    public string itemName;
    public Sprite sprite;
    public int cost;
}
