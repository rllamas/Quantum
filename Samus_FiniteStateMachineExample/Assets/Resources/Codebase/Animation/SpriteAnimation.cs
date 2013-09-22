/*  
 *  Sprite Animation Class
 * 
 *  Written By: Russell Jahn
 * 
 */

using GameFramework;
using UnityEngine;

namespace GameFramework.Animation {

	public class SpriteAnimation {

	    public SpriteAnimation(string name, Texture2D sprite, int startFrame, int endFrame, float fps, string shaderName) {
	        this.Name = name;
	        this.Spritesheet = sprite;
	        this.StartFrame = startFrame;
	        this.EndFrame = endFrame;
	        this.Fps = fps;
			this.ShaderName = shaderName;
		}
		
		public Texture2D Spritesheet {get; set;}
	    public string Name {get; set;}
	    public int StartFrame {get; set;}
	    public int EndFrame {get; set;}
	    public float Fps {get; set;}
		public string ShaderName {get; set;}
		
		public override string ToString()
		{
			return string.Format ("[SpriteAnimation: spritesheet={0}, name={1}, startFrame={2}, endFrame={3}, fps={4}, shaderName={5}]", Spritesheet, Name, StartFrame, EndFrame, Fps, ShaderName);
		}

	}
}