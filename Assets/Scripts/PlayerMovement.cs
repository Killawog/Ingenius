using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class PlayerMovement : MonoBehaviour
{
    #region Variables
    public Vector3 input;
    public Vector3 velocity,maxVel;
    public float PlayerAcceleration, jumpHeight, gravity;
    [SerializeField] private Transform rc_origin;
    #endregion

    void Update()
    {
        if(input!=Vector3.zero)
        {
            velocity += input * PlayerAcceleration * Time.deltaTime;
            input = Vector3.zero;
        }
        
        velocity.y -= gravity * Time.deltaTime; 
    }

    private void LateUpdate()
    {
        Velocity_Move();
    }

    public void jump()
    {
        ContactFilter2D contactFilter2D = new ContactFilter2D
        {
            useLayerMask = true, 
            layerMask = ~LayerMask.GetMask(new[] {"Player"})
        };
        Vector3 origin = transform.position;
        float distance = GetComponent<Collider2D>().bounds.extents.y + 0.1f;
        RaycastHit2D[] results = new RaycastHit2D[10];
        int hitCount = Physics2D.Raycast(origin, Vector3.down, contactFilter2D, results, distance);
        if (hitCount != 0)
        {
            Debug.Log($"Hit GameObject = {results[0].transform.name}\nHit point = {results[0].point}");
            velocity.y = jumpHeight;
        }
        Debug.DrawRay(origin, Vector3.down * distance, Color.red, 1.0f);
    }

    private void Velocity_Move()
    {
        velocity.x = Mathf.Clamp(velocity.x, 0, maxVel.x);
        //velocity.y = Mathf.Clamp(velocity.y, -5, maxVel.y);

        if (velocity.y < 0.0f)
        {
            RaycastHit2D [] HitResults = new RaycastHit2D [10];
            ContactFilter2D cnt = new ContactFilter2D();
            int abcd = Physics2D.Raycast(rc_origin.position, Vector2.down, cnt, HitResults, velocity.y * Time.deltaTime);
            if (abcd != 0)
            {
                Debug.DrawLine(rc_origin.position, rc_origin.position + (Vector3.down * HitResults[0].distance), Color.red);
                velocity.y = -HitResults[0].distance / Time.deltaTime;
            }
        }
        
        transform.position += velocity * Time.deltaTime;
    }
}
