using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteDestroyer : MonoBehaviour
{
    [SerializeField] private LayerMask _noteLayer;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (1<<collision.gameObject.layer == _noteLayer)
        {
            collision.gameObject.GetComponent<Note>().Hit(HitTypes.Bad);
        }
    }
}
