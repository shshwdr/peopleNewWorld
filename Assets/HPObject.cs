using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPObject : MonoBehaviour
{
    public int maxHP = 100;
    int hp;
    public bool isDead;
    public bool temporaryLeave;
    HPBar hpbar;

    public virtual int attack { get { return 1; } }
    public void Init()
    {

        hp = maxHP;
        hpbar.setMaxValue(maxHP);
    }
    protected virtual void Awake()
    {

        hpbar = GetComponentInChildren<HPBar>();
    }

    public virtual void doDamage(int damage)
    {
        hp -= damage;
        hpbar.updateCurrentValue(hp);
        if (hp <= 0)
        {
            die();
        }
    }

    public virtual void heal(int val)
    {

        hp += val;
        hpbar.updateCurrentValue(hp);
    }

    public virtual void die()
    {
        if (isDead)
        {
            return;
        }
        isDead = true;

    }
}
