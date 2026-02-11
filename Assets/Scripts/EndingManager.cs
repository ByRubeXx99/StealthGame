using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class EndingManager : MonoBehaviour
{
    public InputAction ExitAction;
    public InputAction RestartAction;
    private void OnEnable()
    {
        ExitAction.Enable();
        RestartAction.Enable();
        ExitAction.started += ExitGame;
        RestartAction.started += Restart;
    }
    private void OnDisable()
    {
        ExitAction.started -= ExitGame;
        RestartAction.started -= Restart;
        ExitAction.Disable();
        RestartAction.Disable();
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
        ExitAction.Disable();
        RestartAction.Disable();
    }
}