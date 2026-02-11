using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CountdownTimer : MonoBehaviour
{
    public TMP_Text timerText;
    public float startTime = 30f;

    private float currentTime;
    private bool timeUp = false;

    void Start()
    {
        currentTime = startTime;
    }

    void Update()
    {
        if (timeUp) return;

        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;

            if (currentTime < 0)
                currentTime = 0;

            timerText.text = "Time: " + (int)currentTime;
        }

        if (currentTime <= 0 && !timeUp)
        {
            timeUp = true;
            OnTimeUp();
        }
    }

    void OnTimeUp()
    {
        PlayerPrefs.SetInt("MissionStatus", 0);
        PlayerPrefs.SetInt("Score", 0);
        SceneManager.LoadScene("Ending");
    }
}
