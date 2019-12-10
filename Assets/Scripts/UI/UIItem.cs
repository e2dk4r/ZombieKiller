using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIItem : MonoBehaviour
{
    public ShopItem shopItem;
    public void onClick()
    {
        Debug.Log(shopItem.itemName);
        Destroy(gameObject);
    }
}
