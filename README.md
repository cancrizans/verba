# VerBa
A Unity vertex colour baker for lighting and textures for low poly meshes, oriented to lightmapping static geometry.

VerBa works directly in the Unity editor. This is your typical workflow:

1. Model and texture your static meshes without lighting. You can also paint some vertex colours manually to emphasize certain features.
2. In Unity, lay down copies of your mesh and VerbaLight objects.
3. The Bake replaces each copy original mesh in the MeshFilters with an instance of your mesh asset with the lighting baked into the vertex colours.
4. Profit. It's ready for play mode.

# Installation

Coming soon...

# Usage

## Setup

You will need to ensure the following things are true for VerBa to work correctly:

- All meshes that need to be baked must be marked as editable. Check the box that says "Read/Write enabled" in the model asset.
- Your models in-game are displayed with a shader that:
  1. Shows the vertex colours.
  2. If textured, the texture sample is multiplied with the vertex colour.
  3. It's Unlit (white colour + white texture = pure white).
- Your rendering pipeline is uses the Gamma colorspace, not Linear.

The shader is not bundled with this package. It's up to you.

## Basic Walkthrough

Let's say we have modeled and textured a Mesh, and we've laid down a few copies of it in our scene. We want to light them up without having to resort to real-time vertex lights. I assume your materials and their shaders are set up correctly as per the previous section.

As a first step we create a Baking Profile asset. Right click in an Asset folder, then `Create/Verba/Baking Profile`. This holds basic overall options that may be useful to share amongst many if not all meshes in a scene. Set the `Occlusion Mask` to `Nothing` (this disables all shadows for now), and the `Color Space` to `Linear`, which means the light calculations and mixing will use the Linear colorspace (but the final rendering always assumes a Gamma pipeline).

Then we get to each of our meshes, specifically the GameObjects that have the MeshFilter component. Add a VerbaOven component to each of them. You'll see the `Source Mesh` property automatically pick up the Mesh *asset*, which is the original mesh we modeled no matter what happens in the Scene, and the `Target MeshFilter` property is also automatically set to our own. Set the `Bake Profile` to the one we created. We can ignore `Original Colors Behaviour` for now since we didn't paint any vertex colours on the original mesh.

This step is optional, but very convenient. We can create an empty GameObject with a `VerbaBakeMaster` component, and set its `Behaviour` to `Bake Everything`. The Bake Master is just a big Bake button that tells all Ovens in the scene to Bake.  Otherwise, you always have the option to only manually bake the Ovens you need with their own Bake button.

We now lay down some lights. Select `GameObject/Verba/` to create some lights, like Ambient, Directional and Point. These are all pretty self-explanatory, alongside their properties.

Finally, press the Bake button from the BakeMaster, and voilà.
