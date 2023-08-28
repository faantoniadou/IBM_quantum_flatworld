using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;

public class QuantumGateHandler : MonoBehaviour
{
    public Transform quboTransform;
    public GameObject blochSphere; // Reference to the Bloch Sphere GameObject
    //public Text gateExplainerText;  // Reference to the Text component


    private float blochSphereRadius;
    private const string baseURL = "http://127.0.0.1:5000";

    [System.Serializable]
    public class BlochVectorResponse
    {
        public float[] bloch_vector;
    }

    private void Start()
    {
        blochSphereRadius = blochSphere.transform.localScale.x / 2; // Assuming the sphere is uniformly scaled
        ResetQuboToZeroState();
        StartCoroutine(ResetQubitStateOnServer());
    }

    private IEnumerator ResetQubitStateOnServer()
    {
        string url = baseURL + "/reset";
        UnityWebRequest www = UnityWebRequest.Post(url, new WWWForm());

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Qubit state reset on server.");
        }
    }

    private void ResetQuboToZeroState()
    {
        Vector3 zeroStatePosition = new Vector3(0, blochSphereRadius, 0);  // Replace with the position representing |0> state on your Bloch sphere
        quboTransform.position = zeroStatePosition;
    }

    public void OnGateClick(string gateName)
    {
        Debug.Log($"{gateName.ToUpper()} Gate button clicked");
        StartCoroutine(ApplyGate(gateName));

        //// Update the explainer text
        //switch (gateName)
        //{
        //    case "hadamard":
        //        gateExplainerText.text = "The Hadamard gate creates superposition.";
        //        break;
        //    case "x":
        //        gateExplainerText.text = "The X gate flips the qubit along the X-axis.";
        //        break;
        //    case "y":
        //        gateExplainerText.text = "The Y gate flips the qubit along the Y-axis.";
        //        break;
        //    case "z":
        //        gateExplainerText.text = "The Z gate flips the qubit along the Z-axis.";
        //        break;
        //    case "s":
        //        gateExplainerText.text = "The S gate rotates the qubit by π/2 along the Z-axis.";
        //        break;
        //    case "t":
        //        gateExplainerText.text = "The T gate rotates the qubit by π/4 along the Z-axis.";
        //        break;
        //    default:
        //        gateExplainerText.text = "Unknown gate.";
        //        break;
        //}
    }

    private IEnumerator ApplyGate(string gateName)
    {
        string url = baseURL + "/apply_gate";

        UnityWebRequest www = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes("{\"gate_name\": \"" + gateName + "\"}");
        www.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Raw Server Response: " + www.downloadHandler.text);

            BlochVectorResponse response = JsonUtility.FromJson<BlochVectorResponse>(www.downloadHandler.text);

            if (response == null)
            {
                Debug.LogError("Response is null");
            }
            else if (response.bloch_vector == null)
            {
                Debug.LogError("Bloch vector is null");
            }
            else
            {
                Vector3 blochVector = new Vector3(response.bloch_vector[0], response.bloch_vector[1], response.bloch_vector[2]);
                MoveQuboToPosition(blochVector);
                Debug.Log("Received Bloch Vector: " + www.downloadHandler.text);
            }
        }

    }



    private void MoveQuboToPosition(Vector3 blochVector)
    {
        Debug.Log("Attempting to move Qubo");

        blochVector.Normalize();
        Vector3 targetPosition = blochVector * blochSphereRadius;
        quboTransform.position = targetPosition;

        Debug.Log("Target Position: " + targetPosition);
        Debug.Log("Qubo's new position: " + quboTransform.position);
    }
}
