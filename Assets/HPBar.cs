using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

public class HPBar : MonoBehaviour, IPointerEnterHandler
     , IPointerExitHandler
{
    public CharacterStatus status;
    public Image image;
    float maxValue;
    float currentValue;
    private void Awake()
    {
       // image = GetComponentInChildren<Image>();
    }
    public void setMaxValue(int v)
    {
        maxValue = v;
        currentValue = v;
        image.fillAmount = 1;
    }
    public void updateCurrentValue(int v)
    {
        if (currentValue != v)
        {

            image.transform.DOPunchScale(Vector3.one, 1, 10, 0.5f);
        }
        currentValue = v;
        DOTween.To(() => image.fillAmount, x => image.fillAmount = x, currentValue / maxValue, 1);


        //image.fillAmount = currentValue / maxValue;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        HUDManager.Instance.showExplain(4, (int)status);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        HUDManager.Instance.hideExplain();
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
