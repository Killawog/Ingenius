using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Screen_Manager : MonoBehaviour
{
    [SerializeField]
    private GameObject button;

    private void Start()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(button.transform.DOScale(1.1f, 1f));
        seq.Append(button.transform.DOScale(1f, 1f));
        seq.SetLoops(-1);
    }
    public void OnClick()
    {
        SceneManager.LoadScene("SampleScene",LoadSceneMode.Single);    
    }
}
