using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "User/Item")]
public class ItemSO : ScriptableObject
{
    public int itemID;
    public string itemName = string.Empty;

    private void OnValidate()
    {
        if (itemID == 0) Debug.LogWarning($"Please change the item ID! Name: {name}");
        if (string.IsNullOrEmpty(itemName)) Debug.LogWarning($"Please change the item name! Name: {name}");
    }
}
