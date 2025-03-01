using UnityEngine;

public class StorageBuilding : Building
{
    public enum StorageType
    {
        Crops = 0,
        //adjustable
    }
    [SerializeField] private int storageCapacity = 100;
    private int currentStorage = 0;

    private void AddResources(int amount)
    {
        if (currentStorage + amount <= storageCapacity)
        {
            currentStorage += amount;
            Debug.Log("Added resources! Current storage: " + currentStorage);
        }
        else
        {
            Debug.Log("Not enough space in storage.");
        }
    }

    protected override void UpdateBuildingVisuals() {}

    public override void ShowOptions()
    {
        base.ShowOptions();
    }
}
