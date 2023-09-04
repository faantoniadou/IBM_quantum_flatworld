using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;

public class QuantumGateHandler : MonoBehaviour
{
    public Transform  quboTransform;
    public GameObject blochSphere; // Reference to the Bloch Sphere GameObject
    public ServerConfig serverConfig;

    private float        blochSphereRadius;


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
        string url = serverConfig.baseURL;
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
    }

    private IEnumerator ApplyGate(string gateName)
    {
        string url = serverConfig.baseURL + "/apply_gate";
        
        UnityWebRequest www = new(url, "POST");
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
