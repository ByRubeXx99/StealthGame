using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float Speed = 5.0f;

    Rigidbody2D Rigidbody;
    private bool isMoving;

    private int Score = 2000;

    public bool IsMoving => isMoving;
    void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    public void OnMove(InputValue value)
    {
        var MoveDir = value.Get<Vector2>();

        Vector2 velocity = MoveDir * Speed;
        Rigidbody.linearVelocity = velocity;

        isMoving = (velocity.magnitude > 0.01f);

        if (isMoving)
        {
            float Angle = Mathf.Atan2(MoveDir.y, MoveDir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, Angle); 
          
        }
    }

    public void OnSaveScore()
    {
        // Usage example on how to save score
        PlayerPrefs.SetInt("Score", Score);
        Score = PlayerPrefs.GetInt("Score");
    }
}
