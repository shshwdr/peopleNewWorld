using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PopupUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.DOJump(transform.position, 1, 1, 1f);
        Destroy(gameObject, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
