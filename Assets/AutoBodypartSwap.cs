using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoBodypartSwap : MonoBehaviour
{
    SpriteRenderer renderer;
    public List<Sprite> options = new List<Sprite>();
    // Start is called before the first frame update
    void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
    }
    
    public void generate(int i )
    {
        renderer.sprite = options[i];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
