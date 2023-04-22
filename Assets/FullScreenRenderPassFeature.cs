using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class FullScreenRenderPassFeature : ScriptableRendererFeature
{
    [SerializeField]
    private Material material;

    class CustomRenderPass : ScriptableRenderPass
    {
        [SerializeField]
        private Material material;
        RTHandle tempTexture, sourceTexture;

        public CustomRenderPass(Material material) : base()
        {
            this.material = material;
        }

        // This method is called before executing the render pass.
        // It can be used to configure render targets and their clear state. Also to create temporary render target textures.
        // When empty this render pass will render to the active camera render target.
        // You should never call CommandBuffer.SetRenderTarget. Instead call <c>ConfigureTarget</c> and <c>ConfigureClear</c>.
        // The render pipeline will ensure target setup and clearing happens in a performant manner.
        public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
        {
            sourceTexture = renderingData.cameraData.renderer.cameraColorTargetHandle;

            tempTexture =
                RTHandles.Alloc(
                    new RenderTargetIdentifier("_TempTexture"),
                    name: "_TempTexture");
        }

        // Here you can implement the rendering logic.
        // Use <c>ScriptableRenderContext</c> to issue drawing commands or execute command buffers
        // https://docs.unity3d.com/ScriptReference/Rendering.ScriptableRenderContext.html
        // You don't have to call ScriptableRenderContext.submit, the render pipeline will call it at specific points in the pipeline.
        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            CommandBuffer commandBUffer = CommandBufferPool.Get("Full Screen Render Feature");

            RenderTextureDescriptor targetDescriptor =
                renderingData.cameraData.cameraTargetDescriptor;

                targetDescriptor.depthBufferBits = 0;

            commandBUffer.GetTemporaryRT(
                Shader.PropertyToID(tempTexture.name), targetDescriptor, FilterMode.Bilinear);


            //copies data from source texture, but applies material
            //optimizes memor usage
            Blit(commandBUffer, sourceTexture, tempTexture, material);
            Blit(commandBUffer, tempTexture, sourceTexture);

            context.ExecuteCommandBuffer(commandBUffer);
            CommandBufferPool.Release(commandBUffer);

        }

        // Cleanup any allocated resources that were created during the execution of this render pass.
        public override void OnCameraCleanup(CommandBuffer cmd)
        {
            tempTexture.Release(); //releases memory from tempTexture
        }
    }

    CustomRenderPass m_ScriptablePass;

    /// <inheritdoc/>
    public override void Create()
    {
        m_ScriptablePass = new CustomRenderPass(this.material);

        // Configures where the render pass should be injected.
        m_ScriptablePass.renderPassEvent = RenderPassEvent.BeforeRenderingPostProcessing;
    }

    // Here you can inject one or multiple render passes in the renderer.
    // This method is called when setting up the renderer once per-camera.
    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        renderer.EnqueuePass(m_ScriptablePass);
    }
}


