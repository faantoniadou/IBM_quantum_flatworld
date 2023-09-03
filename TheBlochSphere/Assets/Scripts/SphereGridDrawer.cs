using UnityEngine;

public class SphereGridDrawer : MonoBehaviour
{
    public int latitudeLines = 10;
    public int longitudeLines = 10;
    public float radius = 1f;
    public Material lineMaterial;

    private void Start()
    {
        DrawGrid();
    }

    void DrawGrid()
    {
        for (int i = 0; i <= latitudeLines; i++)
        {
            float lat = (i * Mathf.PI) / latitudeLines;
            DrawLatitudeLine(lat);
        }

        for (int i = 0; i <= longitudeLines; i++)
        {
            float lon = (i * 2 * Mathf.PI) / longitudeLines;
            DrawLongitudeLine(lon);
        }
    }

    void DrawLatitudeLine(float latitude)
    {
        GameObject lineObj = new GameObject("LatitudeLine");
        lineObj.transform.SetParent(this.transform, false);

        LineRenderer lr = lineObj.AddComponent<LineRenderer>();
        lr.material = lineMaterial;
        lr.positionCount = longitudeLines + 1;
        lr.useWorldSpace = false;
        lr.startWidth = 0.01f;
        lr.endWidth = 0.01f;

        for (int i = 0; i <= longitudeLines; i++)
        {
            float longitude = (i * 2 * Mathf.PI) / longitudeLines;
            Vector3 pos = new Vector3(
                radius * Mathf.Sin(latitude) * Mathf.Cos(longitude),
                radius * Mathf.Cos(latitude),
                radius * Mathf.Sin(latitude) * Mathf.Sin(longitude)
            );
            lr.SetPosition(i, pos);
        }
    }

    void DrawLongitudeLine(float longitude)
    {
        GameObject lineObj = new GameObject("LongitudeLine");
        lineObj.transform.SetParent(this.transform, false);

        LineRenderer lr = lineObj.AddComponent<LineRenderer>();
        lr.material = lineMaterial;
        lr.positionCount = latitudeLines + 1;
        lr.useWorldSpace = false;
        lr.startWidth = 0.01f;
        lr.endWidth = 0.01f;

        for (int i = 0; i <= latitudeLines; i++)
        {
            float latitude = (i * Mathf.PI) / latitudeLines;
            Vector3 pos = new Vector3(
                radius * Mathf.Sin(latitude) * Mathf.Cos(longitude),
                radius * Mathf.Cos(latitude),
                radius * Mathf.Sin(latitude) * Mathf.Sin(longitude)
            );
            lr.SetPosition(i, pos);
        }
    }
}
