using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SuccessUI : MonoBehaviour
{
    [SerializeField] private List<SuccessStar> _successStars;
    [SerializeField] private float _starShowingPeriod;
    [SerializeField] private Button _lobby;
    [SerializeField] private Button _replay;
    [SerializeField] private Button _next;

    private void OnEnable()
    {
        _lobby.onClick.AddListener(() => GameManager.Instance.GoToLobby());
        _replay.onClick.AddListener(() => GameManager.Instance.SelectLevel(GameManager.Instance.Data));
        if (LevelDataAssets.Instance.TryGetLevelData(GameManager.Instance.Data.Level + 1, out LevelData nextLevelData))
        {
            _next.onClick.AddListener(() => GameManager.Instance.SelectLevel(nextLevelData));
        }
        else
        {
            _next.gameObject.SetActive(false);
        }

        StartCoroutine(E_ShowStars());
    }

    IEnumerator E_ShowStars()
    {
        int count = 0;
        float lifeRatio = (float)Player.Instance.Life / GameManager.Instance.Data.LifeInit;
        while (count < Mathf.FloorToInt(_successStars.Count * lifeRatio))
        {
            _successStars[count].Show();
            count++;
            yield return new WaitForSeconds(_starShowingPeriod);
        }
    }
}
