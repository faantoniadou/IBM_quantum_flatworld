using UnityEngine;

public class CloseButton : MonoBehaviour
{
    public void OnCloseButtonClick()
    {
        // Navigate up the hierarchy to get the desired parent
        Transform grandparentTransform = this.transform.parent?.parent?.parent;
        if (grandparentTransform != null)
        {
            GameObject label = grandparentTransform.gameObject;
            if (label != null)
            {
                label.SetActive(false); // Set the label GameObject to inactive
            }
        }
    }
}
