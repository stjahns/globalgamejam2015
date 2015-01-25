using UnityEngine;
using System.Collections;

public class LevelRestart : TriggerBase {

    float delay = 1.0f;

    [InputSocket]
    public void Restart () {
        StartCoroutine(ReloadRoutine());
    }

    IEnumerator ReloadRoutine()
    {
        FadeController.AllFadeToBlack();
        DialogBox.currentDialog = null;
        yield return new WaitForSeconds(delay);
        Application.LoadLevel(Application.loadedLevel);
    }
}
