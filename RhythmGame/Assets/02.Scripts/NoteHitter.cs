using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteHitter : MonoBehaviour
{
    private const float HIT_JUDGE_RANGE_COOL = 0.3f;
    private const float HIT_JUDGE_RANGE_GREAT = 0.5f;
    private const float HIT_JUDGE_RANGE_GOOD = 0.7f;
    private const float HIT_JUDGE_RANGE_MISS = 0.9f;
    private const float HIT_JUDGE_RANGE_SPEED_GAIN = 0.05f;
    public KeyCode Key;
    [SerializeField] private LayerMask _noteLayer;
    private SpriteRenderer _spriteRenderer;
    private Color _colorOrigin;
    [SerializeField] private Color _colorPressed;
    [SerializeField] private GameObject _spotlightEffect;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _colorOrigin = _spriteRenderer.color;
    }

    private void Update()
    {
        if (Input.GetKeyDown(Key))
        {
            SetColorPressed();
            _spotlightEffect.SetActive(true);
        }
        if (Input.GetKeyUp(Key))
        {
            SetColorOrigin();
            _spotlightEffect.SetActive(false);
        }
    }

    private void SetColorPressed()
    {
        _spriteRenderer.color = _colorPressed;
    }

    private void SetColorOrigin()
    {
        _spriteRenderer.color = _colorOrigin;
    }

    private void OnDrawGizmos()
    {
        if (NoteSpawnManager.Instance == null)
            return;

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position,
                            new Vector3(transform.lossyScale.x / 2.0f,
                                        HIT_JUDGE_RANGE_COOL + NoteSpawnManager.Instance.NoteSpeedScale * HIT_JUDGE_RANGE_SPEED_GAIN,
                                        0.0f));
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position,
                            new Vector3(transform.lossyScale.x / 2.0f,
                                        HIT_JUDGE_RANGE_GREAT + NoteSpawnManager.Instance.NoteSpeedScale * HIT_JUDGE_RANGE_SPEED_GAIN,
                                        0.0f));
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, 
                            new Vector3(transform.lossyScale.x / 2.0f,
                                        HIT_JUDGE_RANGE_GOOD + NoteSpawnManager.Instance.NoteSpeedScale * HIT_JUDGE_RANGE_SPEED_GAIN,
                                        0.0f));
        Gizmos.color = Color.grey;
        Gizmos.DrawWireCube(transform.position, 
                            new Vector3(transform.lossyScale.x / 2.0f,
                                        HIT_JUDGE_RANGE_MISS + NoteSpawnManager.Instance.NoteSpeedScale * HIT_JUDGE_RANGE_SPEED_GAIN,
                                        0.0f));
    }
}
