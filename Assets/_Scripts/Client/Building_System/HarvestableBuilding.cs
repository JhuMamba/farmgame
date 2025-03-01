using System;
using UnityEngine;

public class HarvestableBuilding : Building, IHarvestable
{
    [SerializeField] Transform sCropRoot;
    GameObject mCurrentCropVisual;
    CropData mCurrentCropData;
    Inventory mInventory;
    enum GrowthState { Empty, Planted, Growing, GrowingHalf, Harvestable }
    GrowthState mCurrentState = GrowthState.Empty;
    TimeSpan mGrowthProgress;
    DateTime mPlantTime;
    private void OnEnable()
    {
        mInventory = FindAnyObjectByType<Inventory>();
    }
    public override void ShowOptions()
    {
        base.ShowOptions();
    }

    public void PlantCrop(CropData data)
    {
        if (!mCurrentCropData && mCurrentState == GrowthState.Empty)
        {
            if (!mInventory.RemoveItem(data.requiredItem, 1))
            {
                Debug.Log("You do not have the required item!");
                return;
            }
            mCurrentCropData = data;
            mCurrentState = GrowthState.Planted;
            mPlantTime = DateTime.UtcNow;
            UpdateBuildingVisuals();
            StartGrowing();
        }
    }

    public void HarvestCrop()
    {
        if (mCurrentState == GrowthState.Harvestable)
        {
            mCurrentState = GrowthState.Empty;
            mInventory.AddItem(mCurrentCropData.yieldItem, mCurrentCropData.yieldCount);
            mPlantTime = default;
            mCurrentCropData = null;
            UpdateBuildingVisuals();
        }
    }

    private void StartGrowing()
    {
        mCurrentState = GrowthState.Growing;
        // Handle any animation or visual update for growth start
    }

    protected override void UpdateBuildingVisuals()
    {
        base.UpdateBuildingVisuals();
        if (mCurrentCropVisual != null) Destroy(mCurrentCropVisual);
        GameObject switchedVisual = mCurrentState switch
        {
            GrowthState.Planted => mCurrentCropData.plantedVisual,
            GrowthState.GrowingHalf => mCurrentCropData.growingVisual,
            GrowthState.Harvestable => mCurrentCropData.harvestableVisual,
            _ => null,
        };
        if (switchedVisual != null)
            mCurrentCropVisual = Instantiate(switchedVisual, transform.position, Quaternion.identity, sCropRoot);
    }

    protected override void Update()
    {
        base.Update();

        if (mCurrentState == GrowthState.Growing || mCurrentState == GrowthState.GrowingHalf)
        {
            mGrowthProgress = DateTime.UtcNow - mPlantTime;
            if (mGrowthProgress >= TimeSpan.FromSeconds(mCurrentCropData.growthTimeSec))
            {
                mCurrentState = GrowthState.Harvestable;
                UpdateBuildingVisuals();
            }
            else if (mGrowthProgress >= TimeSpan.FromSeconds(mCurrentCropData.growthTimeSec / 2))
            {
                mCurrentState = GrowthState.GrowingHalf;
                UpdateBuildingVisuals();
            }
        }
    }
}

public interface IHarvestable
{
    public void PlantCrop(CropData data);
    public void HarvestCrop();
}