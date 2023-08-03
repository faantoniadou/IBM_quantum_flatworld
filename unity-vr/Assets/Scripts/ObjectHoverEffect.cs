using UnityEngine;

public class HoverEffect : MonoBehaviour
{
    public Material hoverMaterial;
    private Material[] defaultMaterials;

    private void Start()
    {
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
}
