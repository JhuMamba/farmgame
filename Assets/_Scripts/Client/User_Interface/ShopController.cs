using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    [Header("ItemSO")]
    [SerializeField] ItemSO wheatItemSO;
    [SerializeField] ItemSO carrotItemSO;
    [Header("References")]
    [SerializeField] Button sBuyWheatButton;
    [SerializeField] Button sBuyCarrotButton;

    Inventory mInventory;
    private void Start()
    {
        mInventory = FindAnyObjectByType<Inventory>();
        if (!mInventory) return;
        if (sBuyWheatButton) sBuyWheatButton.onClick.AddListener(() => mInventory.AddItem(wheatItemSO, 1));
        if (sBuyCarrotButton) sBuyCarrotButton.onClick.AddListener(()=> mInventory.AddItem(carrotItemSO, 1));
    }
}
