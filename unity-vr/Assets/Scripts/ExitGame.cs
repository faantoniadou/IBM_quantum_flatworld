using UnityEngine;
using System.Runtime.InteropServices;


public class ExitGame : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void closewindow();

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
        closewindow();  
#endif
    }
}
