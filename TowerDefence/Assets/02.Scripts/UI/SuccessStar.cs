using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuccessStar : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _star;


    public void Show()
    {
        _star.SetActive(true);
        _animator.Play("Show");
    }
}
