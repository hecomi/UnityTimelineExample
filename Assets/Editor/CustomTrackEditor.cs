using UnityEngine;
using UnityEngine.Timeline;
using UnityEditor.Timeline;

[CustomTimelineEditor(typeof(CustomTrack))]
public class CustomTrackEditor : TrackEditor
{
    Texture2D _iconTexture;

    public override TrackDrawOptions GetTrackOptions(TrackAsset track, Object binding)
    {
        track.name = "CustomTrack";

        if (!_iconTexture)
        {
            _iconTexture = Resources.Load<Texture2D>("CustomTrack-Icon");
        }

        var options = base.GetTrackOptions(track, binding);
        options.trackColor = Color.magenta;
        options.icon = _iconTexture;
        return options;
    }
}