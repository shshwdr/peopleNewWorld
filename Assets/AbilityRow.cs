using Pool;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class AbilityRow : MonoBehaviour, IPointerEnterHandler
     , IPointerExitHandler
{
    public CharacterAbility item;
    public TMP_Text staticLabel;
    public TMP_Text valueLabel;
    Character character;
    // Start is called before the first frame update
    void Start()
    {
        character = GetComponentInParent<Character>();
        staticLabel.text = Character.abilityNameMap[item];
        updateCityResource();
        //if (Inventory.Instance.getItemAmount(item) == 0)
        //{
        //    gameObject.SetActive(false);
        //}
        //EventPool.OptIn("updateCityResource", updateCityResource);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        HUDManager.Instance.showExplain(3, (int)item);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        HUDManager.Instance.hideExplain();
    }
    public void updateCityResource()
    {
        if (character == null)
        {
            character = GetComponentInParent<Character>();
        }
        if (item == null)
        {
            item = item;
        }
        int value = character.getAbility(item);
        valueLabel.text = value.ToString();
        //if (value != 0 /*&& !gameObject.active*/)
        //{
        //    gameObject.SetActive(true);
        //}
    }

    // Update is called once per frame
    void Update()
    {

    }
}
