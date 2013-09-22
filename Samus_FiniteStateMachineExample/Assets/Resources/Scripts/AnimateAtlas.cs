using UnityEngine;
using System.Collections;


/*      =====================================================================
 *      Takes a one-row texture atlas with textures of uniform size and animates them.
 *      Expects the atlas dimensions and the frame width to be powers of 2.
 */

public class AnimateAtlas : MonoBehaviour
{

    public Texture2D atlas;

    public int initialFrame = 0;
    public int framesToDraw = 1;

    public float offset = 0f;
    public float framerate = 15.0f;


    private int currentFrame;
    private int frameWidth;

    private float nextFrameTime = 0;
    private float timeBetweenFrames;


    void Start()
    {
        currentFrame = initialFrame;
        frameWidth = atlas.height;

        renderer.material.mainTexture = atlas;
        renderer.material.shader = Shader.Find("Unlit/Transparent");

        renderer.material.mainTextureOffset = Vector2.zero;
        renderer.material.mainTextureScale = new Vector2( ((float)frameWidth) / atlas.width, 1.0f);
    } // End method Start()


    
    void LateUpdate()
    {
        
        timeBetweenFrames = 1.0f / framerate;

        if (Time.time > nextFrameTime)
        {

            renderer.material.mainTextureOffset = new Vector2((float)(currentFrame * frameWidth + offset)/atlas.width, 0f);

            nextFrameTime = Time.time + timeBetweenFrames;
            currentFrame++;

            if (currentFrame >= initialFrame + framesToDraw)
            {
                currentFrame = initialFrame;
            }
        }
    } // End method LateUpdate()

    
}
