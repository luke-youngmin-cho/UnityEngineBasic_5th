using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SuccessUI : MonoBehaviour
{
    [SerializeField] private List<SuccessStar> _successStars;
    [SerializeField] private float _starShowingPeriod;

    private void OnEnable()
    {
        StartCoroutine(E_ShowStars());
    }

    IEnumerator E_ShowStars()
    {
        int count = 0;
        float lifeRatio = (float)Player.Instance.Life / GameManager.Instance.Data.LifeInit;
        while (count < _successStars.Count * lifeRatio)
        {
            _successStars[count].Show();
            count++;
            yield return new WaitForSeconds(_starShowingPeriod);
        }
    }
}
