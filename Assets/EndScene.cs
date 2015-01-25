using UnityEngine;
using System.Collections;

public class EndScene : TriggerBase {


    public UnityEngine.UI.Image image;

    [InputSocket]
    public void Show()
    {
        image.enabled = true;
    }

}
