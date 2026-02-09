using UnityEngine;

public class EnemyLookAround : MonoBehaviour
{
    public float rotationSpeed = 90f;
    public float exposureTime = 1.5f;
    public float rotationTime = 1f;
    public LayerMask wall;

    public Vector2 Idirection = Vector2.right;

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
                //direction *= -1;
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
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Idirection, 1f, wall);
        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Idirection);
    }
}
