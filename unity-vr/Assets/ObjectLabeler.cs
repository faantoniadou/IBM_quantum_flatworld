using UnityEngine;
using UnityEngine.UI;

public class ObjectLabeler : MonoBehaviour
{
    public string description; // The description for this part
    public GameObject labelPrefab; // The UI Text or Panel prefab
    public Vector2 screenPosition; // Position on the screen where the label should appear

    private GameObject currentLabel; // The label currently being displayed
    private GameObject outline; // The outline object

    private void Start()
    {
        // Create the outline by duplicating the current object
        outline = Instantiate(gameObject, transform.position, transform.rotation, transform.parent);
        outline.transform.localScale = new Vector3(1.05f, 1.05f, 1.05f); // Slightly larger than the original object
        outline.GetComponent<Renderer>().material.color = Color.black; // Set outline color
        outline.SetActive(false); // Initially set it to inactive
    }

    void OnMouseOver()
    {
        outline.SetActive(true); // Show the outline when the mouse hovers over the object
    }

    void OnMouseExit()
    {
        outline.SetActive(false); // Hide the outline when the mouse is no longer over the object

        if (currentLabel != null) Destroy(currentLabel); // Destroy the label
    }

    void OnMouseDown()
    {
        if (currentLabel != null) Destroy(currentLabel); // Destroy the current label if it exists

        currentLabel = Instantiate(labelPrefab, Vector3.zero, Quaternion.identity); // Create a new label
        currentLabel.transform.SetParent(GameObject.Find("Canvas").transform, false); // Set the canvas as the parent
        currentLabel.GetComponentInChildren<Text>().text = description; // Set the text of the label

        // Position the label at the specified screen position
        RectTransform canvasRect = GameObject.Find("Canvas").GetComponent<RectTransform>();
        currentLabel.GetComponent<RectTransform>().anchoredPosition = new Vector2(canvasRect.sizeDelta.x * screenPosition.x, canvasRect.sizeDelta.y * screenPosition.y);

        // Add functionality to the 'X' button to close the label
        Button closeButton = currentLabel.GetComponentInChildren<Button>(); // Assuming the button is a child of the label prefab
        closeButton.onClick.AddListener(() => Destroy(currentLabel));
    }
}
