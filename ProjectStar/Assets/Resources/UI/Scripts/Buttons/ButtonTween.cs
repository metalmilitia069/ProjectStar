using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonTween : MonoBehaviour
{
    [Range(0, 1)]
    public float EmphasiseSpeed;
    [Range(1, 2)]
    public float EmphasisAmount;
    public LeanTweenType TweenType;

    private LTDescr activeTween;

    private bool isTweening;

    public void Emphasise()
    {
        if (!isTweening)
        {
            isTweening = true;
            activeTween = transform.LeanScale(Vector3.one * EmphasisAmount, EmphasiseSpeed).setEase(TweenType).setOnComplete(() => isTweening = false);
            LeanTween.color(transform.GetComponent<RectTransform>(), Color.red, EmphasiseSpeed); //color(gameObject, Color.white, 2.0f);            
        }
        else
        {
            activeTween.setOnComplete(() => activeTween = transform.LeanScale(Vector3.one * EmphasisAmount, EmphasiseSpeed).setEase(TweenType).setOnComplete(() => isTweening = false));
            LeanTween.color(transform.GetComponent<RectTransform>(), Color.red, EmphasiseSpeed); //color(gameObject, Color.white, 2.0f);
        }
    }

    public void DeEmphasise()
    {
        if (!isTweening)
        {
            isTweening = true;
            activeTween = transform.LeanScale(Vector3.one, EmphasiseSpeed).setEase(TweenType).setOnComplete(() => isTweening = false);
            LeanTween.color(transform.GetComponent<RectTransform>(), Color.white, EmphasiseSpeed);
        }
        else
        {
            activeTween.setOnComplete(() => activeTween = transform.LeanScale(Vector3.one, EmphasiseSpeed).setEase(TweenType).setOnComplete(() => isTweening = false));
            LeanTween.color(transform.GetComponent<RectTransform>(), Color.white, EmphasiseSpeed);
        }
    }

    public void ButtonDown()
    {
        Color color = new Color(.7f, .7f, .7f, 1f);
        LeanTween.color(transform.GetComponent<RectTransform>(), color, EmphasiseSpeed);
    }

    public void ButtonUp()
    {
        LeanTween.color(transform.GetComponent<RectTransform>(), Color.white, EmphasiseSpeed);
    }
}
