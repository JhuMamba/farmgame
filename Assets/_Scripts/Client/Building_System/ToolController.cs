using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolController : MonoBehaviour
{
    [SerializeField] Button sDefaultButton;
    [SerializeField] Button sHarvestButton;
    [SerializeField] PlantButtonHolder[] sPlantButtons;
    [SerializeField] InputManager m_InputManager;

    Button m_SelectedButton;
    Color m_NormalColor = Color.white;
    Color m_SelectedColor = Color.green;

    static CropData m_currentCropData = default;

    public static CropData GetCurrentCropData => m_currentCropData;

    private void Start()
    {
        if (m_InputManager == null)
        {
            Debug.LogError("No input manager attached!");
            return;
        }

        sDefaultButton.onClick.AddListener(() => SelectButton(sDefaultButton, InputManager.InputState.Default));
        sHarvestButton.onClick.AddListener(() => SelectButton(sHarvestButton, InputManager.InputState.Harvesting));

        if (sPlantButtons == null || sPlantButtons.Length == 0) return;

        foreach (var plantButton in sPlantButtons)
        {
            plantButton.button.onClick.AddListener(() =>
            {
                SelectButton(plantButton.button, InputManager.InputState.Planting);
                m_currentCropData = plantButton.crop;
            });
        }

        // Set default selection
        SelectButton(sDefaultButton, InputManager.InputState.Default);
    }

    void SelectButton(Button newButton, InputManager.InputState state)
    {
        // Reset previous button color
        if (m_SelectedButton != null)
            m_SelectedButton.image.color = m_NormalColor;

        // Update selected button
        m_SelectedButton = newButton;
        m_SelectedButton.image.color = m_SelectedColor;

        // Update input state
        m_InputManager.SetInputState(state);
    }


    [Serializable]
    public struct PlantButtonHolder
    {
        public Button button;
        public CropData crop;
    }
}
