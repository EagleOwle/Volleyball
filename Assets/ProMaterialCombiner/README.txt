ProMaterialCombiner

To open the app, click on Window -> Pro Material Combiner

ProMaterial Combiner is an editor extension that just by selecting an object
and clicking on combine it will compress all your materials to just one.

This package will remap and combine automatically each of your UV maps on your
meshes to a single atlas.

With this you will get rid of the overhead of having several materials on each of your
meshes and also reduce the number of draw calls each of your renderers consume.

Have specific / custom shaders?, dont worry, the tool will automatically
recognize any custom shader and work with it.

UNITY 5 supported.

NOTES:
- Easy, Just select your object and click combine.
- No scripting required.
- Meshes automatically set up and adjusted.
- Support for any custom shader (detected automatically).
- Full lightmapping support.
- Your source assets will not be touched.
- Works with colored and textured materials.
- Supports tiled materials.
- Generate prefabs from the combined object.
- Supports skinned mesh renderers.
- Malformed UVs on colored-only materials are organized automatically


NOTE: In order to combine materials all shaders have to be the same type.

/*** REMEMBER ***/
Dont name any custom shader with the word "Standard" as the tool will
recognize it as a standard type of shader and will cause errors.
/******************************* TUTORIAL ************************************/
Quick how to:
Atlas name(optional): [Text field] This will rename the generated textures if used
Auto select: [Check box] If checked, the selected object will be automatically be used for combining
Generate Prefab: [Check box] If checked a prefab will be generated when the object is combined.
	Note: Prefabs are saved into a folder created by the same name of the current opened scene under the path where the scene is located in the project view.

Combine: [Button] when clicked the selected object will be combined (if assembled correctly).

/**** Contact me ****/
Any comments / suggestions / bugs?, drop me a line at:
support@pencilsquaregames.com
/********************/

/**** Known Issues *****/
/********************/

Check my other projects!
- Pro Draw Call optimizer: Combine and atlas your objects textures to minimize draw call usage
-- https://www.assetstore.unity3d.com/#/content/16538

- Pro Pivot Modifier: Modify your meshes pivot quickly and easy.
-- https://www.assetstore.unity3d.com/en/#!/content/8913

- Pro Mouse: Set the cursor position wherever you want
-- https://www.assetstore.unity3d.com/en/#!/content/8910
