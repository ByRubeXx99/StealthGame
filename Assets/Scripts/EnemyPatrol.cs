using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{    
    public float  Speed = 3;
    public LayerMask wall;
    public Vector2 Idirection=Vector2.right;

    private void Update()
    {
        Patrol();
      if(Collision()){ transform.Rotate(0, 0, 180); Idirection=Idirection*-1; }
    } 

    private void Patrol()
    {
        transform.Translate(Idirection*Speed*Time.deltaTime,Space.World);
    }

    private bool Collision()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position,Idirection,1f,wall); 
        if (hit.collider!=null)
        {
            Debug.Log("paret trobada");
            return true;
        } 
        return false;
    }
    
    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position, Idirection, Color.red);
    }
}