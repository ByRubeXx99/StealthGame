using UnityEngine;

public class EnemyLookAround : MonoBehaviour
{
    public float rotationSpeed = 90f;
    public float exposureTime = 0.5f;
    public float rotationTime = 1f;
    public LayerMask wall;
    public float detectionRange = 2.5f;
    public Vector2 Idirection = Vector2.down;
    public float visionAngle = 45f;
    private float timer;
    private bool waiting = true;
    private int direction = -1;

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
                direction *= -1;
            }
            return;
        }
        
        float step = rotationSpeed * Time.deltaTime * direction;
        transform.Rotate(0, 0, step);

        // Updates direction of looking
        Idirection = transform.right;

        if (timer >= rotationTime)
        {
            timer = 0f;
            waiting = true;
            direction *= 1;
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
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Idirection, 1f, wall);
        return hit.collider != null;
    }

   
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.yellow;
        Vector3 rightLimit = Quaternion.AngleAxis(visionAngle / 2, Vector3.forward) * Idirection;
        Vector3 leftLimit = Quaternion.AngleAxis(-visionAngle / 2, Vector3.forward) * Idirection;

        Gizmos.DrawRay(transform.position, rightLimit * detectionRange);
        Gizmos.DrawRay(transform.position, leftLimit * detectionRange);
    }

}
