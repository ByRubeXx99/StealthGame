using TMPro;
using UnityEngine;

public class DistanceUI : MonoBehaviour
{
    public TMP_Text DistanceText;
    public DistanceCounter Counter;

    void Update()
    {
        if (Counter == null) return;
        DistanceText.text = $"Distance: {Counter.TotalDistanceInt} m";
    }
}
