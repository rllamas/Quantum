// ------------------------------------------------------------------------
// Orthello Pro 2D Framework Example 
// (C)opyright 2011 - WyrmTale Games - http://www.wyrmtale.com
// ------------------------------------------------------------------------
// More info http://www.wyrmtale.com/orthello
// ------------------------------------------------------------------------
// Because Orthello is created as a C# framework the C# classes 
// will only be available as you place them in the /Standard Assets folder.
//
// If you would like to test the JS examples or use the framework in combination
// with Javascript coding, you will have to move the /Orthello/Standard Assets folder
// to the / (root).
//
// This code was commented to prevent compiling errors when project is
// downloaded and imported using a package.
// ------------------------------------------------------------------------
// Pro Example 1
// SpriteBatching
// ------------------------------------------------------------------------
// Main Pro Example 1 Demo class
// ------------------------------------------------------------------------

/*
private var batch:OTSpriteBatch = null;     // will container a reference to the SpriteBatch
private var showText:boolean = true;        // indicates if the text has to be shown ( 4 drawcalls )
private var textPlane:GameObject = null;    // Transparent text panel ( =  empty sprite with alpha material reference )
private var texts:GameObject = null;        // GameObject with text children

private var whiteMat:Material = null;           // White text material
private var yellowMat:Material = null;          // Yellow (active) text material
private var currentMode:String = "t1";          // Current active menu option (default option [1] - No Sprite Batching)

// Create a screen full of tiles (100x100px)
function  CreateTiles()
{
    // Lookup the tiles container
    var Tiles:GameObject = GameObject.Find("-Tiles");
    // Loop x and y so we can fill the screen with tiles
    for (var x:Number=0; x<=(Screen.width/100); x++)
    {
        for (var y:Number=0; y<=(Screen.width/100); y++)
        {
            // Create a new tile from its prototype
            var tile:OTSprite = OT.CreateObject("tile").GetComponent("OTSprite");
            // Set the tile as a child of the container
            tile.transform.parent = Tiles.transform;
            // position the tile
            tile.position = new Vector2((Screen.width * -1 / 2) + x * 100, (Screen.height / 2) - y * 100);
            // take a random index
            // HINT : we hard-coded the framecount because in the Start() function , that calls this CreateTiles function,
            // the tile.spriteContainer is not initialized yet and will have a framecount of 0
            // alternative is to wait until all spriteContainers are ready and call the sprite generation functions
            // after that.
            tile.frameIndex = Mathf.Floor(Random.value * 16);
            // Set the tile size to 100x100 pixels
            tile.size = new Vector2(100, 100);                
        }
    }
}

// Create 60 random sized and rotated asteroids
function CreateAsteroids()
{
    // Lookup the asteroid container
    var Asteroids:GameObject = GameObject.Find("-Asteroids");
    // Iterate and create 60 asteroid sprites
    for (var a:int = 0; a < 60; a++)
    {
        // Create a new asteroid from its prototype
        var asteroid:OTSprite = OT.CreateObject("asteroid").GetComponent("OTSprite");
        // Set the parent of this asteroid to the container
        asteroid.transform.parent = Asteroids.transform;
        // Determine a random x size
        var sx:Number = 30+Random.value*120;
        // Set size of the asteroid
        asteroid.size = new Vector2(sx, sx * (73f / 97f));
        // Put the asteroid on a random screen position
        asteroid.position = new Vector2((Screen.width * -1 / 2) + Random.value * Screen.width, (Screen.height / 2) - Random.value * Screen.height);
        // Set the asteroid image to a random index
        asteroid.frameIndex = Mathf.Floor(Random.value * 133);
        // Set a random rotation
        asteroid.rotation = Random.value * 360;
    }
}

// Create 60 random sized and rotated stars
function Createstars()
{
    // Lookup the star container
    var stars:GameObject = GameObject.Find("-Stars");
    // Iterate and create 60 star sprites
    for (var a:int = 0; a < 60; a++)
    {
        // Create a new star from its prototype
        var star:OTSprite = OT.CreateObject("star").GetComponent("OTSprite");
        // Set the parent of this star to the container
        star.transform.parent = stars.transform;
        // Determine a random x size
        var sx:Number = 30 + Random.value * 120;
        // Set size of the star
        star.size = new Vector2(sx, sx);
        // Put the star on a random screen position
        star.position = new Vector2((Screen.width * -1 / 2) + Random.value * Screen.width, (Screen.height / 2) - Random.value * Screen.height);
        // Set the star image to a random index
        star.frameIndex = Mathf.Floor(Random.value * 70);
        // Set a random rotation
        star.rotation = Random.value * 360;
    }
}

// Create a shadow for each TextMesh in the text container
function ShadowText()
{
    var black:Material = null;

    // Find text container
    var Texts:GameObject = GameObject.Find("-Text");
    // Create an array to store all TextMesh references found
    var textObjects:Array = new Array();
    // Collect the TextMeshes
    for (var t:int = 0; t < Texts.transform.childCount; t++)
      textObjects[t] = Texts.transform.GetChild(t).gameObject;

    // Loop all TextMesh objects
    for (t = 0; t < textObjects.length; t++)
    {
        var txt:TextMesh = textObjects[t].GetComponent("TextMesh");
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
        var g:GameObject = Instantiate(txt.gameObject) as GameObject;
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
function Batch(menu:String, doBatch:boolean, packTextures:boolean)
{
    // Find current active menu 1-3
    var menuO:GameObject = GameObject.Find(currentMode + "w");
    // Give the text a white color
    if (menuO != null) menuO.renderer.material = whiteMat;
    // Assign new menu
    currentMode = menu;
    menuO = GameObject.Find(currentMode + "w");
    // Give it a yellow color
    if (menuO != null) menuO.renderer.material = yellowMat;

    // Find sprite containers
    var Tiles:GameObject = GameObject.Find("-Tiles");
    var Asteroids:GameObject = GameObject.Find("-Asteroids");
    var Stars:GameObject = GameObject.Find("-Stars");

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
            var g:GameObject = batch.transform.GetChild(0).gameObject;
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

private var started:boolean = false;
// Because we are creating sprites from scratch, it is best to start creating our sprites
// after all containers are initialized and ready.
function DoStart()
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
    batch = GameObject.Find("-Batch").GetComponent("OTSpriteBatch");
    // Store the reference to the text container
    texts = GameObject.Find("-Text");
    // Store the reference of the transparent text panel
    textPlane = GameObject.Find("Text-Plane");
}

// Update is called once per frame
function Update () {

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
                    texts.SetActiveRecursively(showText);
                    textPlane.active = showText;
                }
    	            	    
}
*/