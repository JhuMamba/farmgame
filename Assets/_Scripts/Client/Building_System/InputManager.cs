using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] LayerMask sBuildingLayer;
    static Building mCurrentBuilding;
    public static Building CurrentBuilding => mCurrentBuilding;
    PlacementSystem m_PlacementSystem;
    InputState m_InputState = InputState.Default;
    private void Start()
    {
        m_PlacementSystem = FindAnyObjectByType<PlacementSystem>();
    }
    void Update()
    {
        if (m_PlacementSystem.IsBusy) return;
        if (Input.touchCount > 0) // Mobile Touch Input
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                HandleTouch(touch.position);
            }
        }
        else if (Input.GetMouseButtonDown(0)) // Mouse Click Input for Editor
        {
            HandleTouch(Input.mousePosition);
        }
    }

    void HandleTouch(Vector2 touchPosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(touchPosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f, sBuildingLayer))
        {
            mCurrentBuilding = hit.collider.GetComponentInParent<Building>();
            if (mCurrentBuilding != null)
            {
                switch (m_InputState)
                {
                    case InputState.Default:
                        mCurrentBuilding.ShowOptions();
                        break;
                    case InputState.Planting:
                        if (mCurrentBuilding is HarvestableBuilding)
                            ((HarvestableBuilding)mCurrentBuilding).PlantCrop(ToolController.GetCurrentCropData);
                        break;
                    case InputState.Harvesting:
                        if (mCurrentBuilding is HarvestableBuilding)
                            ((HarvestableBuilding)mCurrentBuilding).HarvestCrop();
                        break;
                    default:
                        break;
                }
            }
        }
    }
    public void SetInputState(InputState state)
    {
        m_InputState = state;
    }
    public enum InputState
    {
        Default = 0,
        Planting = 1,
        Harvesting = 2,
    }
}
