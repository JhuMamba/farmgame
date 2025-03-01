using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SlotUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text_ItemName;
    [SerializeField] TextMeshProUGUI text_ItemCount;
    public void InitUI(ItemSO item, int count)
    {
        text_ItemName.text = item.itemName;
        text_ItemCount.text = count.ToString();
    }
}
