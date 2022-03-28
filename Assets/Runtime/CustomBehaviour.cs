using UnityEngine;
using UnityEngine.Playables;

[System.Serializable]
public class CustomBehaviour : PlayableBehaviour
{
    public CustomClip clip { get; set; }
    public Color outputColor { get; private set; }

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        var t = playable.GetTime();
        var d = clip.durationInTrack;
        var a = (float)(t / d);
        outputColor = clip.gradient.Evaluate(a);
        Debug.Log(playable.GetLeadTime());
    }
}