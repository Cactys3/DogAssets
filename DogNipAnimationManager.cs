using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogNipAnimationManager : MonoBehaviour
{
    [SerializeField] private Animator Anim;

    // Update is called once per frame
    void Update()
    {
        if (Anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.2)
        {
            FindObjectOfType<ChangeToScene>().ChangeScene();
        }
    }
}
