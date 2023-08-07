using UnityEngine;
using UnityEngine.UI;

public class CombinedEffect : MonoBehaviour
{
    // HoverEffect variables
    public Material hoverMaterial;
    private Material[] defaultMaterials;

    // ObjectLabeler variables
    public string title; // The title for this part
    public string description; // The description for this part
    public GameObject labelPrefab; // The UI Text or Panel prefab
    public Vector2 screenPosition; // Position on the screen where the label should appear

    private GameObject currentLabel; // The label currently being displayed

    private void Start()
    {
        // HoverEffect Start logic
        defaultMaterials = GetComponent<Renderer>().materials;
    }

    private void OnMouseEnter()
    {
        if (hoverMaterial)
        {
            GetComponent<Renderer>().material = hoverMaterial;
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto); // Change to hand cursor
        }
    }

    private void OnMouseExit()
    {
        GetComponent<Renderer>().materials = defaultMaterials;
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto); // Change back to default cursor
    }

    void OnMouseDown()
    {
        if (currentLabel != null) Destroy(currentLabel); // Destroy the current label if it exists

        currentLabel = Instantiate(labelPrefab, Vector3.zero, Quaternion.identity); // Create a new label
        currentLabel.transform.SetParent(GameObject.Find("Canvas").transform, false); // Set the canvas as the parent
        currentLabel.SetActive(true); // Ensure the label is active
        currentLabel.GetComponentInChildren<Text>().text = description; // Set the text of the label

        // Set the position and scale of the label
        RectTransform rectTransform = currentLabel.GetComponent<RectTransform>();
        rectTransform.offsetMin = new Vector2(421.34f, 653f); // Left, Bottom
        rectTransform.offsetMax = new Vector2(-2351.993f, -907f); // -Right, -Top (because it's from the top-right corner)
        rectTransform.localPosition = new Vector3(rectTransform.localPosition.x, rectTransform.localPosition.y, 813.61f);
        currentLabel.transform.localScale = new Vector3(1.896934f, 1.21544f, 1.407513f);

        // Add functionality to the 'X' button to close the label
        Button closeButton = currentLabel.GetComponentInChildren<Button>(); // Assuming the button is a child of the label prefab
        closeButton.onClick.AddListener(() => Destroy(currentLabel));

        // Get all Text components in the label
        Text[] texts = currentLabel.GetComponentsInChildren<Text>();
        foreach (Text textComponent in texts)
        {
            if (textComponent.name == "TitleLabel") // Assuming the title Text component is named "TitleLabel"
            {
                textComponent.text = title;
            }
            else if (textComponent.name == "DescriptionField") // Assuming the description Text component is named "DescriptionField"
            {
                textComponent.text = description;
            }
        }
    }
}
