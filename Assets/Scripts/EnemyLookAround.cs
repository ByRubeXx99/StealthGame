using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyLookAround : MonoBehaviour
{
    public float RotationSpeed = 90f;
    public float ExposureTime = 1.5f;
    public float RotationTime = 1f;
    public float DetectionRange = 2.5f;
    public float VisionAngle = 45f;
    public LayerMask Wall;
    private float Timer;
    private bool Waiting = true;
    private int Direction = 1;

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
        Timer += Time.deltaTime;

        if (Waiting)
        {   
            if (Timer >= ExposureTime)
            {
                Timer = 0f;
                Waiting = false;
            }
            return;
        }
        
        float step = RotationSpeed * Time.deltaTime * Direction;
        transform.Rotate(0, 0, step);

        if (Timer >= RotationTime)
        {
            Timer = 0f;
            Waiting = true;
            Direction *= -1;
        }
    }

    private void ChangeDirection()
    {
        Direction *= -1;
        Waiting = true;
        Timer = 0f;
    }

    private bool Collision()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, 1f, Wall);
        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, DetectionRange);

        Gizmos.color = Color.yellow;
        Vector3 rightLimit = Quaternion.AngleAxis(VisionAngle / 2, Vector3.forward) * transform.right;
        Vector3 leftLimit = Quaternion.AngleAxis(-VisionAngle / 2, Vector3.forward) * transform.right;
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