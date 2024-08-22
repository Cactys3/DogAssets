using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogNipAnimationManager : MonoBehaviour
{
    [SerializeField] private Animator Anim;
    private bool done = false;
    // Update is called once per frame
    void Update()
    {
        if (Anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && (Time.timeSinceLevelLoad > FindObjectOfType<AudioManager>().GetLengthMusic(MusicTracker.DognipMusic) + 1))
        {
            if (!done)
            {
                done = true;
                StartCoroutine("NextScene");
            }
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

    private IEnumerator NextScene()
    {
        yield return new WaitUntil(() => (FindObjectOfType<AudioManager>().GetLengthMusic(MusicTracker.DognipMusic) < Time.timeSinceLevelLoad));//wait for music to end
        FindObjectOfType<AudioManager>().PlaySFX("cool_1"); //play cool sound
        yield return new WaitForSeconds(FindObjectOfType<AudioManager>().GetLengthSFX("cool_1")); //wait for cool sound to end
        FindObjectOfType<MusicTracker>().SetMusic(MusicTracker.PostDognipMusic);
        FindObjectOfType<ChangeToScene>().ChangeScene();
    }
}
