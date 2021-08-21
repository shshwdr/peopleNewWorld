using DG.Tweening;
using Pool;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MonsterRow : MonoBehaviour, IPointerEnterHandler
     , IPointerExitHandler
{
    public int monster;
    public Image image;
    public TMP_Text staticLabel;
    public TMP_Text valueLabel;

    // Start is called before the first frame update
    void Start()
    {
        CityManager.Instance.monsterRow.Add(this);
        image.sprite = MonsterManager.Instance.getMonsterInfo(monster).image;
        //staticLabel.text = Inventory.Instance.inventoryNameMap[item];
        updateCityMonster();
        //if (Inventory.Instance.getItemAmount(item) == 0)
        //{
        //    gameObject.SetActive(false);
        //}
        EventPool.OptIn("updateCityMonster", updateCityMonster);
    }

    void updateCityMonster()
    {
        int value = monster< CityManager.Instance.currentCityInfo().monsters.Length?  CityManager.Instance.currentCityInfo().monsters[monster]:0;

        if (value.ToString() != valueLabel.text)
        {
            valueLabel.transform.DOKill();
            valueLabel.transform.localScale = Vector3.one;
            valueLabel.transform.DOPunchScale(Vector3.one, 0.5f).SetUpdate(true);
        }
        if (value > 0)
        {
            gameObject.SetActive(true);
        }
        else
        {

            gameObject.SetActive(false);
        }
        valueLabel.text = value.ToString();
        //if (value != 0 /*&& !gameObject.active*/)
        //{
        //    gameObject.SetActive(true);
        //}
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        HUDManager.Instance.showExplain(2, monster);
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
