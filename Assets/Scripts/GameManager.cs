using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public InputAction ExitAction;

    private void OnEnable()
    {
        ExitAction.Enable();
        ExitAction.started += ExitGame;
    }
    private void OnDisable()
    {
        ExitAction.started -= ExitGame;
        ExitAction.Disable();
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