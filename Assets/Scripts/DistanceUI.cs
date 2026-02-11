using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DistanceUI : MonoBehaviour
{
    public TMP_Text distanceText;
    public DistanceCounter counter;

    void Update()
    {
        if (counter == null) return;

        distanceText.text = $"Distance: {counter.totalDistance:F2} units";
    }
}
