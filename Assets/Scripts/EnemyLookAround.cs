using UnityEngine;

public class EnemyLookAround : MonoBehaviour
{
    public float rotationSpeed = 90f;
    public float exposureTime = 1.5f;
    public float rotationTime = 1f;
    public float detectionRange = 2.5f;
    public float visionAngle = 45f;
    public LayerMask wall;

    private float timer;
    private bool waiting = true;
    private int direction = 1;

    private void Update()
    {
        LookAround();

        if (Collision())
        {
            ChangeDirection();
        }
    }

    private void LookAround()
    {
        timer += Time.deltaTime;

        if (waiting)
        {   
            if (timer >= exposureTime)
            {
                timer = 0f;
                waiting = false;
            }
            return;
        }
        
        float step = rotationSpeed * Time.deltaTime * direction;
        transform.Rotate(0, 0, step);

        if (timer >= rotationTime)
        {
            timer = 0f;
            waiting = true;
            direction *= -1;
        }
    }

    private void ChangeDirection()
    {
        direction *= -1;
        waiting = true;
        timer = 0f;
    }

    private bool Collision()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, 1f, wall);
        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.yellow;
        Vector3 rightLimit = Quaternion.AngleAxis(visionAngle / 2, Vector3.forward) * transform.right;
        Vector3 leftLimit = Quaternion.AngleAxis(-visionAngle / 2, Vector3.forward) * transform.right;

        Gizmos.DrawRay(transform.position, rightLimit * detectionRange);
        Gizmos.DrawRay(transform.position, leftLimit * detectionRange);
    }
}