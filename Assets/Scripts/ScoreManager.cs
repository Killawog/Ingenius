using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text ScoreValueLbl;
    [SerializeField] private PlayerManager playerManager;
    private Vector3 PlayerInitialPos;
    [SerializeField] private float Score_Multiplier;

    void Start()
    {
        PlayerInitialPos = playerManager.transform.position;
    }

    void Update()
    {
        ScoreValueLbl.text = (int)((playerManager.transform.position.x - PlayerInitialPos.x)/Score_Multiplier) + "";
    }
}
