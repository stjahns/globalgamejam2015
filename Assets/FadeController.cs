using UnityEngine;
using System.Collections;
using System;

public class FadeController : StateMachineBase {

    public enum State
    {
        Blackout,
        Clear,
        FadingToBlack,
        FadingFromBlack
    }

    public static event Action OnFadeToBlack;
    public static event Action OnFadeFromBlack;

    public float FadeTime = 1.0f;

    private float fadeTimer;

    public static void AllFadeFromBlack() {
        if (OnFadeFromBlack != null) {
            OnFadeFromBlack();
        }
    }

    public static void AllFadeToBlack() {
        if (OnFadeToBlack != null) {
            OnFadeToBlack();
        }
    }

    public State initialState = State.Clear;

    void Start () {

        currentState = initialState;

        renderer.sortingLayerName = "Blackout";

        OnFadeFromBlack += () => {
            currentState = State.FadingFromBlack;
        };

        OnFadeToBlack += () => {
            currentState = State.FadingToBlack;
        };
    }

    [InputSocket]
    public void FadeToBlack() {
        currentState = State.FadingToBlack;
    }

    [InputSocket]
    public void FadeFromBlack() {
        currentState = State.FadingFromBlack;
    }

    void FadingFromBlack_Update () {
        fadeTimer += Time.deltaTime;

        float t = fadeTimer / FadeTime;

        renderer.material.color = Color.Lerp(Color.black, Color.clear, t);

        if (fadeTimer > FadeTime) {
            currentState = State.Clear;
        }
    }

    void FadingToBlack_Update () {
        fadeTimer += Time.deltaTime;

        float t = fadeTimer / FadeTime;

        renderer.material.color = Color.Lerp(Color.clear, Color.black, t);

        if (fadeTimer > FadeTime) {
            currentState = State.Blackout;
        }
    }

    IEnumerator FadingToBlack_EnterState() {
        print("FADING TO BLACK");
        renderer.material.color = Color.clear;
        fadeTimer = 0;
        yield return 0;
    }

    IEnumerator FadingFromBlack_EnterState() {
        print("FADING FROM BLACK");
        renderer.material.color = Color.black;
        fadeTimer = 0;
        yield return 0;
    }

    IEnumerator Blackout_EnterState() {
        print("BLACKOUT");
        renderer.material.color = Color.black;
        yield return 0;
    }

    IEnumerator Clear_EnterState() {
        print("CLEAR");
        renderer.material.color = Color.clear;
        yield return 0;
    }
}
