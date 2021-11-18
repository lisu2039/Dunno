using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 3f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform rayOrigin;
    [SerializeField] private float rayDistance = 0.1f;
    
    private bool isGrounded;
    private bool canDoubleJump;
    
    private void FixedUpdate()
    {
        Move();
        CheckGround();
    }

    private void Update()
    {
           if (Input.GetButtonDown("Jump")){
              if (isGrounded){
                  Jump();
                  canDoubleJump = true;
              } else if(canDoubleJump){
                  Jump();
                  canDoubleJump = false;
              }
          }

    }
    
    
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Pickup pickup = collider.GetComponent<Pickup>();
        
        if (pickup != null)
        {
            pickup.Collect();
        }
    }
    
    private void Move()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            movementSpeed = 6f;
        }
        else
        {
            movementSpeed = 3f;
        }
        transform.position += Vector3.right * Input.GetAxis("Horizontal") * Time.deltaTime * movementSpeed;
    }

    private void Jump()
    {
        rb.AddForce(Vector2.up*jumpForce,ForceMode2D.Impulse);       
    }

    private void CheckGround()
    {
        RaycastHit2D raycastHit = Physics2D.Raycast(
            rayOrigin.position,
            Vector2.down,
            rayDistance);

        isGrounded = raycastHit.collider != null;
        
        if (isGrounded)
        {
            Debug.Log(raycastHit.collider.name);
        }
        
    }
}
