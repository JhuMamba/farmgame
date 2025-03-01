using UnityEngine;

[CreateAssetMenu(fileName = "NewBuildingData", menuName = "Building/BuildingData")]
public class BuildingData : ScriptableObject
{
    public GameObject prefab;
    public BuildingType buildingType;
    public int width = 1;
    public int depth = 1;
    public string buildingName;
    public float cost;

    private void OnValidate()
    {
        if (!prefab) Debug.LogWarning($"Prefab is null! Name: {name}");
        if (width <= 0) Debug.LogWarning($"Unsupported width! Name: {name}");
        if (depth <= 0) Debug.LogWarning($"Unsupported depth! Name: {name}");
        if (string.IsNullOrEmpty(buildingName)) Debug.LogWarning($"Building name is not given! Name: {name}");
        if (cost <= 0) Debug.LogWarning($"Cost can not be lower than 0! Name: {name}");
    }
}
