// ------------------------------------------------------------------------
// Orthello Pro 2D Framework Example Source Code
// (C)opyright 2011 - WyrmTale Games - http://www.wyrmtale.com
// ------------------------------------------------------------------------
// More info http://www.wyrmtale.com/orthello
// ------------------------------------------------------------------------
// Pro Example 1
// SpriteBatching
// ------------------------------------------------------------------------
// Main example class
// ------------------------------------------------------------------------
using UnityEngine;
using System.Collections;


public class CProExample1_TintedAlpha : MonoBehaviour {

    private OTSpriteBatch batch = null;         // will container a reference to the SpriteBatch
    private bool showText = true;               // indicates if the text has to be shown ( 4 drawcalls )
    private GameObject textPlane = null;        // Transparent text panel ( =  empty sprite with alpha material reference )
    private GameObject texts = null;            // GameObject with text children

    private Material whiteMat = null;           // White text material
    private Material yellowMat = null;          // Yellow (active) text material
    private string currentMode = "t1";          // Current active menu option (default option [1] - No Sprite Batching)

    // Create a screen full of tiles (100x100px)
    void CreateTiles()
    {
        // Lookup the tiles container
        GameObject Tiles = GameObject.Find("-Tiles");
        // Loop x and y so we can fill the screen with tiles
        for (float x=0; x<=(Screen.width/100); x++)
        {
            for (float y=0; y<=(Screen.width/100); y++)
            {
                // Create a new tile from its prototype
                OTSprite tile = OT.CreateObject("tile").GetComponent<OTSprite>();
                // Set the tile as a child of the container
                tile.transform.parent = Tiles.transform;
                // position the tile
                tile.position = new Vector2((Screen.width * -1 / 2) + x * 100, (Screen.height / 2) - y * 100);
                // take a random index
                // HINT : we hard-coded the framecount because in the Start() function , that calls this CreateTiles function,
                // the tile.spriteContainer is not initialized yet and will have a framecount of 0
                // alternative is to wait until all spriteContainers are ready and call the sprite generation functions
                // after that.
                tile.frameIndex = (int)Mathf.Floor(Random.value * 16);
                // Set the tile size to 100x100 pixels
                tile.size = new Vector2(100, 100);                
                // Tint 50% tiles darker color
                if (Random.value < 0.5f)
                {
                    tile.materialReference = "tint";
                    tile.tintColor = new Color(0.4f, 0.4f, 0.4f);
                }
            }
        }
    }

    // Create 60 random sized and rotated asteroids
    void CreateAsteroids()
    {
        // Lookup the asteroid container
        GameObject Asteroids = GameObject.Find("-Asteroids");
        // Iterate and create 60 asteroid sprites
        for (int a = 0; a < 60; a++)
        {
            // Create a new asteroid from its prototype
            OTSprite asteroid = OT.CreateObject("asteroid").GetComponent<OTSprite>();
            // Set the parent of this asteroid to the container
            asteroid.transform.parent = Asteroids.transform;
            // Determine a random x size
            float sx = 30+Random.value*120;
            // Set size of the asteroid
            asteroid.size = new Vector2(sx, sx * (73f / 97f));
            // Put the asteroid on a random screen position
            asteroid.position = new Vector2((Screen.width * -1 / 2) + Random.value * Screen.width, (Screen.height / 2) - Random.value * Screen.height);
            // Set the asteroid image to a random index
            asteroid.frameIndex = (int)Mathf.Floor(Random.value * 133);
            // Set a random rotation
            asteroid.rotation = Random.value * 360;
            // make 50% asteroids 50% transparent
            if (Random.value < 0.5f)
            {
                asteroid.materialReference = "alpha";
                asteroid.alpha = 0.5f;
            }
        }
    }

