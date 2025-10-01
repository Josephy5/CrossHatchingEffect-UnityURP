using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEditor;

[Serializable, VolumeComponentMenu("Cross Hatching")]
public class HatchingVolume : VolumeComponent, IPostProcessComponent
{
    public bool _UseVolumeComponent = false;
    public Texture2DArrayParameter _HatchTextures = new Texture2DArrayParameter(null, true);
    public Vector2Parameter _HatchTextureTiling = new Vector2Parameter(new Vector2(8, 8), true);
    public ClampedFloatParameter _HatchTextureCount = new ClampedFloatParameter(18f, 0f, 50f, true);
    public ClampedFloatParameter _ColorIntensity = new ClampedFloatParameter(1f, 0f, 1f, true);
    public BoolParameter _ExtraBrightness = new BoolParameter(false, true);
    public ClampedFloatParameter _ExtraBrightnessStrength = new ClampedFloatParameter(0.25f, 0f, 2f, true);
    public ClampedFloatParameter _FresnelPower = new ClampedFloatParameter(1f, 0f, 50f, true);
    public ClampedFloatParameter _ExtraFresnelTuning = new ClampedFloatParameter(1f, 0f, 50f, true);
    public BoolParameter _InvertHatchColour = new BoolParameter(false, true);
    public FloatParameter _InvertStrength = new FloatParameter(3.1f, true);
    public ClampedFloatParameter _AnimationCyclesPerSecond = new ClampedFloatParameter(12f, 0f, 30f, true);

    public void load(Material material, ref RenderingData renderingData)
    {
        if (_UseVolumeComponent) return;

        material.SetVector("_Hatch_Texture_Tiling", _HatchTextureTiling.value);
        material.SetFloat("_Hatch_Texture_Count", _HatchTextureCount.value);
        material.SetFloat("_Color_Intensity", _ColorIntensity.value);
        material.SetFloat("_Extra_Brightness_Strength", _ExtraBrightnessStrength.value);
        material.SetFloat("_Fresnel_Power", _FresnelPower.value);
        material.SetFloat("_Extra_Fresnel_Tuning", _ExtraFresnelTuning.value);
        material.SetFloat("_Invert_Strength", _InvertStrength.value);
        material.SetFloat("_Animation_Cycles_Per_Second", _AnimationCyclesPerSecond.value);

        //if you already have the hatching textures in place at the material or using the material
        //to insert the hatching textures
        var hold = material.GetTexture("_Hatch_Textures");
        if (hold != null && _HatchTextures.value == null) {
            _HatchTextures.value = (Texture2DArray) hold;
        }

        material.SetTexture("_Hatch_Textures", _HatchTextures.value);
        material.SetInt("_Extra_Brightness", _ExtraBrightness.value ? 1 : 0);
        material.SetInt("_Invert_Hatch_Colour", _InvertHatchColour.value ? 1 : 0);
    }

    public bool IsActive() => true;
    public bool IsTileCompatible() => false;
}

[System.Serializable]
public class Texture2DArrayParameter : VolumeParameter<Texture2DArray>
{
    public Texture2DArrayParameter(Texture2DArray value, bool overrideState = false) : base(value, overrideState) { }
}