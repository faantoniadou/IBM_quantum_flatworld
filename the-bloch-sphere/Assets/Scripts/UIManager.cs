using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public GameObject         welcomePanel;
    public GameObject         blochSpherePanel;
    public GameObject         quboPanel;
    public GameObject         blochSphere;
    public GameObject         quboObject;
    public GameObject         gateButtons;
    public GameObject         gatePanel;
    public GameObject         gateExplainerPanel;
    public Text               gateExplainerText;
    public QuantumGateHandler quantumGateHandler;
    public Button             welcomeNextButton;
    public Button             blochSphereNextButton;
    public Button             quboNextButton;
    public Button             gateNextButton;

    private float lastInputTime = -1f;
    private float inputCooldown = 0.5f;


    private void Update()
    {
        if (Time.time - lastInputTime < inputCooldown)
        {
            return;
        }


        if (Input.GetKeyDown(KeyCode.H))
        {
            lastInputTime = Time.time;
            quantumGateHandler.OnGateClick("hadamard");
            OnGateHover("hadamard");
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            lastInputTime = Time.time;
            quantumGateHandler.OnGateClick("x");
            OnGateHover("x");
        }
        else if (Input.GetKeyDown(KeyCode.Y))
        {
            lastInputTime = Time.time;
            quantumGateHandler.OnGateClick("y");
            OnGateHover("y");
        }
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            lastInputTime = Time.time;
            quantumGateHandler.OnGateClick("z");
            OnGateHover("z");
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            lastInputTime = Time.time;
            quantumGateHandler.OnGateClick("s");
            OnGateHover("s");
        }
        else if (Input.GetKeyDown(KeyCode.T))
        {
            lastInputTime = Time.time;
            quantumGateHandler.OnGateClick("t");
            OnGateHover("t");
        }

        else if (Input.GetKeyDown(KeyCode.Return))
        {
            if (welcomePanel.activeSelf)
            {
                welcomeNextButton.onClick.Invoke();
            }
            else if (blochSpherePanel.activeSelf)
            {
                blochSphereNextButton.onClick.Invoke();
            }
            else if (quboPanel.activeSelf)
            {
                quboNextButton.onClick.Invoke();
            }
            else if (gatePanel.activeSelf)
            {
                gateNextButton.onClick.Invoke();
            }
        }
    }

    // Call this when the "Next" button on the Welcome Panel is clicked
    public void ShowBlochSpherePanel()
    {
        welcomePanel.SetActive(false); // Hide welcome panel
        blochSpherePanel.SetActive(true); // Show Bloch Sphere panel
    }

    // Call this when the "Next" button on the Bloch Sphere Panel is clicked
    public void ShowQuboPanel()
    {
        blochSpherePanel.SetActive(false);
        quboPanel.SetActive(true); // Show Bloch Sphere panel
    }

    public void ShowBlochSphere()
    {
        blochSphere.SetActive(true);
    }

    public void ShowQubo()
    {
        blochSphere.SetActive(false);
        quboObject.SetActive(true);
    }

    public void ShowGatePanel()
    {
        quboPanel.SetActive(false);
        gateButtons.SetActive(true);
        gatePanel.SetActive(true);
    }

    public void HideGatePanel()
    {
        gatePanel.SetActive(false);
    }

    public void OnPointerExit()
    {
        // This method will be called when the mouse pointer exits the button of the gate panel
        gateExplainerPanel.SetActive(false);
    }

    public void OnGateHover(string gateName)
    {
        if (!Application.isEditor)
        {
            if (!gatePanel.activeInHierarchy) return;
        }

        gateExplainerPanel.SetActive(true);

        gateExplainerText.text = gateName switch
        {
            "hadamard" => "The Hadamard gate creates superposition and is a 180 degree rotation around the diagonal X+Z axis.",
            "x" => "The X gate flips qubo along the X-axis.",
            "y" => "The Y gate flips qubo along the Y-axis.",
            "z" => "The Z gate flips qubo along the Z-axis.",
            "s" => "The S gate rotates qubo by 90° along the Z-axis.",
            "t" => "The T gate rotates qubo by 45° along the Z-axis.",
            _ => "Unknown gate.",
        };
    }

}