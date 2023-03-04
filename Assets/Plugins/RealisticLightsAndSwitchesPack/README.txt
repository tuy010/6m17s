//
// Realistic Lights and Switches Pack
// A model and script package that allows you to setup systems of working light fixtures and switches for your scene
// Copyright 2017 While Fun Games
// http://whilefun.com
//

This package will let you quickly and easily light your scene with functioning lights and switches.

Create multiple custom light systems with as many lights and switches as you want in each system.

Each Lighting System has one or more lights and switches. The switches can be turned on and off to control each system.
Customize the the color and materials of any of the many included light and switch prefabs, or easily import your own models to work with the scripts.

The package includes the following:

Twelve classic and modern light switch models and sound effects, with ready to use prefabs
Ten classic and moden light fixtures with regular and illuminated materials, with ready to use prefabs
Scripts for the Lights, Light Switches, Lighting System, and Player Control for activating the switches


Creating a Lighting System:
--------------------------
1) Create a new scene
2) Add a player game object (e.g. Standard FPS Character Controller), and add the "PlayerLightControlScript" script to it
3) To create a Lighting System, simply drag the LightSystemTemplate prefab into your scene, and name it something like "LightingSystemLivingRoom"
4) Select a Light prefab from the Prefabs -> Lights folder (e.g. WallLightModernB) and make this a child object of your LightingSystemLivingRoom from step 2)
5) Select a LightSwitch prefab from the Prefabs -> Switches folder (e.g. DecorStyleAluminumSwitch) and make this a child object of your LightSystemLivingRoom from step 2)
6) Run your scene. Walk the player to the light switch, and press the "Flip Switch Key" (Default 'F') to test your Lighting System
7) Add as many lights and switches to your Lighting System as you want, and add more Lighting Systems to your scene.

If your light switches don't want to toggle, ensure that it has a Box Collider that the player can get line of sight to see. Also ensure the switch's maxSwitchDistance is sufficient.

Note: All of the included Light Prefabs can also be used in "dummy mode" by simply placing them in the scene and leaving them out of a lighting system.

Creating a Custom Light Switch:
------------------------------
1) Create a model with an animation called "ToggleSwitch". When importing to Unity, ensure that "Play Automatically" is unchecked. Also ensure you only go from frames zero to N, where N is the last frame of your animation.
2) To make your model compatible with the Lighting System, simply drag the LightSwitchTemplate prefab into your scene and delete the "lightSwitchNorthAmericaRegular" child object.
3) Drag your model into the scene, and make it a child object of LightSwitchTemplate (where "lightSwitchNorthAmericaRegular" was). Tag your model with "LightSwitchModel", and you're done.

It is recommended that you save this new switch as its own prefab.

Creating a Custom Light:
-----------------------
1) Create a model, with at least 2 sub-meshes (e.g. one mesh for the fixture, and one for the bulb)
2) Import your model into Unity and create a second material for the light's illumated state (e.g. MyCustomLight_Illum), using a "Self-Illumin" shader
3) To make your model compatible with the Lighting System, simply drag the LightTemplate prefab into your scene and delete the "edisonLight" child object.
4) Drag your model into the scene, and make it a child object of LightTemplate (where "edisonLight" was). 
5) Tag one or more sub-meshes of your model with "LightMeshIlluminator" that you want to light up when the light is turned on (e.g. the light bulb in the model)
6) Change the "Light Illuminator On" and "Light Illuminator Off" materials to be the materials for your new model

It is recommended that you save this new light as its own prefab. You may also change the light colors, and add/remove/move the position of the light child objects (e.g. Point and Spot lights)


If you have any problems using the scripts, or have tweaks or new features you'd like to see included, please let me know.

Thanks,
Richard

@whilefun
support@whilefun.com

THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.