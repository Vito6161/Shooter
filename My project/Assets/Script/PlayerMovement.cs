using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float directionX, directionY;
    [SerializeField] private float speed;
    private Rigidbody2D rb => gameObject.GetComponent<Rigidbody2D>();

    // Update is called once per frame
    void Update()
    {
        directionX = Input.GetAxisRaw("Horizontal");
        directionY = Input.GetAxisRaw("Vertical");

        rb.linearVelocity = new Vector2(directionX, directionY) * speed;

    }
}
