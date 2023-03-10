using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using ULB.RPG;
using ULB.RPG.UISystems;
using UnityEngine;

public class CharacterInfoUI : MonoBehaviour, IUI
{
    [SerializeField] private TMP_Text _str;
    [SerializeField] private TMP_Text _dex;
    [SerializeField] private TMP_Text _int;
    [SerializeField] private TMP_Text _luk;
    [SerializeField] private CharacterBase _character;

    public Canvas canvas => _canvas;
    private Canvas _canvas;
    public event Action OnShow;
    public event Action OnHide;
    public void Hide()
    {
        gameObject.SetActive(false);
        OnHide?.Invoke();
    }

    public void Show()
    {
        gameObject.SetActive(true);
        OnShow?.Invoke();
    }

    private void Awake()
    {
        _canvas = GetComponent<Canvas>();
        CanvasManager.instance.Register(this);

        _character.stats[StatType.STR].onValueModifiedChanged += (valueModified) =>
        {
            int original = _character.stats[StatType.STR].value;
            _str.text = $"{original} <color=#84FD57>(+{valueModified})</color>";
        };
        _character.stats[StatType.DEX].onValueModifiedChanged += (valueModified) =>
        {
            int original = _character.stats[StatType.DEX].value;
            _dex.text = $"{original} <color=#84FD57>(+{valueModified})</color>";
        };
        _character.stats[StatType.INT].onValueModifiedChanged += (valueModified) =>
        {
            int original = _character.stats[StatType.INT].value;
            _int.text = $"{original} <color=#84FD57>(+{valueModified})</color>";
        };
        _character.stats[StatType.LUK].onValueModifiedChanged += (valueModified) =>
        {
            int original = _character.stats[StatType.LUK].value;
            _luk.text = $"{original} <color=#84FD57>(+{valueModified})</color>";
        };
        _str.text = $"{_character.stats[StatType.STR].value} (+<color=#84FD57>(+{_character.stats[StatType.STR].valueModified})</color>";
        _dex.text = $"{_character.stats[StatType.DEX].value} (+<color=#84FD57>(+{_character.stats[StatType.DEX].valueModified})</color>";
        _int.text = $"{_character.stats[StatType.INT].value} (+<color=#84FD57>(+{_character.stats[StatType.INT].valueModified})</color>";
        _luk.text = $"{_character.stats[StatType.LUK].value} (+<color=#84FD57>(+{_character.stats[StatType.LUK].valueModified})</color>";

    }
}
