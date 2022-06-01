# !DEMO NOTE! #
If the fish in the demo are not facing forward, click on any of their prefabs, open the Mesh Filter component, correct the the rotaiton (Y 90), and click 'Fix Rotation'


# STEP ONE #
Create or Select any gameObject to serve as a spawnpoint.
- Attach the script "Flock.cs" to it:
    - Populate 'Agent Prefab' with the desired agent/boid game object (must have script component "Flock Agent" attached)
    - Populate 'Behaviour' with a custom scriptable object (Create > Stadaroj)
    - Populate the remaining variables as desired

# STEP TWO #
Create a custom behaviour object (Create > Stadaroj > 'CustomBehaviour')
Click the 'Add Behaviour' and populate with any Stadaroj Scriptable Object Behaviour (you must create these manually)
- The weights are multipliers that control the intensity of each behaviours