using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.RenderGraphModule;
using UnityEngine.Rendering.RenderGraphModule.Util;
using UnityEngine.Rendering.Universal;

//some of the rendering/blit code in Render() is based on IronStar's render feature code in https://github.com/BattleDawnNZ/Image-Effects-with-Shadergraph/blob/master/Assets/Renderer%20Feature/BlitPass.cs
//but the rest is original as most of the code in IronStar's render feature code is unecessary and can be simplified down to this instead.
public class HatchingPass : ScriptableRenderPass
{
    static readonly string renderPassTag = "Hatching";

    //volume for the hatching effect
    private HatchingVolume hatchingVolume;

    //material containing the shader
    private Material HatchingMaterial;

    //RTHandles
    //RTHandle source;

    //initializes our variables
    public HatchingPass(RenderPassEvent evt, Material mat)
    {
        renderPassEvent = evt;
        if (mat == null)
        {
            Debug.LogError("No Hatching Material, Please input a material that has the hatching shader into the hatching effect's render feature setting");
            return;
        }
        //to make profiling easier
        profilingSampler = new ProfilingSampler(renderPassTag);
        HatchingMaterial = mat;
    }

    //where our rendering of the effect starts
    public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
    {
        if (HatchingMaterial == null)
        {
            Debug.LogError("No Hatching Material, Please input a material that has the hatching shader into the hatching effect's render feature setting");
            return;
        }

        //in case if the camera doesn't have the post process option enabled
        //didn't use a debug log cuz it also effects preview camera for any 3d objects/animation and when scene or game is loading.
        //hence, doing so will cause premature logging errors that may not actually be an error
        if (!renderingData.cameraData.postProcessEnabled)
        {
            //Debug.LogError("Post Processing in Camera not enabled");
            return;
        }
        VolumeStack stack = VolumeManager.instance.stack;
        hatchingVolume = stack.GetComponent<HatchingVolume>();

        var cmd = CommandBufferPool.Get(renderPassTag);
        Render(cmd, ref renderingData);

        context.ExecuteCommandBuffer(cmd);
        cmd.Clear();

        CommandBufferPool.Release(cmd);
    }

    //helper method to contain all of our rendering code for the Execute() method
    void Render(CommandBuffer cmd, ref RenderingData renderingData)
    {
        if (hatchingVolume.IsActive() == false) return;
        hatchingVolume.load(HatchingMaterial, ref renderingData);

        //for profiling
        using (new ProfilingScope(cmd, profilingSampler))
        {
            //actual rendering code
            var src = renderingData.cameraData.renderer.cameraColorTargetHandle;

            int width = renderingData.cameraData.cameraTargetDescriptor.width;
            int height = renderingData.cameraData.cameraTargetDescriptor.height;

            var tempColorTexture = RenderTexture.GetTemporary(width, height, 0, RenderTextureFormat.ARGB32);

            //old code uses source variable instead of src
            cmd.Blit(src, tempColorTexture, HatchingMaterial, 0);
            cmd.Blit(tempColorTexture, src);

            RenderTexture.ReleaseTemporary(tempColorTexture);
        }
    }

    //sets up the camera color targets to our scripts's private variables of the camera targets
    public void Setup(ScriptableRenderer renderer)
    {
        //old code
        //source = renderer.cameraColorTargetHandle; //source
    }
}
