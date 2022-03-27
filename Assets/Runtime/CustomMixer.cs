using UnityEngine;
using UnityEngine.Playables;

public class CustomMixer : PlayableBehaviour
{
    Renderer _renderer = null;
    Material _origMat = null;
    Material _mat = null;

    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        if (_mat) Object.DestroyImmediate(_mat);
        if (_renderer) _renderer.material = _origMat;
    }

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        var renderer = playerData as Renderer;
        if (!renderer) return;

        if (!_mat) 
        {
            _renderer = renderer;
            _origMat = renderer.sharedMaterial;
            _mat = new Material(_origMat);
            renderer.material = _mat;
        }

        int n = playable.GetInputCount();
        if (n == 0) return;

        var color = Color.clear;
        for (int i = 0; i < n; i++)
        {
            var sp = (ScriptPlayable<CustomBehaviour>)playable.GetInput(i);
            var behaviour = sp.GetBehaviour();
            var weight = playable.GetInputWeight(i);
            color += behaviour.outputColor * weight;
        }

        _mat.color = color;
    }
}