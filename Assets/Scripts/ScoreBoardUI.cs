using UnityEngine;
using TMPro; 
using System.Collections;

public class ScoreBoard : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI TitleText; 
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI TimeText;   
    public GameObject FilePanel;    

    [Header("Settings")]
    public float AnimationDuration = 2.0f;
    public Color WinColor = Color.green;
    public Color LoseColor = Color.red; 

    private void Start()
    {
        int finalScore = PlayerPrefs.GetInt("Score", 0);
        float finalTime = PlayerPrefs.GetFloat("FinalTime", 0f);
        int missionStatus = PlayerPrefs.GetInt("MissionStatus", 1);

        if (missionStatus == 1)
        {
            TitleText.text = "MISSION COMPLETED";
            TitleText.color = WinColor; 
        }
        else
        {
            TitleText.text = "MISSION FAILED";
            TitleText.color = LoseColor;
        }
       
        ScoreText.text = "0000";
        TimeText.text = FormatTime(finalTime); 
        StartCoroutine(AnimateScore(finalScore));
    }
    private string FormatTime(float timeInSeconds)
    {
        float minutes = Mathf.FloorToInt(timeInSeconds / 60);
        float seconds = Mathf.FloorToInt(timeInSeconds % 60);

        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    private IEnumerator AnimateScore(int targetScore)
    {
        yield return new WaitForSeconds(2f); 
        float timeElapsed = 0f;
        int startValue = 0;

        while (timeElapsed < AnimationDuration)
        {
            timeElapsed += Time.deltaTime;
            float percentage = timeElapsed / AnimationDuration;
            int currentValue = (int)Mathf.Lerp(startValue, targetScore, percentage);

            ScoreText.text = currentValue.ToString("D4"); 
            yield return null;
        }
        ScoreText.text = targetScore.ToString("D4");
    }
}