    // Create 60 random sized and rotated stars
    void Createstars()
    {
        // Lookup the star container
        GameObject stars = GameObject.Find("-Stars");
        // Iterate and create 60 star sprites
        for (int a = 0; a < 60; a++)
        {
            // Create a new star from its prototype
            OTSprite star = OT.CreateObject("star").GetComponent<OTSprite>();
            // Set the parent of this star to the container
            star.transform.parent = stars.transform;
            // Determine a random x size
            float sx = 30 + Random.value * 120;
            // Set size of the star
            star.size = new Vector2(sx, sx);
            // Put the star on a random screen position
            star.position = new Vector2((Screen.width * -1 / 2) + Random.value * Screen.width, (Screen.height / 2) - Random.value * Screen.height);
            // Set the star image to a random index
            star.frameIndex = (int)Mathf.Floor(Random.value * 70);
            // Set a random rotation
            star.rotation = Random.value * 360;
            // Tint 50% stars to red 
            if (Random.value < 0.5f)
            {
                star.materialReference = "tint";
                star.tintColor = Color.red;
            }
        }
    }

    // Create a shadow for each TextMesh in the text container
    void ShadowText()
    {
        Material black = null;

        // Find text container
        GameObject Texts = GameObject.Find("-Text");
        // Create an array to store all TextMesh references found
        GameObject[] textObjects = new GameObject[Texts.transform.childCount];
        // Collect the TextMeshes
        for (int t = 0; t < Texts.transform.childCount; t++)
          textObjects[t] = Texts.transform.GetChild(t).gameObject;

        // Loop all TextMesh objects
        for (int t = 0; t < textObjects.Length; t++)
        {
            TextMesh txt = textObjects[t].GetComponent<TextMesh>();
            if (black == null)
            {
                // Create black material
                black = new Material(txt.renderer.material);
                black.color = Color.black;
            }
            if (whiteMat == null)
            {
                // Create white material
                whiteMat= new Material(txt.renderer.material);
                whiteMat.color = Color.white;
            }
            if (yellowMat == null)
            {
                // Create Yellow material
                yellowMat = new Material(txt.renderer.material);
                yellowMat.color = new Color(0.9f, 0.8f, 0.2f);
            }
            // Make a copy of the TextMesh object and give it a name
            GameObject g = Instantiate(txt.gameObject) as GameObject;
            g.name = txt.name + "w";
            // Assign the white material
            g.renderer.material = whiteMat;
            // If active option assign the yellow material
            if (txt.name == currentMode)
                g.renderer.material = yellowMat;
            // Put instanced TextMesh also under the text container
            g.transform.parent = Texts.transform;
            // assign Black material to the text Mesh and offset its position
            txt.renderer.material = black;
            g.transform.position = txt.transform.position;
            txt.transform.position += new Vector3(2, -2, 0.1f);
        }
    }

