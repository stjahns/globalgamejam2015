using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class WinTrigger : TriggerBase {

    public float triggerRadius = 2.0f;

    [OutputEventConnections]
    [HideInInspector]
    public List<SignalConnection> onWin = new List<SignalConnection>();

    private bool won = false;

    void Update () {

        if (won)
            return;

        var hits = Physics2D.CircleCastAll(transform.position, triggerRadius, Vector2.zero, Mathf.Infinity, LayerMask.GetMask("Body"));


        if (hits.Length == 6)
        {
            won = true;
            GameObject.FindObjectsOfType<DummyAI>().ToList().ForEach(a => a.stop());

            FadeController.AllFadeToBlack();

            onWin.ForEach(s => s.Fire());
        }
    }
}
