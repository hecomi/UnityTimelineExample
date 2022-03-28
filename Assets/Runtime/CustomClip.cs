using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
#if UNITY_EDITOR
using System.ComponentModel;
#endif

#if UNITY_EDITOR
[DisplayName("Color Gradation Clip")]
#endif
public class CustomClip : PlayableAsset, ITimelineClipAsset
{
    public Gradient gradient;
    public float durationInTrack { get; set; }

    public ClipCaps clipCaps
    {
        get => 
            ClipCaps.Blending |
            ClipCaps.Extrapolation;
    }

    public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
    {
        var playable = ScriptPlayable<CustomBehaviour>.Create(graph);
        var behaviour = playable.GetBehaviour();
        behaviour.clip = this;
        return playable;
    }
}