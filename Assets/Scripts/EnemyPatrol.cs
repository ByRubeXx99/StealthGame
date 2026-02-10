using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float  Speed = 3;
    public LayerMask wall,player;
    public Vector2 Idirection=Vector2.right;
    // Update is called once per frame 
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
        RaycastHit2D hit = Physics2D.Raycast(transform.position,Idirection,1f,wall|player);
        if (hit.collider!=null) {
            if (hit.collider.CompareTag("Wall"))
            {
                Debug.Log("paret trobada");
                return true;
            }
            else if (hit.collider.CompareTag("Player"))
            {
                Debug.Log("Player trobat ");
                Speed = 0;
            }
        }
        return false;
    }
    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position, Idirection, Color.red);
    }
}