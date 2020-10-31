using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField]
    private List<Transform> Lvl_List;
    [SerializeField]
    private Transform Lvl_start;
    private Vector2 last_end_position;
    private const float playerdist = 18f;
    [SerializeField]
    private GameObject Player;
    private void Awake()
    {
        last_end_position = Lvl_start.Find("LevelEnd").position;
        //Debug.Log(last_end_position);

        spawn_lvl_part();
    }

    private void Update()
    {
        //Debug.Log(Vector2.Distance(Player.transform.position, last_end_position) < playerdist);
        if(Vector2.Distance(Player.transform.position, last_end_position) < playerdist)
        {
            spawn_lvl_part();
        }
    }

    private void spawn_lvl_part()
    {
        Transform chosen_lvl_part = Lvl_List[Random.Range(0, Lvl_List.Count)];
        Transform last_lvl_part = spawn_lvl_part(chosen_lvl_part, last_end_position);
        last_end_position = last_lvl_part.Find("LevelEnd").position;
    }


    private Transform spawn_lvl_part(Transform lvl_part, Vector2 spawnPosition)
    {
        Transform lvl_part_transform = Instantiate(lvl_part, spawnPosition, Quaternion.identity);
        return lvl_part_transform;
    }
}
