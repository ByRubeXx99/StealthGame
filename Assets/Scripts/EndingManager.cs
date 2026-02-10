using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class EndingManager : MonoBehaviour
{
    public InputAction exitAction;
    public InputAction restartAction;
    private void OnEnable()
    {
        exitAction.Enable();
        restartAction.Enable();
        exitAction.started += ExitGame;
        restartAction.started += Restart;
    }
    private void OnDisable()
    {
        exitAction.started -= ExitGame;
        restartAction.started -= Restart;
        exitAction.Disable();
        restartAction.Disable();
    }
    private void ExitGame(InputAction.CallbackContext context)
    {
        DisableAllInput();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    private void Restart(InputAction.CallbackContext context)
    {
        DisableAllInput();
        SceneManager.LoadScene("Title");
    }
    private void DisableAllInput()
    {
        exitAction.Disable();
        restartAction.Disable();
    }
}