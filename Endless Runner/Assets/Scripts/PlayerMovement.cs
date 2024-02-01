using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //variables made public to allow editing in Unity editor
    public float speed = 5;
    public Rigidbody rb;

    private void FixedUpdate()
    {
        Vector3 forwardMove = transform.forward * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + forwardMove);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
