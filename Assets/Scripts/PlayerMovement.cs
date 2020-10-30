using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Vector3 input;
    public Vector3 velocity,maxVel;
    public float PlayerAcceleration, jumpHeight, gravity;
    [SerializeField]
    private Transform rc_origin;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(input!=Vector3.zero)
        {
            velocity += input * PlayerAcceleration * Time.deltaTime;
            input = Vector3.zero;
            Debug.Log(velocity);
        }
        velocity.y -= gravity; 
        Velocity_Move();
    }

    void jump()
    {
        velocity.y = jumpHeight;
    }

    private void Velocity_Move()
    {
        velocity.x = Mathf.Clamp(velocity.x, 0, maxVel.x);
        velocity.y = Mathf.Clamp(velocity.y, -5, maxVel.y);
        
        RaycastHit2D [] HitResults = new RaycastHit2D [10];
        ContactFilter2D cnt = new ContactFilter2D();
        int abcd = Physics2D.Raycast(rc_origin.position, Vector2.down, cnt, HitResults, velocity.y * Time.deltaTime);
        if (abcd != 0)
        {
            Debug.DrawLine(rc_origin.position, rc_origin.position + (Vector3.down * HitResults[0].distance), Color.red);
            velocity.y = -HitResults[0].distance / Time.deltaTime;
        }
        
        transform.position += velocity * Time.deltaTime;
    }
}
