using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawnCanvas : MonoBehaviour
{
    public GameObject overlayText;
    public Animator animator;

    public void FadeIn()
    {
        overlayText.SetActive(true);
        animator.SetTrigger("FadeTrigger");
    }
    public void FadeOut()
    {
        overlayText.SetActive(false);
        animator.Play("WaveSpawnFadeOut");
    }
}
