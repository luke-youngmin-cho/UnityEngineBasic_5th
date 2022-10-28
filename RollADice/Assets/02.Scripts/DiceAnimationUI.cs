using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
public class DiceAnimationUI : MonoBehaviour
{
    public static DiceAnimationUI Instance;

    [SerializeField] private Image _image;
    [SerializeField] private Button _normalDiceButton;
    [SerializeField] private Button _goldenDiceButton;

    [SerializeField] private float _animationDelay;
    [SerializeField] private float _animationTime;
    private float _animationDelayTimer;
    private List<Sprite> _sprites;

    public event Action OnAnimationStarted;
    public event Action<int> OnAnimationFinished;

    public void PlayDiceAnimation(int diceValue)
    {
        StartCoroutine(E_DiceAnimation(diceValue));
    }

    private void Awake()
    {
        Instance = this;
        LoadSprites();
        OnAnimationStarted += () =>
        {
            _normalDiceButton.interactable = false;
            _goldenDiceButton.interactable = false;
        };
        OnAnimationFinished += (diceValue) =>
        {
            _normalDiceButton.interactable = true;
            _goldenDiceButton.interactable = true;
        };
    }    

    private void LoadSprites()
    {
        _sprites = Resources.LoadAll<Sprite>("DiceImages").ToList();
    }

    // Coroutine ( ���� ��ƾ )
    // � �Լ��� ��ȯ �� �� �ٸ� �Լ��� ȣ�����ְ�, �� �ٸ� �Լ��� ��ȯ�Ҷ� � �Լ��� ȣ�����ִ� 
    // ��ȣ �����ϴ� ������ �Լ����踦 ���� ��ƾ �̶�� �Ѵ�.

    // Monobehavior �� �ڷ�ƾ�� �ش� �ڷ�ƾ�� ȣ���� MonoBehavior �� Update �̺�Ʈ�� Coroutine�� �����Ѵ�.
    // ��, �ڷ�ƾ�� ������ ��ü MonoBehavior �� ��Ȱ��ȭ �� ��� (Update �̺�Ʈ�Լ��� ȣ����� ���ϰ� �� ���)
    // , �ش� Coroutine �� ���ư��� �ʴ´�.
    IEnumerator E_DiceAnimation(int diceValue)
    {
        OnAnimationStarted?.Invoke();

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
        
        // ? : Null���� ������
        OnAnimationFinished?.Invoke(diceValue);
    }
}
