using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitManager : MonoBehaviour
{
    [Header("Settings")]
    public string SceneName = "Ending";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            FinishLevel();
        }
    }
    private void FinishLevel()
    {
        int currentScore = 1000; 
        float timePlayed = Time.timeSinceLevelLoad;

        PlayerPrefs.SetInt("Score", currentScore);
        PlayerPrefs.SetFloat("FinalTime", timePlayed);
        PlayerPrefs.SetInt("MissionStatus", 1);
        SceneManager.LoadScene(SceneName);
    }
}