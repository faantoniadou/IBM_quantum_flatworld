using UnityEngine;

public class CloseButton : MonoBehaviour
{
    public void OnCloseButtonClick()
    {
        GameObject labelPrefab = GameObject.Find("LabelPrefab");
        if (labelPrefab != null)
        {
            labelPrefab.SetActive(false); // Set the label GameObject to inactive
        }
    }
}
