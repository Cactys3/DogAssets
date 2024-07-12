using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAIScript : MonoBehaviour
{
    [SerializeField] private BossFightManager manager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        manager.BossHit(collision.tag.ToString());
    }
}
