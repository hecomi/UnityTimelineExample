using UnityEngine;
using UnityEngine.Timeline;
using UnityEditor.Timeline;
using System.Collections.Generic;

[CustomTimelineEditor(typeof(CustomClip))]
public class CustomClipEditor : ClipEditor
{
    Dictionary<CustomClip, Texture2D> _textures = new Dictionary<CustomClip, Texture2D>();

    public override void DrawBackground(TimelineClip clip, ClipBackgroundRegion region)
    {
        var tex = GetGradientTexture(clip);
        if (tex) GUI.DrawTexture(region.position, tex);
    }

    public override ClipDrawOptions GetClipOptions(TimelineClip clip)
    {
        var options = base.GetClipOptions(clip);
        options.highlightColor = Color.clear;
        return options;
    }

    public override void OnCreate(TimelineClip clip, TrackAsset track, TimelineClip clonedFrom)
    {
        clip.displayName = " ";
    }

    public override void OnClipChanged(TimelineClip clip)
    {
        GetGradientTexture(clip, true);
    }

    Texture2D GetGradientTexture(TimelineClip clip, bool update = false)
    {
        Texture2D tex = Texture2D.whiteTexture;

        var customClip = clip.asset as CustomClip;
        if (!customClip) return tex;

        var gradient = customClip.gradient;
        if (gradient == null) return tex;

        if (update) 
        {
            _textures.Remove(customClip);
        }
        else
        {
            _textures.TryGetValue(customClip, out tex);
            if (tex) return tex;
        }

        var b = (float)(clip.blendInDuration / clip.duration);
        tex = new Texture2D(128, 1);
        for (int i = 0; i < tex.width; ++i)
        {
            var t = (float)i / tex.width;
            var color = customClip.gradient.Evaluate(t);
            if (b > 1f) color.a = Mathf.Min(t / b, 1f);
            tex.SetPixel(i, 0, color);
        }
        tex.Apply();
        _textures.Add(customClip, tex);

        return tex;
    }
}