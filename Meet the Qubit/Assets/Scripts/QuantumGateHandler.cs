using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class QuantumGateHandler : MonoBehaviour
{
    public Transform quboTransform;
    public GameObject blochSphere; // Reference to the Bloch Sphere GameObject

    private float blochSphereRadius;
    private string baseURL = "http://127.0.0.1:5000";

    [System.Serializable]
    public class BlochVectorResponse
    {
        public float[] bloch_vector;
    }

    private void Start()
    {
        blochSphereRadius = blochSphere.transform.localScale.x / 2; // Assuming the sphere is uniformly scaled
    }

    public void OnGateClick(string gateName)
    {
        Debug.Log($"{gateName.ToUpper()} Gate button clicked");
        StartCoroutine(ApplyGate(gateName));
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
            string jsonResponse = www.downloadHandler.text;
            BlochVectorResponse response = JsonUtility.FromJson<BlochVectorResponse>(jsonResponse);
            Vector3 blochVector = new Vector3(response.bloch_vector[0], response.bloch_vector[1], response.bloch_vector[2]);

            MoveQuboToPosition(blochVector);
            Debug.Log("Received Bloch Vector: " + jsonResponse);
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
