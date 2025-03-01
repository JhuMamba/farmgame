using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Canvases")]
    [SerializeField] RectTransform sDefaultCanvas;
    [SerializeField] RectTransform sBuildCanvas;
    [SerializeField] RectTransform sInteractCanvas;

    [Header("Button References")]
    [SerializeField] Button sMoveButton;
    [SerializeField] Button sRotateButton;
    [SerializeField] Button sPlaceButton;

    [Header("Inventory")]
    [SerializeField] SlotUI sItemPrefab;
    [SerializeField] RectTransform sItemParent;

    PlacementSystem mPlacementSystem;
    Inventory mInventory;
    private void Start()
    {
        mPlacementSystem = FindAnyObjectByType<PlacementSystem>();
        mInventory = FindAnyObjectByType<Inventory>();
        if (mPlacementSystem != null)
        {
            sMoveButton.onClick.AddListener(MoveAction);
            sRotateButton.onClick.AddListener(mPlacementSystem.RotateObject);
            sPlaceButton.onClick.AddListener(mPlacementSystem.PlaceObject);
        }
        if (mInventory != null)
        {
            mInventory.onInventoryChanged += DrawInventory;
        }
    }
    public void ShowUI(CanvasType type)
    {
        sDefaultCanvas.gameObject.SetActive(type == CanvasType.Default);
        sBuildCanvas.gameObject.SetActive(type == CanvasType.Build);
        sInteractCanvas.gameObject.SetActive(type == CanvasType.Interact);
    }
    
    void DrawInventory()
    {
        foreach (Transform child in sItemParent)
        {
            Destroy(child.gameObject);
        }
        foreach (var item in mInventory.Items)
        {
            var newItem = Instantiate(sItemPrefab, sItemParent);
            newItem.InitUI(item.Key, item.Value);
        }
    }

    void MoveAction()
    {
        InputManager.CurrentBuilding.Move();
    }

    public enum CanvasType
    {
        Default = 0,
        Build = 1,
        Interact = 2,
    }
    private void OnDestroy()
    {
        sMoveButton!.onClick.RemoveListener(MoveAction);
        sRotateButton!.onClick.RemoveListener(mPlacementSystem.RotateObject);
        sPlaceButton!.onClick.RemoveListener(mPlacementSystem.PlaceObject);
        if (mInventory != null)
        {
            mInventory!.onInventoryChanged -= DrawInventory;
        }
    }
}