    // configure the OTSpriteBatch and set the material (white or yellow) of menu option
    void Batch(string menu, bool doBatch, bool packTextures)
    {
        // Find current active menu 1-3
        GameObject menuO = GameObject.Find(currentMode + "w");
        // Give the text a white color
        if (menuO != null) menuO.renderer.material = whiteMat;
        // Assign new menu
        currentMode = menu;
        menuO = GameObject.Find(currentMode + "w");
        // Give it a yellow color
        if (menuO != null) menuO.renderer.material = yellowMat;

        // Find sprite containers
        GameObject Tiles = GameObject.Find("-Tiles");
        GameObject Asteroids = GameObject.Find("-Asteroids");
        GameObject Stars = GameObject.Find("-Stars");

        // Lets Set up the OTSpriteBatch object
        if (doBatch)
        {
            // Batching , notify if we need to pack textures
            batch.packTextures = packTextures;
            // Move all tiles as children under the OTSpriteBatch object
            while (Tiles.transform.childCount > 0)
                Tiles.transform.GetChild(0).transform.parent = batch.transform;
            // Move all asteroids as children under the OTSpriteBatch object
            while (Asteroids.transform.childCount > 0)
                Asteroids.transform.GetChild(0).transform.parent = batch.transform;
            // Move all stars as children under the OTSpriteBatch object
            while (Stars.transform.childCount > 0)
                Stars.transform.GetChild(0).transform.parent = batch.transform;
        }
        else
        {
            // No Batching so lets remove all OTSpriteBatch children and move them
            // to their original containers
            while (batch.transform.childCount > 0)
            {
                GameObject g = batch.transform.GetChild(0).gameObject;
                // check gameobject name for a star and move it
                if (g.name.IndexOf("star-") == 0)
                    g.transform.parent = Stars.transform;
                // check gameobject name for a asteroid and move it
                if (g.name.IndexOf("asteroid-") == 0)
                    g.transform.parent = Asteroids.transform;
                // check gameobject name for a tile and move it
                if (g.name.IndexOf("tile-") == 0)
                    g.transform.parent = Tiles.transform;
            }
        }
        // ReBuild the OTSpriteBatch object. OTSpriteBatch will hide itself if the are no batchable sprites.
        // Sprites will be reset if they are moved out of the spritebatch.
        batch.Build();

        // HINT: By setting the batch.autoCheckSprites setting to true and batch.autoCheckFrequency to the desired
        // frequency, the OTSpriteBatch object will check for changes and rebuild itself automaticly.
        // In this example we have disabled the autochecking mechanism and are rebuilding the batch manually.

        // HINT: When auto-checking sprites, the OTSpriteBatch also montitors the frameIndex (image), position,
        // scale and rotation of a batched sprite and will re-build ( partly or all ) as soon as a change is 
        // detected. This way the OTSpriteBatch can have moving, animating sprites with only 1 drawcall. Important
        // to note is that this will be CPU performed not GPU. You have to check for performance yourself.

        // IMPORTANT: If you want to use TexturePacking on the OTSpriteBatch object, You have to make sure that
        // all textures that you will use and want to pack have their 'readable' importer setting set, otherwise
        // you will get runtime errors. You can set this by setting the texture type on 'advanced' and checking 
        // the read/write setting on your texture in your project view.

    }

    bool started = false;
    // Because we are creating sprites from scratch, it is best to start creating our sprites
    // after all containers are initialized and ready.
    void DoStart()
    {
        // Indicate that we are creating sprites from scratch
        OT.RuntimeCreationMode();
        // Create tile sprites
        CreateTiles();
        // Create asteroid sprites
        CreateAsteroids();
        // Create star sprites
        Createstars();
        // Put nice shadows under the text 
        ShadowText();
        // Store the reference to the SpriteBatch object
        batch = GameObject.Find("-Batch").GetComponent<OTSpriteBatch>();
		
		if (batch==null)
			print("batch null");
		
		
        // Store the reference to the text container
        texts = GameObject.Find("-Text");
        // Store the reference of the transparent text panel
        textPlane = GameObject.Find("Text-Plane");
    }

	// Update is called once per frame
	void Update () {

        if (!started)
        {
            // Because we are creating sprites from scratch, it is best to start creating our sprites
            // after all containers are initialized and ready.
            if (OT.ContainersReady())
            {
                DoStart();
                started = true;
            }
        }

        // check keyboard input
        if (Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Alpha1))
            Batch("t1", false, false);  // menu option [1] - No Batching 
        else
            if (Input.GetKeyDown(KeyCode.Keypad2) || Input.GetKeyDown(KeyCode.Alpha2))
                Batch("t2", true, false); // menu option [2] - Batching - no texture packing
            else
                if (Input.GetKeyDown(KeyCode.Keypad3) || Input.GetKeyDown(KeyCode.Alpha3))
                    Batch("t3", true, true); // menu option [3] - Batching with texture packing
                else
                    if (Input.GetKeyDown(KeyCode.Keypad4) || Input.GetKeyDown(KeyCode.Alpha4))
                    {
                        // toggle text display on/off
                        showText = !showText;
#if UNITY_3_0 || UNITY_3_1 || UNITY_3_2 || UNITY_3_3 || UNITY_3_4 || UNITY_3_5
                        texts.SetActiveRecursively(showText);
                        textPlane.active = showText;
#else
                        texts.SetActive(showText);
                        textPlane.SetActive(showText);
#endif
                    }	    
        	    
	}
}
