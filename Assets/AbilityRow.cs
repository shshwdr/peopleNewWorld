using Pool;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AbilityRow : MonoBehaviour
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

    public void updateCityResource()
    {
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
