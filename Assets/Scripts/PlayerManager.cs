using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> Lives;
    [SerializeField] private GameObject StartScreen;
    int health;
    Animator anim;
    int play_is_dead;
    PlayerMovement ply;
    private void Awake()
    {
        play_is_dead = 0;
        anim = GetComponent<Animator>();
        health = 3;
        ply = GetComponent<PlayerMovement>();
    }
    void start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if (play_is_dead == 0)
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
    }

    private IEnumerator deathTime()
    {
        anim.SetTrigger("to_death");
        ply.velocity.x = 0;
        play_is_dead = 1;
        
        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene("Main-Menu", LoadSceneMode.Single);
    }
    public void ReduceHealth_F()
    {
        health--;
        GetComponentInChildren<SpriteRenderer>().DOFade(0f, 0.1f).SetLoops(3).OnComplete(()=> GetComponentInChildren<SpriteRenderer>().color = Color.white);
        RefreshHealth_UI();
        if (health == 0)
        {
            StartCoroutine(deathTime());
        }
    }
    public void RefreshHealth_UI()
    {
        for(int i = 0; i < 3 ; i++)
        {
            Lives[i].SetActive(i<health);
        }
    }
}
