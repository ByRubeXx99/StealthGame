using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitManager : MonoBehaviour
{
    [Header("Settings")]
    public string SceneName = "Ending";

    private void OnTriggerEnter2D(Collider2D Other)
    {
        if (Other.CompareTag("Player"))
        {
            FinishLevel();
        }
    }
    private void FinishLevel()
    {
        int CurrentScore = 1000; 
        float TimePlayed = Time.timeSinceLevelLoad;

        PlayerPrefs.SetInt("Score", CurrentScore);
        PlayerPrefs.SetFloat("FinalTime", TimePlayed);
        PlayerPrefs.SetInt("MissionStatus", 1);
        SceneManager.LoadScene(SceneName);
    }
}