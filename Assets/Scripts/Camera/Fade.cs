using UnityEngine;

public class Fade : MonoBehaviour {
    [HideInInspector]
    public bool fadeOutCompleted;

    public void FadeOut()
    {
        this.GetComponent<Animator>().SetTrigger("Fadeout");
    }

    public void OnFadeOutComplete()
    {
        fadeOutCompleted = true;
    }
}
