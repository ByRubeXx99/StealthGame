using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public LayerMask wall, player;

    public float speed = 1f;
    public float detectionRange = 2.5f;
    public float visionAngle = 45f;

    private Vector2 direction = Vector2.right;
    private Transform playerTransform;
    private bool playerDetected;

    enum EnemyState
    {
        Patrol,
        Chase
    }

    EnemyState currentState = EnemyState.Patrol;


    void Update()
    {
        playerDetected = PlayerDetector();

        if (playerDetected) currentState = EnemyState.Chase;
        else if (currentState == EnemyState.Chase) currentState = EnemyState.Patrol;

        switch (currentState)
        {
            case EnemyState.Patrol:
                Patrol();
                break;
            case EnemyState.Chase:
                ChasePlayer();
                break;
        }
    }

    private void Patrol()
    {
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
        LookAt(transform.position + (Vector3)direction);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, detectionRange, wall);
        if (hit.collider != null)
        {
            direction *= -1;
        }
    }

    private bool PlayerDetector()
    {
        Collider2D playerCol = Physics2D.OverlapCircle(transform.position, detectionRange, player);
        if (playerCol == null) return false;

        Vector2 dirToPlayer = ((Vector2)playerCol.transform.position - (Vector2)transform.position).normalized;
        float angle = Vector2.Angle(direction, dirToPlayer);

        if (angle <= visionAngle / 2f)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, dirToPlayer, detectionRange, wall | player);
            if (hit.collider != null && hit.collider.CompareTag("Player"))
            {
                playerTransform = hit.collider.transform;
                return true;
            }
        }
        return false;
    }

    private void ChasePlayer()
    {
        if (playerTransform == null) return;

        Vector2 dir = ((Vector2)playerTransform.position - (Vector2)transform.position).normalized;
        direction = dir;
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
        LookAt(playerTransform.position);
    }

    /*private void Flip(float x)
    {
        transform.localScale = new Vector3(x >= 0 ? 1 : -1, 1, 1);
    }
    */
    void LookAt(Vector2 target)
    {
        float dirX = target.x - transform.position.x;
        if (Mathf.Abs(dirX) < 0.01f) return;

        transform.localScale = new Vector3(dirX > 0 ? 1 : -1.25f, 1.25f, 1.25f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.yellow;
        Vector3 rightLimit = Quaternion.AngleAxis(visionAngle / 2, Vector3.forward) * direction;
        Vector3 leftLimit = Quaternion.AngleAxis(-visionAngle / 2, Vector3.forward) * direction;

        Gizmos.DrawRay(transform.position, rightLimit * detectionRange);
        Gizmos.DrawRay(transform.position, leftLimit * detectionRange);
    }
}