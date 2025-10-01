using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class HatchingRenderFeature : ScriptableRendererFeature
{
    //initialzing the render feature settings
    [System.Serializable]
    public class Settings
    {
        public RenderPassEvent renderPassEvent = RenderPassEvent.AfterRenderingSkybox;
        //the material that contains the hatching effect's shader, user must put in manually for now
        public Material material;
    }
    public Settings settings = new Settings();

    HatchingPass m_HatchingPass;

    //sets the hatching's render pass up
    public override void Create()
    {
        this.name = "Hatching Pass";
        if (settings.material == null)
        {
            Debug.LogWarning("No Hatching Material, Please input a material that has the hatching shader into the hatching effect's render feature setting");
            return;
        }
        m_HatchingPass = new HatchingPass(settings.renderPassEvent, settings.material);
    }
    
    //call and adds the hatching render pass to the scriptable renderer's queue
    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        renderer.EnqueuePass(m_HatchingPass);
    }

    //setups the pass with the correct camera target at that period in the render pipline
    //this is only exclusive for this effect, due to how its code/logic works
    public override void SetupRenderPasses(ScriptableRenderer renderer, in RenderingData renderingData)
    {
        //old code
        //m_HatchingPass.Setup(renderer);
    }
}
