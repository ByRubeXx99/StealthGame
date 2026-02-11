using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CountdownTimer : MonoBehaviour
{
    public TMP_Text TimerText;
    public float StartTime = 30f;

    private float CurrentTime;
    private bool TimeUp = false;

    void Start()
    {
        CurrentTime = StartTime;
    }

    void Update()
    {
        if (TimeUp) return;

        if (CurrentTime > 0)
        {
            CurrentTime -= Time.deltaTime;
            if (CurrentTime < 0)
                CurrentTime = 0;

            TimerText.text = "Time: " + (int)CurrentTime;
        }

        if (CurrentTime <= 0 && !TimeUp)
        {
            TimeUp = true;
            OnTimeUp();
        }
    }

    void OnTimeUp()
    {
        PlayerPrefs.SetInt("MissionStatus", 0);
        PlayerPrefs.SetInt("Score", 0);
        PlayerPrefs.SetFloat("FinalTime", Time.timeSinceLevelLoad);
        SceneManager.LoadScene("Ending");
    }
}
