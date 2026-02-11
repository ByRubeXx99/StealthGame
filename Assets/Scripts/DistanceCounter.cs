using UnityEngine;

public class DistanceCounter : MonoBehaviour
{
    private float totalDistance;
    public int TotalDistanceInt => Mathf.FloorToInt(totalDistance); 

    private Vector3 lastPosition;

    void Start()
    {
        lastPosition = transform.position;
        totalDistance = 0f;
    }

    void Update()
    {
        float distanceThisFrame = Vector3.Distance(transform.position, lastPosition);
        totalDistance += distanceThisFrame;
        lastPosition = transform.position;
    }
}
