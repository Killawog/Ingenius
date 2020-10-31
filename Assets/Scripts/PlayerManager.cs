using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    PlayerMovement ply;
    private void Awake()
    {
        ply = GetComponent<PlayerMovement>();
    }
    void start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        ply.input.x = 1;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ply.jump();
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            ply.slide();
        }
    }

    public void ReduceHealth_F()
    {
        Debug.Log("Reduced Health");
    }
}
