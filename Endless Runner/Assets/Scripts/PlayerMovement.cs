using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //variables made public to allow editing in Unity editor
    public float speed = 5;
    public Rigidbody rb;
    public float horizontalMultiplier = 2;

    float horizontalInput;
    
    private void FixedUpdate()
    {
        Vector3 forwardMove = transform.forward * speed * Time.fixedDeltaTime;
        Vector3 horizontalMove = transform.right * horizontalInput * speed * Time.fixedDeltaTime * horizontalMultiplier;
        rb.MovePosition(rb.position + forwardMove + horizontalMove);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
    }
}
