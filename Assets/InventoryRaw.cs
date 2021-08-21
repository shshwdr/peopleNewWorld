using Pool;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryRaw : MonoBehaviour, IPointerEnterHandler
     , IPointerExitHandler
{
    public InventoryItem item;
    public Image image;
    public TMP_Text staticLabel;
    public TMP_Text valueLabel;
    // Start is called before the first frame update
    void Start()
    {
        image .sprite = Inventory.Instance.itemSprites[(int)(item)];
        updateInventory();
        if (Inventory.Instance.getItemAmount(item) == 0)
        {
            gameObject.SetActive(false);
        }
        EventPool.OptIn("updateInventory", updateInventory);
    }

    void updateInventory()
    {
        int value = Inventory.Instance.getItemAmount(item);
        if(value.ToString()!= valueLabel.text)
        {
            valueLabel.transform.DOKill();
            valueLabel.transform.localScale = Vector3.one;
            valueLabel.transform.DOPunchScale(Vector3.one, 0.5f).SetUpdate(true);
        }
        valueLabel.text = value.ToString();
        if (value != 0 && !gameObject.active)
        {
            gameObject.SetActive(true);
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        HUDManager.Instance.showExplain(0, (int)item);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        HUDManager.Instance.hideExplain();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
