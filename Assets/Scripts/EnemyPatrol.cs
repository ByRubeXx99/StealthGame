using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{    
    public LayerMask wall,player;
    public Vector2 Idirection = Vector2.right;
    private Transform PlayerTransform;

    public float  Speed = 3;
    public float DetectionRange;
    public float VisionAngle;
    public bool PlayerDetected = false;

    private void Update()
    {
        PlayerDetected = PlayerDetector();

        if(!PlayerDetected)
        {
            Patrol();
            if(Collision()) { transform.Rotate(Vector3.forward * 180); Idirection *= -1; }
        }
        else
        {
            Chase();
        }
    }

    private void Patrol()
    {
        transform.Translate(Idirection*Speed*Time.deltaTime,Space.World);
    }

    private bool Collision()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position,Idirection,DetectionRange,wall|player);
        if (hit.collider!=null)
        {
            if (hit.collider.CompareTag("Wall"))
            {
              
                return true;
            }
         
        }
        return false;
    }
    
    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position, Idirection, Color.red);

        Gizmos.DrawWireSphere(transform.position, DetectionRange);

        Gizmos.color = Color.yellow;

        var Direction = Quaternion.AngleAxis(VisionAngle/2, Vector3.forward) * transform.right;
        Gizmos.DrawRay(transform.position, Direction * DetectionRange);

        var Direction2 = Quaternion.AngleAxis(-VisionAngle/2, Vector3.forward) * transform.right;
        Gizmos.DrawRay(transform.position, Direction2 * DetectionRange);

        Gizmos.color = Color.white;
    }

    private bool PlayerDetector()
    {
       Collider2D playerCol = Physics2D.OverlapCircle(transform.position, DetectionRange, player);
        if (playerCol == null) return false;
        else 
        {
            Vector2 dirToPlayer = ((Vector2)playerCol.transform.position - (Vector2)transform.position).normalized;
           
            float angle = Vector2.Angle(Idirection, dirToPlayer);

            if (angle <= VisionAngle / 2f)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, dirToPlayer, DetectionRange, wall | player);
                if (hit.collider != null && hit.collider.CompareTag("Player"))
                {
                    Debug.Log("Player found");
                    return true;
                }
            }
           // return false;
        }
        return false;
    } 
    private void Chase()
    {
        Collider2D playerCol2 = Physics2D.OverlapCircle(transform.position, DetectionRange, player);

        Vector2 dirToPlayer = ((Vector2)playerCol2.transform.position - (Vector2)transform.position).normalized;
        transform.Translate(dirToPlayer*Speed*Time.deltaTime,Space.World);
        Idirection = dirToPlayer;
    }
}