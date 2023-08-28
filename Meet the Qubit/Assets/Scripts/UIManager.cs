using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject welcomePanel;
    public GameObject blochSpherePanel;
    public GameObject quboPanel;
    public GameObject blochSphere;
    public GameObject quboObject;
    public GameObject gateButtons;
    public GameObject gatePanel;
    public GameObject gateExplainerPanel;

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

    public void ShowGateExplainerPanel()
    {
        gateExplainerPanel.SetActive(true);

    }



    //// Optionally, if you want a "Back" button on the Bloch Sphere Panel
    //public void ShowWelcomePanel()
    //{
    //    blochSpherePanel.SetActive(false);
    //    welcomePanel.SetActive(true);
    //}
}
