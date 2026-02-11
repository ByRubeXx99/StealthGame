using System;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EnemyPatrol : MonoBehaviour
{
    public LayerMask Wall, Player;

    public float Speed = 1f;
    public float DetectionRange = 2.5f;
    public float VisionAngle = 45f;

    private Vector2 Direction = Vector2.right;
    private Transform PlayerTransform;
    private bool PlayerDetected;

    enum EnemyState
    {
        Patrol,
        Chase
    }

    EnemyState CurrentState = EnemyState.Patrol;


    void Update()
    {
        bool WasChasing = CurrentState == EnemyState.Chase;
        PlayerDetected = PlayerDetector();

        if (PlayerDetected) CurrentState = EnemyState.Chase;
        else if (CurrentState == EnemyState.Chase) CurrentState = EnemyState.Patrol;
        if(WasChasing && CurrentState == EnemyState.Patrol) ResetPatrolDirection();

        switch (CurrentState)
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
    transform.Translate(Direction * Speed * Time.deltaTime, Space.World);
    LookAt(transform.position + (Vector3)Direction);

    Direction = transform.right;

    RaycastHit2D hit = Physics2D.Raycast(transform.position, Direction, DetectionRange, Wall);
    if (hit.collider != null) Direction *= -1; 
}

    private bool PlayerDetector()
    {
        Collider2D playerCol = Physics2D.OverlapCircle(transform.position, DetectionRange, Player);
        if (playerCol == null) return false;

        Vector2 dirToPlayer = ((Vector2)playerCol.transform.position - (Vector2)transform.position).normalized;
        float angle = Vector2.Angle(Direction, dirToPlayer);
        if (angle <= VisionAngle / 2f)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, dirToPlayer, DetectionRange, Wall | Player);
            if (hit.collider != null && hit.collider.CompareTag("Player"))
            {
                PlayerTransform = hit.collider.transform;
                return true;
            }
        }
        return false;
    }

    private void ChasePlayer()
    {
        if (PlayerTransform == null) return;

        Vector2 dir = ((Vector2)PlayerTransform.position - (Vector2)transform.position).normalized;

        transform.Translate(dir * Speed * Time.deltaTime, Space.World);
        LookAt(PlayerTransform.position);

        Direction = transform.right;
    }

    private void LookAt(Vector2 target)
    {
        Vector2 direction = target - (Vector2)transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void ResetPatrolDirection()
    {
        Direction = transform.right.x >= 0 ? Vector2.right : Vector2.left;
        
        float angle = Direction == Vector2.right ? 0f : 180f;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, DetectionRange);

        Gizmos.color = Color.yellow;
        Vector3 rightLimit = Quaternion.AngleAxis(VisionAngle / 2, Vector3.forward) * Direction;
        Vector3 leftLimit = Quaternion.AngleAxis(-VisionAngle / 2, Vector3.forward) * Direction;
        Gizmos.DrawRay(transform.position, rightLimit * DetectionRange);
        Gizmos.DrawRay(transform.position, leftLimit * DetectionRange);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene("Ending");
        }
    }
}