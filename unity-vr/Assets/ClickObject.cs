using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ClickObject : MonoBehaviour
{
    public GameObject cube;

    private Vector3 lastMousePosition; // To store the last position of the mouse

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (cube == GetClickedObject(out RaycastHit hit))
            {
                print("clicked/touched!");
                lastMousePosition = Input.mousePosition; // Store the initial mouse position
            }
        }
        else if (Input.GetMouseButton(0) && !isPointerOverUIObject())

            lastMousePosition = Input.mousePosition;

        else if (Input.GetMouseButtonUp(0))
        {
            //print("Mouse is off!");
        }
    }


GameObject GetClickedObject(out RaycastHit hit)
    {
        GameObject target = null;
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray.origin, ray.direction * 10, out hit))
        {
            Debug.Log("Raycast hit: " + hit.collider.name);
            if (!isPointerOverUIObject()) { target = hit.collider.gameObject; }
        }
        return target;

    }
    private bool isPointerOverUIObject()
    {
        PointerEventData ped = new PointerEventData(EventSystem.current);
        ped.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(ped, results);
        return results.Count > 0;
    }
}
