using UnityEngine;
using System.Collections;

public class BoundingBox : MonoBehaviour {

    public string ToolTip;
    public Vector2 TooltipOffset = Vector2.zero;

    public bool Showing = false;

    void OnGUI()
    {
        if (Showing)
        {
            var screenPos = Camera.main.WorldToScreenPoint(transform.position).XY() + TooltipOffset;
            GUILayout.BeginArea(new Rect(screenPos.x,
                                         Screen.height - screenPos.y,
                                         100,
                                         100));
            GUILayout.Label(ToolTip);
            GUILayout.EndArea();
        }
    }

    public void Show()
    {
        renderer.enabled = true;
        Showing = true;
    }

    public void Hide()
    {
        renderer.enabled = false;
        Showing = false;
    }

    public Transform Target;

    void Start()
    {
        Target = transform.parent;
        transform.parent = null;
    }

    void Update()
    {
        transform.position = Target.position;
    }

}
