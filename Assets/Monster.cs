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
    public float avoidRate
    {
        get
        {
            return info.basicAvoidRate;
        }
    }
    public float hitRate
    {
        get
        {
            return info.basicHitRate;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void die()
    {
        base.die();

        StartCoroutine(delayDie());
    }

    IEnumerator delayDie()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
