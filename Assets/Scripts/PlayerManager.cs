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
    // Update is called once per frame
    void Update()
    {
        ply.input.x = 1;
    }
}
