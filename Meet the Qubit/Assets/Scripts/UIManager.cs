using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

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
    public Text       gateExplainerText;

    //private bool      continueJumping = true; // New field to control jumping animation


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
        //StartCoroutine(StartQuboJumpingAfterDelay(0.5f));  // Start jumping after a 0.5 second delay
    }

    private IEnumerator StartQuboJumpingAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for the specified delay time

        // Now start the Qubo jumping
        //StartQuboJumping();
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
        if (!gatePanel.activeInHierarchy)
        {
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

    //public void StartQuboJumping()
    //{
    //    // Check if UIManager GameObject is active and if quboObject is active
    //    if (this.gameObject.activeInHierarchy && quboObject.activeInHierarchy)
    //    {
    //        StartCoroutine(QuboJumpingRoutine());
    //    }
    //    else
    //    {
    //        Debug.Log("Either UIManager GameObject or quboObject is not active!");
    //    }
    //}

    //public void StopQuboJumping()
    //{
    //    continueJumping = false;
    //}

    //private IEnumerator QuboJumpingRoutine()
    //{
    //    // Initial height and target height
    //    Vector3 initialPosition = quboObject.transform.position;
    //    Vector3 targetPosition = initialPosition + new Vector3(0f, 1f, 0f);  // Change the 1f to however high you want the jump to be

    //    while (continueJumping)
    //    {
    //        // First Jump
    //        quboObject.transform.position = Vector3.Lerp(quboObject.transform.position, targetPosition, Time.deltaTime * 5);
    //        yield return new WaitForSeconds(0.2f);

    //        // Second Jump
    //        quboObject.transform.position = Vector3.Lerp(quboObject.transform.position, targetPosition, Time.deltaTime * 5);
    //        yield return new WaitForSeconds(0.2f);

    //        // Return to Initial Position
    //        quboObject.transform.position = Vector3.Lerp(quboObject.transform.position, initialPosition, Time.deltaTime * 5);
    //        yield return new WaitForSeconds(0.5f);

    //        // Third Jump
    //        quboObject.transform.position = Vector3.Lerp(quboObject.transform.position, targetPosition, Time.deltaTime * 5);
    //        yield return new WaitForSeconds(0.2f);

    //        // Return to Initial Position again
    //        quboObject.transform.position = Vector3.Lerp(quboObject.transform.position, initialPosition, Time.deltaTime * 5);
    //        yield return new WaitForSeconds(0.2f);
    //    }
    //}


    //// Optionally, if we want a "Back" button on the Bloch Sphere Panel
    //public void ShowWelcomePanel()
    //{
    //    blochSpherePanel.SetActive(false);
    //    welcomePanel.SetActive(true);
    //}
}
