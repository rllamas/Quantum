///*  
// *  Platformer Character Controller
// *  Written By Russell Jahn
// *  
// *  Mark 2: Rewriting Animation System, 
// *          Smooth switching between standing and walking
// *          
// *  Mark 3: Adding Jump Capabilities (Incomplete)
// *  
// *  Mark 4: Rewriting animation system to use an Xml-based approach to read animation data
// *              and a more streamlined state-based approach to the engine.
// *  
// *   Script conventions:
// *   -------------------
// *   sg_variable: script global variable 
// * 
// * 
// */ 
//
//# pragma warning disable
//
//using UnityEngine;
//using System.Collections.Generic;
//using System.IO;
//using System.Xml;
//using System;
//
//
///* Sprite sg_animations must have all equal pixel-size frames. */
//public class SpriteAnimation {
//
//    public Texture2D Spritesheet;
//    public string name;
//    public int StartFrame;
//    public int EndFrame;
//    public float Fps;
//
//    public SpriteAnimation(string name, Texture2D sprite, int startFrame, int endFrame, float fps) {
//        this.name = name;
//        this.Spritesheet = sprite;
//        this.StartFrame = startFrame;
//        this.EndFrame = endFrame;
//        this.Fps = fps;
//    }
//}
//
//
//public class Animator {
//    private volatile GameObject objectToAnimate;
//    private volatile SpriteAnimation animation;
//    private volatile bool looping;
//    private volatile bool rendering;
//    private volatile float timeSinceLastDraw;
//    private volatile int currentFrame;
//
//    /* objectToAnimate: GameObject that'll be drawn to.
//     * animation:       SpriteAnimation to apply to objectToAnimate.
//     * loop:            Should the SpriteAnimation loop as soon when it fully plays?
//     * shadername:      The name of the shader as a string to apply to the SpriteAnimation.
//     * 
//     * Sets the animation to use and what to draw to. Will automatically and immediately apply 
//     * the first frame of the animation if you pass in a new animation or objectToAnimate.
//     */
//    public void Draw(GameObject objectToAnimate, SpriteAnimation animation, bool loop = false, string shadername = "Unlit/Transparent") {
//
//        // Draw next frame in series if the same animation is continuing on the same object.
//        if (objectToAnimate == this.objectToAnimate && animation == this.animation) {
//            DrawNextFrame();
//        }
//        // Otherwise, draw the first frame of the new animation.
//        else {
//            DrawFirstFrame(objectToAnimate, animation, loop, shadername);
//        }
//        currentFrame++;
//    } // end Draw()
//
//
//     /* Draws the first frame of the the loaded SpriteAnimation.*/
//    private void DrawFirstFrame(GameObject objectToAnimate, SpriteAnimation animation, bool loop = false, string shadername = "Unlit/Transparent") {
//        timeSinceLastDraw = Time.deltaTime;
//        this.animation = animation;
//        this.objectToAnimate = objectToAnimate;
//        this.looping = loop;
//        this.currentFrame = animation.StartFrame;
//
//        // Set the spritesheet as the primary texture of the surface.
//        objectToAnimate.renderer.material.mainTexture = animation.Spritesheet;
//        // Scale the spritesheet properly to the surface.
//        objectToAnimate.renderer.material.mainTextureScale =
//            new Vector2(((float)animation.Spritesheet.height) / animation.Spritesheet.width, 1.0f);
//        // Apply shader.
//        objectToAnimate.renderer.material.shader = Shader.Find(shadername);
//        // Display correct frame on objectToAnimate.
//        objectToAnimate.renderer.material.mainTextureOffset =
//            new Vector2((float)(currentFrame * animation.Spritesheet.height) / animation.Spritesheet.width, 0f);
//    } // end DrawFirstFrame()
//
//
//    /* Draws the next frame in the loaded SpriteAnimation. If sg_animator is already rendering
//     * a previous frame, nothing will be drawn. */
//    private void DrawNextFrame () {
//        timeSinceLastDraw += Time.deltaTime;
//
//        Debug.Log("animation: " + animation.name);
//
//        /* Only draw if it's time to. Must retain FPS of previous animated frame. */
//        if (timeSinceLastDraw >= animation.Fps) {
//            /* Exit if at end frame and shouldn't loop. */
//            if (currentFrame > animation.EndFrame && !looping) {
//                return;
//            }
//            /* If set to loop and at end frame, go back to beginning frame. */
//            else if (currentFrame > animation.EndFrame) {
//                currentFrame = animation.StartFrame;
//            }
//            // Display correct frame on objectToAnimate.
//            objectToAnimate.renderer.material.mainTextureOffset =
//            new Vector2((float)(currentFrame * animation.Spritesheet.height) / animation.Spritesheet.width, 0f);
//        }
//    } // end DrawNextFrame()
//
//    public bool Looping() {
//        return looping;
//    }
//
//    public bool Rendering() {
//        return timeSinceLastDraw < animation.Fps;
//    }
//}
//
//
//public class PlatformerCharacterController_mk4 : MonoBehaviour {
//
//    public float sg_jumpHeight = 5000.0f;
//
//    /* Location to Xml file holding animation data. */
//    public string sg_xmlFilename = "Xml/data_animation_samus.xml";
//
//
//    /* All possible general animation states the character may be in. */
//    private enum sg_ANIMATION_STATES {
//        STANDING,
//        WALKING,
//        RUNNING,
//        JUMPING_STRAIGHT,
//        JUMPING_ANGLED,
//        TURNING
//    }
//
//    /* States that a jump animation may be in. */
//    private enum sg_JUMPING_STATES {
//        NOT_JUMPING,
//        STRAIGHT_BRACING,
//        STRAIGHT_LIFTING,
//        STRAIGHT_APEX,
//        STRAIGHT_FALLING,
//        STRAIGHT_RECOILING
//    }
//
//
//    /* States that character direction is currently in. */
//    private enum sg_DIRECTION_STATES {
//        LEFT,
//        RIGHT,
//        TURNING_LEFT,
//        TURNING_RIGHT
//    }
//
//    /* Data bank containing all sg_animations to be used. */
//    public List<SpriteAnimation> sg_animations;
//
//    private Animator sg_animator;
//
//    /* These change every frame depending on user input and the last animation. */
//    private int sg_prevAnimationState;
//    private int sg_currentAnimationState;
//    private int sg_currentAnimationFrame = 0;
//    private int sg_currentDirectionState = (int)sg_DIRECTION_STATES.RIGHT;
//    private int sg_currentJumpingState = (int)sg_JUMPING_STATES.NOT_JUMPING;
//
//    
//    /* 
//     *  Parses Xml animation file & creates SpriteAnimation objects. path is location to Xml file
//     *  to parse, relative to the /Assets/Resources/ folder.
//     */
//    void ParseXmlIntoAnimations(string path, List <SpriteAnimation> animationList) {
//
//        /*  'directoryToProject'/Assets/Resources/'XmlFilePath'  */
//        string XmlFolderPath = Application.dataPath + "/Resources/" + path.Trim();
//
//        /*  'directoryToProject'/Assets/Resources/  */
//        string spritesheetFolderPath;
//
//        string currentname;
//        string currentFilename;
//        int currentFirstFrame;
//        int currentLastFrame;
//        float currentFPS;
//
//        XmlReaderSettings settings = new XmlReaderSettings();
//        settings.IgnoreWhitespace = true;
//        settings.CloseInput = true; // Close FileStream when XmlReader is closed.
//
//        /* A 'using' block calls the Dispose method on the reader after the block finishes. */
//        using (XmlReader reader = XmlReader.Create( new FileStream(XmlFolderPath, FileMode.Open), settings ) ) {
//            try {
//                reader.MoveToContent(); /* skip header comments & whitespace */
//                reader.ReadStartElement("character_animations");
//             
//                reader.MoveToContent(); /* skip comments & whitespace */
//
//                /* Get spritesheet folder path */
//                reader.ReadStartElement("spritesheet_folder");
//                reader.MoveToContent();
//                //print("reader.name: " + reader.name);
//                spritesheetFolderPath = reader.ReadElementContentAsString("location", "").Trim() + "/";
//                reader.ReadEndElement();
//
//                if (spritesheetFolderPath == null) {
//                    throw new XmlException("Ill-formed Xml file! No 'spritesheet_folder' defined.");
//                }
//
//                while (reader.Read() ) { /* Read Xml body content. */
//                    reader.MoveToContent(); /* skip comments & whitespace */
//                    //print("name (Should be Animation): " + reader.name);
//                    //print("reader.IsStartElement? " + reader.IsStartElement());
//                    reader.ReadStartElement();
//
//                    //print("name (Before Read name)): " + reader.name);
//                    currentname = reader.ReadElementContentAsString("name", "").Trim();
//                    //print("name (Before Read filename)): " + reader.name);
//                    currentFilename = reader.ReadElementContentAsString("filename", "").Trim();
//                    //print("name (Before Read first_frame)): " + reader.name);
//                    currentFirstFrame = reader.ReadElementContentAsInt("first_frame", "");
//                    //print("name (Before Read last_frame)): " + reader.name);
//                    currentLastFrame = reader.ReadElementContentAsInt("last_frame", "");
//                    //print("name (Before Read FPS)): " + reader.name);
//                    currentFPS = reader.ReadElementContentAsFloat("fps", "");
//
//                    /*** NO NEED TO CALL reader.ReadEndElement()!!!  ***
//                     * Already handled by reader.Read() in while loop! */
//
//                    //print("reader.IsStartElement (Should be false)? " + reader.IsStartElement());
//
//                    ///*  'directoryToProject'/Assets/Resources/'currentFilename'  */
//                    Texture2D image = (Texture2D)Resources.Load(spritesheetFolderPath + currentFilename);
//
//                    if (image == null) {
//                        throw new IOException("Image could not be loaded: " + spritesheetFolderPath + currentFilename);
//                    }
//                    else {
//                        print("Image loaded: " + spritesheetFolderPath + currentFilename);
//                    }
//                    animationList.Add(new SpriteAnimation(currentname, image, currentFirstFrame, currentLastFrame, currentFPS));
//
//                } // End while()
//            } // End try block
//
//            catch (XmlException e) {
//                print("Xml ERROR: " + e.Message + " in file '" + XmlFolderPath + "'.");
//                print("Stack trace: " + e.StackTrace);
//            }
//        }
//    } // end ParseXml()
//
//
//	void Start () {
//        sg_animations = new List <SpriteAnimation> ();
//        ParseXmlIntoAnimations(sg_xmlFilename, sg_animations);
//        sg_animator = new Animator();
//
//        sg_currentAnimationState = (int)sg_ANIMATION_STATES.STANDING;
//	} // end Start()
//
//
//
//
//    void Update() {
//        float xAxis = Input.GetAxis("Horizontal");
//        float yAxis = Input.GetAxis("Vertical");
//
//        sg_currentAnimationFrame++; //****** Might be useless!!! NOT USED.
//        
//        /*** Modifies sg_prevAnimationState, sg_currentAnimationState, sg_currentDirectionState, sg_currentJumpingState ***/
//        SetAnimationStates(xAxis, yAxis);
//
//        /*** Accesses sg_currentAnimationFrame, sg_prevAnimationState, sg_currentAnimationState, 
//         *** sg_currentDirectionState, sg_currentJumpingState, sg_animator ***/
//        DrawAnimation();
//
//        //setAnimation(xAxis, yAxis);
//        //moveCharacter(xAxis, yAxis);
//    } // end Update()
//
//
//
//    /* Sets and renders the correct texture frame to the character based on character states. 
//     * Accesses
//     * 
//     * --------------------------
//     * Global variables accessed: sg_animator, sg_currentAnimationFrame, sg_prevAnimationState, 
//     * sg_currentAnimationState, sg_currentDirectionState, sg_currentJumpingState 
//     * --------------------------
//     * 
//     */
//    void DrawAnimation() {
//
//        print("Entering DrawAnimation()");
//        string animationname = GetAnimationname();
//
//        if (animationname != null) {
//            print("Drawing new frame: " + animationname);
//
//            // Grab the new animation to draw by its name.
//            SpriteAnimation animationToDraw = FetchAnimationByname(animationname);
//
//            print("animationToDraw: " + animationToDraw.name);
//
//            if (animationToDraw != null) {
//                sg_animator.Draw(this.gameObject, animationToDraw, true);
//            }
//            else {
//                print("WARNING: Animation '" + animationname + "' was not found in sg_animations!");
//            }
//        }
//        else {
//            print("WARNING: No animation was determined this frame!");
//        }
//
//    } // end DrawAnimation()
//
//
//
//    /*
//     *  Takes a SpriteAnimation name as a string and retrieves the actual animation 
//     *  object in sg_animations. Returns null if the animation wasn't found.
//     * 
//     * --------------------------
//     * Global variables accessed: sg_animations
//     */
//    private SpriteAnimation FetchAnimationByname(string animationname) {
//        print("animationToFind: " + animationname);
//        print("sg_animations.Count: " + sg_animations.Count);
//
//        String animsToPrint = "";
//        sg_animations.ForEach(delegate(SpriteAnimation anim) { animsToPrint += anim.name + ", "; });
//
//        print("sg_animations: [" + animsToPrint.TrimEnd( new char [] {',', ' '} ) + "]");
//        return sg_animations.Find((SpriteAnimation animation) => { return animation.name == animationname; }); ;
//    }
//
//
//
//    /* Helper for DrawAnimation(). 
//     * Retrieves the name of the animation based on the current character state. 
//     * Returns null if no compatible animation name was determined. 
//     * 
//     * --------------------------
//     * Global variables accessed: sg_ANIMATION_STATES, sg_DIRECTION_STATES, sg_JUMPING_STATES,
//     * sg_currentAnimationState, sg_currentDirectionState, sg_currentJumpingState 
//     * -------------------------- 
//     */
//    private string GetAnimationname() {
//        string animationname = null;
//        if (sg_currentAnimationState == (int)sg_ANIMATION_STATES.STANDING) {
//            if (sg_currentDirectionState == (int)sg_DIRECTION_STATES.LEFT) {
//                animationname = "facingLeft";
//            }
//            else {
//                animationname = "facingRight";
//            }
//        }
//        else if (sg_currentAnimationState == (int)sg_ANIMATION_STATES.WALKING) {
//            if (sg_currentDirectionState == (int)sg_DIRECTION_STATES.LEFT) {
//                animationname = "walkingLeft";
//            }
//            else {
//                animationname = "walkingRight";
//            }
//        }
//        else if (sg_currentAnimationState == (int)sg_ANIMATION_STATES.RUNNING) {
//            if (sg_currentDirectionState == (int)sg_DIRECTION_STATES.LEFT) {
//                animationname = "runningLeft";
//            }
//            else {
//                animationname = "runningRight";
//            }
//        }
//        //else if (sg_currentAnimationState == (int)sg_ANIMATION_STATES.TURNING) {
//        //}
//        return animationname;
//    }
//
//
//    /* Determines and sets the direction, jumping, and general animation states. 
//     * 
//     * --------------------------
//     * Global Variables Modified: sg_prevAnimationState, sg_currentAnimationState
//     * --------------------------
//     */
//    private void SetAnimationStates(float xAxis, float yAxis) {
//
//        sg_prevAnimationState = sg_currentAnimationState;
//
//        if (sg_prevAnimationState == (int)sg_ANIMATION_STATES.TURNING) {
//            /* need to write */
//        }
//        else if (sg_prevAnimationState == (int)sg_ANIMATION_STATES.STANDING) {
//            HandlePrevStanding(xAxis, yAxis);
//        }
//        else if (sg_prevAnimationState == (int)sg_ANIMATION_STATES.WALKING) {
//            HandlePrevWalking(xAxis, yAxis);
//        }
//        else if (sg_prevAnimationState == (int)sg_ANIMATION_STATES.RUNNING) {
//        }
//        else if (sg_prevAnimationState == (int)sg_ANIMATION_STATES.JUMPING_STRAIGHT) {
//        }
//        else if (sg_prevAnimationState == (int)sg_ANIMATION_STATES.JUMPING_ANGLED) {
//        }
//    } // end SetAnimationStates()
//
//
//
//    /* Helper for SetAnimationStates(). 
//     * 
//     * --------------------------
//     * Global Variables Modified: sg_prevAnimationState, sg_currentAnimationState, 
//     * sg_currentDirectionState, sg_currentJumpingState.
//     * --------------------------
//     */
//    private void HandlePrevStanding(float xAxis, float yAxis) {
//        /* Standing still. */
//        if (xAxis == 0) { 
//            sg_currentAnimationState = (int)sg_ANIMATION_STATES.STANDING;
//        }
//        /* Pressing left. */
//        else if (xAxis < 0) {
//            /* Previously facing left. */
//            if (sg_currentDirectionState == (int)sg_DIRECTION_STATES.LEFT) { 
//                sg_currentAnimationState = (int)sg_ANIMATION_STATES.WALKING;
//            }
//            /* Previously facing right. */
//            else { 
//                sg_currentAnimationState = (int)sg_ANIMATION_STATES.TURNING; 
//                sg_currentDirectionState = (int)sg_DIRECTION_STATES.TURNING_LEFT;
//            }
//        }
//        /* Pressing right. */
//        else if (xAxis > 0) {
//            /* Previously facing right. */
//            if (sg_currentDirectionState == (int)sg_DIRECTION_STATES.RIGHT) { 
//                sg_currentAnimationState = (int)sg_ANIMATION_STATES.WALKING;
//            }
//            /* Previously facing left. */
//            else { 
//                sg_currentAnimationState = (int)sg_ANIMATION_STATES.TURNING; 
//                sg_currentDirectionState = (int)sg_DIRECTION_STATES.TURNING_RIGHT;
//            }
//        }
//    } // end HandlePrevStanding()
//    
//
//
//    /* Helper for SetAnimationStates(). 
//     * 
//     * --------------------------
//     * Global Variables Modified: sg_prevAnimationState, sg_currentAnimationState, 
//    * sg_currentDirectionState, sg_currentJumpingState.
//    * --------------------------
//    */
//    private void HandlePrevWalking(float xAxis, float yAxis) {
//        if (xAxis == 0) { /* Standing still. */
//            sg_currentAnimationState = (int)sg_ANIMATION_STATES.STANDING;
//        }
//        /* Pressing left. */
//        else if (xAxis < 0) {
//            /* Previously facing left. */
//            if (sg_currentDirectionState == (int)sg_DIRECTION_STATES.LEFT) { 
//                sg_currentAnimationState = (int)sg_ANIMATION_STATES.WALKING;
//            }
//            /* Previously facing right. */
//            else { 
//                sg_currentAnimationState = (int)sg_ANIMATION_STATES.TURNING;
//                sg_currentDirectionState = (int)sg_DIRECTION_STATES.TURNING_LEFT;
//            }
//        }
//        /* Pressing right. */
//        else {
//            /* Previously facing right. */
//            if (sg_currentDirectionState == (int)sg_DIRECTION_STATES.RIGHT) { 
//                sg_currentAnimationState = (int)sg_ANIMATION_STATES.WALKING;
//            }
//            /* Previously facing left. */
//            else { 
//                sg_currentAnimationState = (int)sg_ANIMATION_STATES.TURNING;
//                sg_currentDirectionState = (int)sg_DIRECTION_STATES.TURNING_RIGHT;
//            }
//        }
//    } // end HandlePrevWalking()
//
//
//
//}
//
//
//
