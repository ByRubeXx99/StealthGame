using UnityEngine;

public class DistanceCounter : MonoBehaviour
{
    private float TotalDistance;
    public int TotalDistanceInt => Mathf.FloorToInt(TotalDistance); 

    private Vector3 LastPosition;

    void Start()
    {
        LastPosition = transform.position;
        TotalDistance = 0f;
    }

    void Update()
    {
        float distanceThisFrame = Vector3.Distance(transform.position, LastPosition);
        TotalDistance += distanceThisFrame;
        LastPosition = transform.position;
    }
}