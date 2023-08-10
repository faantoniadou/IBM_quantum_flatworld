using UnityEngine;

public class ExitGame : MonoBehaviour
{
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
//#elif UNITY_WEBGL
//        Application.ExternalEval("window.closeUnityGame();");
#else
        Application.Quit();
#endif
    }
}
