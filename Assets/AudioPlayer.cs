using UnityEngine;
using System.Collections;

public class AudioPlayer : TriggerBase {

    public AudioClip clip;

    [InputSocket]
    public void PlayClip()
    {
        AudioSource.PlayClipAtPoint(clip, transform.position);
    }
}
