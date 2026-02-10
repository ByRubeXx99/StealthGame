using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public InputAction exitAction;

    private void OnEnable()
    {
        exitAction.Enable();
        exitAction.started += ExitGame;
    }
    private void OnDisable()
    {
        exitAction.started -= ExitGame;
        exitAction.Disable();
    }
    private void ExitGame(InputAction.CallbackContext context)
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void LoadEnding()
    {
        SceneManager.LoadScene("Ending");
    }
}