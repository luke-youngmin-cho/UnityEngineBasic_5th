using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
public class DiceAnimationUI : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private float _animationDelay;
    [SerializeField] private float _animationTime;
    private float _animationDelayTimer;
    private List<Sprite> _sprites;

    public delegate void AnimationFinishedHandler(int diceValue);
    public AnimationFinishedHandler OnAnimationFinished2;

    public Action<int> OnAnimationFinished;

    private void Awake()
    {
        LoadSprites();
    }

    

    private void LoadSprites()
    {
        _sprites = Resources.LoadAll<Sprite>("DiceImages").ToList();
    }

    // Coroutine ( 협동 루틴 )
    // 어떤 함수가 반환 될 때 다른 함수를 호출해주고, 그 다른 함수도 반환할때 어떤 함수를 호출해주는 
    // 상호 협동하는 관계의 함수관계를 협동 루틴 이라고 한다.

    IEnumerator E_DiceAnimation(int diceValue)
    {
        float elapsedTime = 0.0f;
        while (elapsedTime < _animationTime)
        {
            if (_animationDelayTimer < 0)
            {
                _image.sprite = _sprites[Random.Range(0, _sprites.Count)];
                _animationDelayTimer = _animationDelay;
            }

            _animationDelayTimer -= Time.deltaTime;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _image.sprite = _sprites[diceValue - 1];
        OnAnimationFinished(diceValue);
    }
}
