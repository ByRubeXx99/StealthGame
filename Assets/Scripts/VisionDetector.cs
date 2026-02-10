using System.Collections.Generic;
using UnityEngine;

public class VisionDetector : MonoBehaviour
{
    public LayerMask Target; // Player Layer
    public LayerMask WhatIsVisible; // Obstacle Layer
    public Transform CurrentTarget;

    public float DetectionRange; // Max Distance of vision
    public float VisionAngle;

    private float MoveSpeed = 3f;
    private float LoseTargetTime = 1.5f;
    private float LoseTimer;

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, DetectionRange);

        Gizmos.color = Color.yellow;

        var Direction = Quaternion.AngleAxis(VisionAngle/2, Vector3.forward) * transform.right;
        Gizmos.DrawRay(transform.position, Direction * DetectionRange);

        var Direction2 = Quaternion.AngleAxis(-VisionAngle/2, Vector3.forward) * transform.right;
        Gizmos.DrawRay(transform.position, Direction2 * DetectionRange);

        Gizmos.color = Color.white;
    }

    private void Update()
    {
        Transform detected = DetectTarget();

        if (detected != null)
        {
            CurrentTarget = detected;
            LoseTimer = LoseTargetTime;
        }
        else if (CurrentTarget != null)
        {
            LoseTimer -= Time.deltaTime;

            if (LoseTimer <= 0) CurrentTarget = null;
        }
        if (CurrentTarget != null) ChasePlayer();
    }

    private Transform DetectTarget()
    {
        Transform[] players = DetectPlayers();
        return players.Length > 0 ? players[0] : null;
    }

    private Transform[] DetectPlayers()
    {
        List<Transform> players = new List<Transform>();

        if (PlayerInRange(ref players))
        {
            if (PlayerInAngle(ref players))
            {
                PlayerIsVisible(ref players);
            }
        }

        return players.ToArray();
    }

    private bool PlayerInRange(ref List<Transform> players)
    {
        bool result = false;
        Collider2D[] playerColliders = Physics2D.OverlapCircleAll(transform.position, DetectionRange, Target);

        if (playerColliders.Length != 0)
        {
            result = true;

            foreach (var item in playerColliders)
            {
                players.Add(item.transform);
            }
        }

        return result;
    }

    private bool PlayerInAngle(ref List<Transform> players)
    {
        for (int i = players.Count - 1; i >= 0; i--)
        {
            var angle = GetAngle(players[i]);
           
            if (angle > VisionAngle/2)
            {
                players.Remove(players[i]);
            }
        }

        return (players.Count > 0);
    }

     private float GetAngle(Transform target)
    {
        Vector2 targetDir = target.position - transform.position;
        float angle = Vector2.Angle(targetDir, transform.right);
        
        return angle;
    }

    private bool PlayerIsVisible(ref List<Transform> players)
    {
        for (int i = players.Count - 1; i >= 0; i--)
        {
            var isVisible = IsVisible(players[i]);

            if (!isVisible)
            {
                players.Remove(players[i]);
            }
        }

        return (players.Count > 0);
    }

    private void ChasePlayer()
    {
        Vector2 Direction = (CurrentTarget.position - transform.position).normalized;
        transform.position += (Vector3)(Direction * MoveSpeed * Time.deltaTime);

        // Gira l'enemic cap al Player. Eliminar si no es vol que giri.
        if (Direction.x != 0) transform.right = Direction.x > 0 ? Vector3.right : Vector3.left;
    }

    private bool IsVisible(Transform target)
    {
        Vector3 dir = target.position - transform.position;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, DetectionRange);

        if (hit.collider == null) return false;

        return hit.collider.transform == target;
    }

}