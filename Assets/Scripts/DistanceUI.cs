using TMPro;
using UnityEngine;

public class DistanceUI : MonoBehaviour
{
    public TMP_Text distanceText;
    public DistanceCounter counter;

    void Update()
    {
        if (counter == null) return;

        distanceText.text = $"Distance: {counter.TotalDistanceInt} units";
    }
}
