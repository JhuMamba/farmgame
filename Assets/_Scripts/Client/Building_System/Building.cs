using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building : MonoBehaviour, IMoveable
{
    [Header("Building Settings")]
    [SerializeField] protected BuildingData sBuildingData;
    protected Animator buildingAnimator;

    PlacementSystem mPlacementSystem;
    UIManager mUIManager;
    
    public HashSet<Vector3> OccupiedPositions = new HashSet<Vector3>();
    public BuildingData GetBuildingData => sBuildingData;
    public void Interact()
    {
        ShowOptions();
    }

    public void Move()
    {
        if (!mPlacementSystem) mPlacementSystem = FindAnyObjectByType<PlacementSystem>();
        mPlacementSystem.MoveBuilding(this);
    }

    public virtual void ShowOptions()
    {
        if (!mUIManager) mUIManager = FindAnyObjectByType<UIManager>();
        mUIManager.ShowUI(UIManager.CanvasType.Interact);
    }
    protected virtual void Start()
    {
        buildingAnimator = GetComponent<Animator>();
    }

    protected virtual void Update() {}
    protected virtual void UpdateBuildingVisuals() {}
}
[Serializable]
public enum BuildingType
{
    Empty,
    Farm,
    Storage,
    Workshop
}

public interface IMoveable
{
    public void Move();
}