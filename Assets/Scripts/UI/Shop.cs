using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [Header("List of items sold")]
    [SerializeField] private ShopItem[] shopItems;

    [Header("References")]
    [SerializeField] private Transform shopContainer;
    [SerializeField] private GameObject shopItemPrefab;

    private void Start()
    {
        PopulateShop();
    }

    private void PopulateShop()
    {
        foreach (var item in shopItems)
        {
            GameObject itemObject = Instantiate(shopItemPrefab, shopContainer);
            itemObject.GetComponent<UIItem>().shopItem = item;
            itemObject.transform.Find("Image").GetComponent<Image>().sprite = item.sprite;
            itemObject.transform.Find("Price").GetComponent<Text>().text = $"${ item.cost }";
        }
    }
}
