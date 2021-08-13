using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : HPObject
{
    public MonsterInfo info;
    public void Init(int mi)
    {
        info = MonsterManager.Instance.getMonsterInfo(mi);
        maxHP = info.maxHP;
        base.Init();
    }

    public override int attack { get { return info.attack; } }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void die()
    {
        base.die();
        gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
