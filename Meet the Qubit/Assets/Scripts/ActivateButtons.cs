using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    public Button hadamardButton;
    public Button xGateButton;
    public Button yGateButton;
    public Button zGateButton;
    public Button sGateButton;
    public Button tGateButton;
    public Button activateButtonsButton;

    void Start()
    {
        // Disable gate buttons at the start
        SetGateButtonsInteractable(false);

        // Set the listener for the 'ActivateButtonsButton'
        activateButtonsButton.onClick.AddListener(ActivateButtons);
    }

    void SetGateButtonsInteractable(bool interactable)
    {
        hadamardButton.interactable = interactable;
        xGateButton.interactable = interactable;
        yGateButton.interactable = interactable;
        zGateButton.interactable = interactable;
        sGateButton.interactable = interactable;
        tGateButton.interactable = interactable;
    }

    void ActivateButtons()
    {
        // Enable all gate buttons when 'ActivateButtonsButton' is clicked
        SetGateButtonsInteractable(true);
    }
}
