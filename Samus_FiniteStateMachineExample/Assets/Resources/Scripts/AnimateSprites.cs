using UnityEngine;
using System.Collections;


/*      =========================================================
 *      Takes an array of textures, creates a texture atlas, and 
 *      animates the textures in the atlas.
 */

public class AnimateSprites : MonoBehaviour {

    public Texture2D[] textures;
    public Texture2D atlas;
    public float framerate = 15.0f;

    private int currentFrame = 0;

    private float nextFrameTime = 0;
    private float timeBetweenFrames;
    private int atlasWidth; // Width of the texture atlas.
    

	void Start () {
        atlasWidth = textures[0].width * textures.Length;

        // Make texture atlas.
        atlas = new Texture2D(atlasWidth, textures[0].height);

        renderer.material.mainTexture = atlas;
        renderer.material.color = Color.white;
        renderer.material.shader = Shader.Find("Sprite");
        
        // Takes every texture and adds it to the atlas in order.
        for (int t = 0; t < textures.Length; t++) {
            for (int y = 0; y < textures[t].height; y++) {
                for (int x = 0; x < textures[t].width; x++) {
                    // The atlas is like film, where the next frame is directly to the right of the current frame.
                    atlas.SetPixel ( x + t*textures[t].width, y, textures[t].GetPixel(x,y) );
                }
            }
        }

        // Actually writes the textures to the atlas itself;
        atlas.Apply();
        renderer.material.mainTextureOffset = Vector2.zero;
        renderer.material.mainTextureScale = new Vector2(1.0f / textures.Length, 1.0f);
    
    } // End method Start()



    void LateUpdate() {

        timeBetweenFrames = 1.0f / framerate;

	    if (Time.time > nextFrameTime) {
            
            renderer.material.mainTextureOffset = new Vector2( (float) (currentFrame * textures[0].width) / atlasWidth, 0f);

            nextFrameTime = Time.time + timeBetweenFrames;
            currentFrame++;

            if (currentFrame >= textures.Length) {
                currentFrame = 0;
            }
        }
	} // End method LateUpdate()


}
