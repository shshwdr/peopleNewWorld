using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    Image image;
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
        image.fillAmount = currentValue / maxValue;
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
