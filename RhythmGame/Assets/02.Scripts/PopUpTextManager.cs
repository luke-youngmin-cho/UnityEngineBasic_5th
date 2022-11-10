using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpTextManager : MonoBehaviour
{
    public static PopUpTextManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] private PopUpText _bad;
    [SerializeField] private PopUpText _miss;
    [SerializeField] private PopUpText _good;
    [SerializeField] private PopUpText _great;
    [SerializeField] private PopUpText _cool;
    [SerializeField] private PopUpText _comboTitle;
    [SerializeField] private PopUpText _combo;

    public void PopUp(HitTypes hitType)
    {
        if (_bad.gameObject.activeSelf) _bad.transform.Translate(Vector3.forward);
        else _bad.ResetPos();
        if (_miss.gameObject.activeSelf) _miss.transform.Translate(Vector3.forward);
        else _miss.ResetPos();
        if (_good.gameObject.activeSelf) _good.transform.Translate(Vector3.forward);
        else _good.ResetPos();
        if (_great.gameObject.activeSelf) _great.transform.Translate(Vector3.forward);
        else _great.ResetPos();
        if (_cool.gameObject.activeSelf) _cool.transform.Translate(Vector3.forward);
        else _cool.ResetPos();


        switch (hitType)
        {
            case HitTypes.None:
                break;
            case HitTypes.Bad:
                {
                    _bad.PopUp();
                    _bad.transform.Translate(Vector3.back);  
                }
                break;
            case HitTypes.Miss:
                {
                    _miss.PopUp();
                    _miss.transform.Translate(Vector3.back);
                }
                break;
            case HitTypes.Good:
                {
                    _good.PopUp();
                    _good.transform.Translate(Vector3.back);
                }
                break;
            case HitTypes.Great:
                {
                    _great.PopUp();
                    _great.transform.Translate(Vector3.back);
                }
                break;
            case HitTypes.Cool:
                {
                    _cool.PopUp();
                    _cool.transform.Translate(Vector3.back);
                }
                break;
            default:
                break;
        }

        if (GameStatus.CurrentCombo > 1)
        {
            _comboTitle.PopUp();
            _combo.PopUp(GameStatus.CurrentCombo.ToString());
        }
    }
}
