using UnityEngine;
using System.Collections;

public class Callout : TriggerBase {

    public GUISkin skin;

    public string Text;
    public Texture CalloutPoint;

    public Vector2 offset;

    public bool Showing = false;
    public float ShowTime = 5f;

    private float showTimer;


    void OnGUI()
    {
        if (Showing)
        {
            GUI.skin = skin;
            GUI.depth = 0;

            // Get pos

            var screenPos = Camera.main.WorldToScreenPoint(transform.position).XY() - offset;

            float width = 200f;
            float height = 50f;

            GUILayout.BeginArea(new Rect(screenPos.x, //screenPos.x - width / 2f,
                                         Screen.height - screenPos.y,
                                         width,
                                         height));

            GUILayout.BeginVertical("CalloutContainer");
            GUILayout.Label(Text);
            GUILayout.Label(CalloutPoint, "CalloutPoint");
            GUILayout.EndVertical();
            GUILayout.EndArea();
        }
    }

    void Update () {
        if (Showing)
        {
            showTimer += Time.deltaTime;
            if (showTimer > ShowTime)
            {
                Showing = false;
            }
        }
    }

    [InputSocket]
    public void Show()
    {
        showTimer = 0;
        Showing = true;
    }

}
