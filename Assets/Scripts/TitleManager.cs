using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class TitleManager : MonoBehaviour
{
    public InputAction exitAction;
    public InputAction startAction;
    private bool canInput = false;

    private void Start()
    {
        Invoke(nameof(EnableInput), 0.15f);
    }
    private void EnableInput()
    {
        canInput = true;
    }
    private void OnEnable()
    {
        exitAction.Enable();
        startAction.Enable();
        exitAction.started += ExitGame;
        startAction.started += StartGame;
    }
    private void OnDisable()
    {
        exitAction.started -= ExitGame;
        startAction.started -= StartGame;

        exitAction.Disable();
        startAction.Disable();
    }
    private void ExitGame(InputAction.CallbackContext context)
    {
        if (!canInput) return;
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    private void StartGame(InputAction.CallbackContext context)
    {
        if (!canInput) return;
        SceneManager.LoadScene("Gameplay");
    }
}