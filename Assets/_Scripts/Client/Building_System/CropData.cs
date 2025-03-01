using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCropData", menuName = "Building/CropData")]
public class CropData : ScriptableObject
{
    public string cropName = "default crop";
    public float growthTimeSec = 5f;
    public ItemSO requiredItem;
    public ItemSO yieldItem;
    public int yieldCount = 1;

    [Header("Visuals")]
    public GameObject plantedVisual;
    public GameObject growingVisual;
    public GameObject harvestableVisual;
}
