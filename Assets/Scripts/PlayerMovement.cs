using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class PlayerMovement : MonoBehaviour
{
    #region Variables
    Animator anim;
    int jump_phase;
    [SerializeField]
    private GameObject run_col;
    [SerializeField]
    private GameObject slide_col;
    [SerializeField]
    private float slide_duration;
    public Vector3 input;
    public Vector3 velocity,maxVel;
    public float PlayerAcceleration, jumpHeight, gravity;
    [SerializeField] private Transform rc_origin;
    #endregion
    private void Awake()
    {
        jump_phase = -1;
        anim = GetComponent<Animator>();
        anim.SetTrigger("to_run");
    }
    void Update()
    {
        if(input!=Vector3.zero)
        {
            velocity += input * PlayerAcceleration * Time.deltaTime;
            input = Vector3.zero;
        }
        
        velocity.y -= gravity * Time.deltaTime; 

        if(jump_phase == 0)
        {
            if(velocity.y < 0)
            {
                anim.SetTrigger("to_fall");
                jump_phase = 1;
            }
        }
        else if(jump_phase == 1 && is_grounded())
        {
            anim.SetTrigger("to_land");
            jump_phase = -1;
        }
        
        if(!is_grounded() && jump_phase == -1)
        {
            jump_phase = 1;
            anim.SetTrigger("to_fall");
        }
        
        
    }

    private void LateUpdate()
    {
        Velocity_Move();
    }

    public void jump()
    {
        if(is_grounded())
        {
            velocity.y = jumpHeight;
            anim.SetTrigger("to_jump");
            jump_phase = 0;
        }
    }
    
    public bool is_grounded()
    {
        ContactFilter2D contactFilter2D = new ContactFilter2D
        {
            useLayerMask = true,
            layerMask = ~LayerMask.GetMask(new[] { "Player" })
        };
        Vector3 origin = rc_origin.position;
        float distance = 0.1f;
        RaycastHit2D[] results = new RaycastHit2D[10];
        int hitCount = Physics2D.Raycast(origin, Vector3.down, contactFilter2D, results, distance);
        if (hitCount != 0)
        {
            Debug.Log($"Hit gameObject {results[0].transform.name}");
            return true;
        }
        Debug.DrawRay(origin, Vector3.down * distance, Color.red, 1.0f);
        return false;
    }

    public void slide()
    {
        run_col.SetActive(false);
        slide_col.SetActive(true);
        anim.SetTrigger("to_slide");
        StartCoroutine(slide_timer());
    }

    private IEnumerator slide_timer()
    {
        yield return new WaitForSeconds(slide_duration);
        anim.SetTrigger("to_run");
        run_col.SetActive(true);
        slide_col.SetActive(false);
    }

    private void Velocity_Move()
    {
        velocity.x = Mathf.Clamp(velocity.x, 0, maxVel.x);
        //velocity.y = Mathf.Clamp(velocity.y, -5, maxVel.y);

        if (velocity.y < 0.0f)
        {
            ContactFilter2D contactFilter2D = new ContactFilter2D
            {
                useLayerMask = true,
                layerMask = ~LayerMask.GetMask(new[] { "Player" })
            };
            Vector3 origin = rc_origin.position;
            float distance = 0.1f;
            RaycastHit2D[] results = new RaycastHit2D[10];
            int hitCount = Physics2D.Raycast(origin, Vector3.down, contactFilter2D, results, distance);
            if (hitCount != 0)
            {
                Debug.Log($"Hit gameObject {results[0].transform.name}");
                velocity.y = -results[0].distance / Time.deltaTime;
            }
            Debug.DrawRay(origin, Vector3.down * distance, Color.black);
        }
        
        transform.position += velocity * Time.deltaTime;
    }
}

/*
RaycastHit2D[] HitResults = new RaycastHit2D[10];
ContactFilter2D cnt = new ContactFilter2D();
int abcd = Physics2D.Raycast(rc_origin.position, Vector2.down, cnt, HitResults, velocity.y * Time.deltaTime);
            if (abcd != 0)
            {
                Debug.DrawLine(rc_origin.position, rc_origin.position + (Vector3.down* HitResults[0].distance), Color.red);
                velocity.y = -HitResults[0].distance / Time.deltaTime;
            }
*/