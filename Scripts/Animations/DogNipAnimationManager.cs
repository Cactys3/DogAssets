using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogNipAnimationManager : MonoBehaviour
{
    [SerializeField] private Animator Anim;

    // Update is called once per frame
    void Update()
    {
        if (Anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && (Time.timeSinceLevelLoad > FindObjectOfType<AudioManager>().GetLengthMusic(MusicTracker.DognipMusic) + 1))
        {
            FindObjectOfType<ChangeToScene>().ChangeScene();
        }
    }
    public void StartMusic()
    {
        FindObjectOfType<MusicTracker>().SetMusic(MusicTracker.DognipMusic);
    }
    public void PlayMunch()
    {
        FindObjectOfType<AudioManager>().PlayMultipleSFX("dognip_munch");
    }
}
