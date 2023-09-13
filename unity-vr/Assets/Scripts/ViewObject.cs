using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ViewComponent : MonoBehaviour
{
    public Dictionary<GameObject, Transform> componentToFocusMap;
    public Transform defaultFocus; // Default camera position if no mapping is found
    public float focusDuration = 2f; // Time it takes to move to the focus point
    public GameObject parentObject; // The parent object containing all components as children

    private GameObject currentComponent; // The component that is currently in focus

    private void Start()
    {
        componentToFocusMap = new Dictionary<GameObject, Transform>();
    }

    public void OnViewButtonClick(GameObject component)
    {
        // Hide all components except the selected one
        foreach (Transform child in parentObject.transform)
        {
            child.gameObject.SetActive(false);
        }
        component.SetActive(true);
        currentComponent = component;

        Transform focusPoint;
        if (componentToFocusMap.TryGetValue(component, out focusPoint))
        {
            StartCoroutine(MoveToFocus(focusPoint));
        }
        else
        {
            StartCoroutine(MoveToFocus(defaultFocus));
        }
    }

    public void OnBackButtonClick()
    {
        // Show all components again
        foreach (Transform child in parentObject.transform)
        {
            child.gameObject.SetActive(true);
        }
        currentComponent = null;
    }

    private IEnumerator MoveToFocus(Transform focusPoint)
    {
        Vector3 initialPosition = Camera.main.transform.position;
        Quaternion initialRotation = Camera.main.transform.rotation;
        float elapsedTime = 0f;

        while (elapsedTime < focusDuration)
        {
            Camera.main.transform.position = Vector3.Lerp(initialPosition, focusPoint.position, elapsedTime / focusDuration);
            Camera.main.transform.rotation = Quaternion.Lerp(initialRotation, focusPoint.rotation, elapsedTime / focusDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Camera.main.transform.position = focusPoint.position;
        Camera.main.transform.rotation = focusPoint.rotation;
    }
}
