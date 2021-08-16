using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSprite : MonoBehaviour
{

    public Sprite[] sprites;
    // Start is called before the first frame update
    void Start()
    {
        var renderer = GetComponent<SpriteRenderer>();
        renderer.sprite = sprites[Random.Range(0,sprites.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
