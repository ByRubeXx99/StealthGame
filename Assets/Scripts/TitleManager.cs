using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class TitleManager : MonoBehaviour
{
    public InputAction ExitAction;
    public InputAction StartAction;
    private bool CanInput = false;

    private void Start()
    {
        Invoke(nameof(EnableInput), 0.15f);
    }
    private void EnableInput()
    {
        CanInput = true;
    }
    private void OnEnable()
    {
        ExitAction.Enable();
        StartAction.Enable();
        ExitAction.started += ExitGame;
        StartAction.started += StartGame;
    }
    private void OnDisable()
    {
        ExitAction.started -= ExitGame;
        StartAction.started -= StartGame;

        ExitAction.Disable();
        StartAction.Disable();
    }
    private void ExitGame(InputAction.CallbackContext context)
    {
        if (!CanInput) return;
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    private void StartGame(InputAction.CallbackContext context)
    {
        if (!CanInput) return;
        SceneManager.LoadScene("Gameplay");
    }
}