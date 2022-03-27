using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;
#if UNITY_EDITOR
using System.ComponentModel;
#endif

[TrackClipType(typeof(CustomClip))]
[TrackBindingType(typeof(Renderer))]
[TrackColor(0.8f, 0.2f, 0.2f)]
#if UNITY_EDITOR
[DisplayName("Color Gradation Track")]
#endif
public class CustomTrack : TrackAsset
{
    public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
    {
        foreach (var clip in GetClips())
        {
            //clip.displayName = " ";
        }
        return ScriptPlayable<CustomMixer>.Create(graph, inputCount);
    }
}