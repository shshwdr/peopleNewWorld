using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HPBar : MonoBehaviour
{
    public Image image;
    float maxValue;
    float currentValue;
    private void Awake()
    {
        image = GetComponentInChildren<Image>();
    }
    public void setMaxValue(int v)
    {
        maxValue = v;
        currentValue = v;
        image.fillAmount = 1;
    }
    public void updateCurrentValue(int v)
    {
        currentValue = v;

        DOTween.To(() => image.fillAmount, x => image.fillAmount = x, currentValue / maxValue, 1);


        //image.fillAmount = currentValue / maxValue;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
