using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGroup : MonoBehaviour
{
    Vector3 originalPosition;
    public Transform parentposition;
    public Transform monsterParent;
    HuntTurnView view;
    //Transform[] childPosiitons;
    public bool isSelected;
    public List<Monster> monsters;
    public void Init(int mi ,int count,HuntTurnView v)
    {
        monsters = new List<Monster>();
        isSelected = false;
        view = v;
        for(int i = 0; i < count; i++)
        {
            GameObject monsterPrefab = Resources.Load<GameObject>("monsters/" + MonsterManager.Instance.getMonsterInfo(mi).name);
            var monster = Instantiate(monsterPrefab, parentposition.GetChild(i).position + new Vector3(0,0,1), Quaternion.identity,monsterParent);
            monster.GetComponent<Monster>().Init(mi);
            monsters.Add(monster.GetComponent<Monster>());
        }
    }

    public void Clear()
    {
        isSelected = true;
        Utils.destroyAllChildren(monsterParent);
        transform.position = originalPosition;
        transform.localScale = Vector3.one;
    }

    private void OnMouseDown()
    {
        if (ControlManager.Instance.shouldBlockMouse()) return;
        if (isSelected)
        {
            return;
        }
        view.selectMonsterGroup(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
