using System.Collections;
using System.Collections.Generic;
using TMPro;
using ULB.RPG;
using UnityEngine;
using static UnityEngine.UI.Image;

public class CharacterInfoUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _str;
    [SerializeField] private TMP_Text _dex;
    [SerializeField] private TMP_Text _int;
    [SerializeField] private TMP_Text _luk;
    [SerializeField] private CharacterBase _character;

    private void Awake()
    {
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
