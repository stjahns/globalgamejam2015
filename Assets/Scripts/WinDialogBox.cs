using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class WinDialogBox : DialogBox
{

    [InputSocket]
    override public void Show()
    {
        if (BodyController.Dismembered)
        {
            base.Responses.Add("How'd you get your arms or whatever back on?");
        }

        base.Show();
    }
}
