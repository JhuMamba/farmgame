using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField] float sGridSize = 3f;
    [SerializeField] BuildingData sDefaultBuildingData;
    [SerializeField] LayerMask sGridLayerMask;
    [SerializeField] Material sGhostMaterial;
    [SerializeField] GameObject sCellIndicatorPrefab;

    GameObject mObjectToPlace;
    GameObject mGhostObject;
    Building mBuildingToPlace;
    List<GameObject> mCellIndicators = new List<GameObject>();
    bool mIsPlacing = false;
    bool mIsReplacing = false;
    bool mIsRotated = false;
    float mObjectWidth, mObjectDepth;

    UIManager mUIManager;

    static HashSet<Vector3> mOccupiedPositions = new HashSet<Vector3>();

    public bool IsBusy => mIsPlacing || mIsReplacing;
    private void Start()
    {
        mUIManager = FindAnyObjectByType<UIManager>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B)) InitBuildingToPlace(sDefaultBuildingData);
        if (EventSystem.current.IsPointerOverGameObject()) return;
        if (mIsPlacing || mIsReplacing)
        {
            if (Input.GetMouseButton(0))
                UpdateGhostPos();
        }
    }

    public void InitBuildingToPlace(BuildingData buildingData)
    {
        mIsPlacing = true;
        mObjectToPlace = buildingData.prefab;
        mObjectWidth = buildingData.width * sGridSize;
        mObjectDepth = buildingData.depth * sGridSize;
        CreateGhostObject();
        CreateCellIndicators();
        if (mUIManager != null) mUIManager.ShowUI(UIManager.CanvasType.Build);
    }

    public void InitBuildingToPlace(Building building)
    {
        if (building == null) Debug.LogError("Building is null!");
        mIsPlacing = true;
        mBuildingToPlace = building;
        mObjectToPlace = mBuildingToPlace.GetBuildingData.prefab;
        mObjectWidth = mBuildingToPlace.GetBuildingData.width * sGridSize;
        mObjectDepth = mBuildingToPlace.GetBuildingData.depth * sGridSize;
        CreateGhostObject();
        CreateCellIndicators();
        if (mUIManager != null) mUIManager.ShowUI(UIManager.CanvasType.Build);
    }

    void CreateGhostObject()
    {
        if (mGhostObject != null) Destroy(mGhostObject);
        mGhostObject = Instantiate(mObjectToPlace);
        mGhostObject.GetComponentInChildren<Collider>().enabled = false;

        Renderer[] renderers = mGhostObject.GetComponentsInChildren<Renderer>();
        foreach (var renderer in renderers)
        {
            var mats = renderer.sharedMaterials;
            Material[] newMats = new Material[mats.Length];
            for (int i = 0; i < mats.Length; i++)
            {
                newMats[i] = sGhostMaterial;
            }
            renderer.sharedMaterials = newMats;
        }
    }

    void CreateCellIndicators()
    {
        foreach (var indicator in mCellIndicators)
        {
            Destroy(indicator);
        }
        mCellIndicators.Clear();

        // Create indicators for each cell the object will occupy
        Vector3 snappedPosition = mGhostObject.transform.position;
        for (int x = 0; x < mObjectWidth / sGridSize; x++)
        {
            for (int y = 0; y < mObjectDepth / sGridSize; y++)
            {
                Vector3 gridPos = snappedPosition + new Vector3(x * sGridSize, 0, y * sGridSize);
                GameObject cellIndicator = Instantiate(sCellIndicatorPrefab, gridPos, Quaternion.identity, mGhostObject.transform);
                mCellIndicators.Add(cellIndicator);
            }
        }
        if (IsPositionAvailable(mGhostObject.transform.position))
            SetGhostColor(GhostColor.AVAILABLE);
        else
            SetGhostColor(GhostColor.OCCUPIED);
    }

    void UpdateGhostPos()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 50f, sGridLayerMask))
        {
            Vector3 point = hit.point;
            Vector3 snappedPosition = SnapToGrid(point);

            mGhostObject.transform.position = snappedPosition;

            if (IsPositionAvailable(snappedPosition))
                SetGhostColor(GhostColor.AVAILABLE);
            else
                SetGhostColor(GhostColor.OCCUPIED);
        }
    }

    Vector3 SnapToGrid(Vector3 point)
    {
        // Snap the mouse position to the nearest grid cell
        Vector3 snappedPosition = new Vector3(
            Mathf.Round(point.x / sGridSize) * sGridSize,
            Mathf.Round(point.y / sGridSize) * sGridSize,
            Mathf.Round(point.z / sGridSize) * sGridSize);

        return snappedPosition;
    }

    void SetGhostColor(GhostColor color)
    {
        Renderer[] renderers = mGhostObject.GetComponentsInChildren<Renderer>();
        foreach (var renderer in renderers)
        {
            var mats = renderer.sharedMaterials;
            foreach (var mat in mats)
            {
                Color newColor = color switch
                {
                    GhostColor.AVAILABLE => Color.green,
                    GhostColor.OCCUPIED => Color.red,
                    _ => Color.red,
                };
                newColor.a = 0.5f;
                mat.color = newColor;
            }
        }
    }

    bool IsPositionAvailable(Vector3 position)
    {
        for (int x = 0; x < mObjectWidth / sGridSize; x++)
        {
            for (int y = 0; y < mObjectDepth / sGridSize; y++)
            {
                Vector3 gridPos = new Vector3(position.x + x * sGridSize, position.y, position.z + y * sGridSize);
                if (mOccupiedPositions.Contains(gridPos))
                {
                    return false;
                }
            }
        }
        return true;
    }

    public void PlaceObject()
    {
        Vector3 placementPos = mGhostObject.transform.position;

        if (IsPositionAvailable(placementPos))
        {
            if (mIsReplacing) // If replacing, just move the existing building
            {
                mBuildingToPlace.transform.position = placementPos;
                mBuildingToPlace.gameObject.SetActive(true); // Reactivate the building
                mBuildingToPlace.OccupiedPositions = OccupyGridCells(placementPos);
                mIsReplacing = false; // Reset replacing state
            }
            else
            {
                var newPlacement = Instantiate(mObjectToPlace, placementPos, mGhostObject.transform.rotation);
                var occupiedPos = OccupyGridCells(placementPos);

                var newBuilding = newPlacement.GetComponentInChildren<Building>();
                newBuilding.OccupiedPositions = occupiedPos;
            }

            Destroy(mGhostObject);
            mIsPlacing = false;
            mIsRotated = false;

            foreach (var indicator in mCellIndicators)
            {
                Destroy(indicator);
            }
            mCellIndicators.Clear();

            if (mUIManager != null) mUIManager.ShowUI(UIManager.CanvasType.Default);
        }
        else
        {
            SetGhostColor(GhostColor.OCCUPIED);
        }
    }

    HashSet<Vector3> OccupyGridCells(Vector3 position)
    {
        HashSet<Vector3> newCells = new HashSet<Vector3>();
        for (int x = 0; x < mObjectWidth / sGridSize; x++)
        {
            for (int y = 0; y < mObjectDepth / sGridSize; y++)
            {
                Vector3 gridPos = new Vector3(position.x + x * sGridSize, position.y, position.z + y * sGridSize);
                newCells.Add(gridPos);
            }
        }
        mOccupiedPositions.AddRange(newCells);
        return newCells;
    }
    public void RotateObject()
    {
        float rotationAngle = 90f;
        if (mIsRotated)
        {
            mIsRotated = false;
            rotationAngle *= 1f;
        }
        else
        {
            mIsRotated = true;
            rotationAngle *= -1f;
        }

        mGhostObject.transform.Rotate(Vector3.up, rotationAngle);

        if (rotationAngle == 90f || rotationAngle == -90f)
        {
            float temp = mObjectWidth;
            mObjectWidth = mObjectDepth;
            mObjectDepth = temp;
        }
    }
    public void MoveBuilding(Building building)
    {
        foreach (var pos in building.OccupiedPositions)
        {
            if (mOccupiedPositions.Contains(pos))
                mOccupiedPositions.Remove(pos);
        }

        mIsReplacing = true;
        mBuildingToPlace = building;
        mObjectToPlace = building.gameObject;

        mObjectWidth = building.GetBuildingData.width * sGridSize;
        mObjectDepth = building.GetBuildingData.depth * sGridSize;

        CreateGhostObject();
        CreateCellIndicators();
        building.gameObject.SetActive(false);
        if (mUIManager != null) mUIManager.ShowUI(UIManager.CanvasType.Build);
    }
}

enum GhostColor
{
    AVAILABLE = 0,
    OCCUPIED = 1,
}
