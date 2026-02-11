using UnityEngine;
using TMPro; 
using System.Collections;

public class ScoreBoard : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI TitleText; 
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI TimeText;
    public TextMeshProUGUI FinalScoreText;   
    public GameObject FilePanel;    

    [Header("Settings")]
    public float AnimationDuration = 2.0f;
    public Color WinColor = Color.green;
    public Color LoseColor = Color.red;
    public int Multiplier = 100;

    private void Start()
    {
        int Score = PlayerPrefs.GetInt("Score", 0);
        float FinalTime = PlayerPrefs.GetFloat("FinalTime", 1f);
        int MissionStatus = PlayerPrefs.GetInt("MissionStatus", 1);
        float TimeForCalculation = Mathf.Floor(FinalTime);
        
        if (FinalTime <= 0.1f) FinalTime = 1f;
        int TotalScoreVal = Mathf.RoundToInt((Score / TimeForCalculation) * Multiplier);
        
        if (MissionStatus == 1)
        {
            TitleText.text = "MISSION COMPLETED";
            TitleText.color = WinColor; 
        }
        else
        {
            TitleText.text = "MISSION FAILED";
            TitleText.color = LoseColor;
            TotalScoreVal = TotalScoreVal / 2;
        }
       
        ScoreText.text = "0000";
        TimeText.text = FormatTime(FinalTime);
        FinalScoreText.text = "0000"; 
        StartCoroutine(AnimateValue(ScoreText, Score));
        StartCoroutine(AnimateValue(FinalScoreText, TotalScoreVal));
    }
    private string FormatTime(float TimeInSeconds)
    {
        float Minutes = Mathf.FloorToInt(TimeInSeconds / 60);
        float Seconds = Mathf.FloorToInt(TimeInSeconds % 60);
        return string.Format("{0:00}:{1:00}", Minutes, Seconds);
    }
    private IEnumerator AnimateValue(TextMeshProUGUI TextComponent,int TargetScore)
    {
        yield return new WaitForSeconds(2f); 
        float TimeElapsed = 0f;
        int StartValue = 0;

        while (TimeElapsed < AnimationDuration)
        {
            TimeElapsed += Time.deltaTime;
            float Percentage = TimeElapsed / AnimationDuration;
            int CurrentValue = (int)Mathf.Lerp(StartValue, TargetScore, Percentage);
            TextComponent.text = CurrentValue.ToString("D4"); 
            
            yield return null;
        }
        TextComponent.text = TargetScore.ToString("D4");
    }
}