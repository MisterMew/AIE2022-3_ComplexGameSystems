**!DEMO NOTE!**
If the fish in the demo are not facing forward, click on any of their prefabs, open the Mesh Filter component, correct the the rotaiton (Y 90), and click 'Fix Rotation'

---

### **STEP ONE** ###
Create or Select any gameObject to serve as a spawnpoint.
- Attach the script "Flock.cs" to it:
    - Populate 'Agent Prefab' with the desired agent/boid game object (must have script component "Flock Agent" attached)
    - Populate 'Behaviour' with a custom scriptable object (Create > Stadaroj)
    - Populate the remaining variables as desired

### **STEP TWO** ###
Create a custom behaviour object (Create > Stadaroj > 'CustomBehaviour')
Click the 'Add Behaviour' and populate with any Stadaroj Scriptable Object Behaviour (you must create these manually)
- The weights are multipliers that control the intensity of each behaviours

---

## **BEHAVIOURS** ##
CUSTOM BEHAVIOUR: Allows a combination of multiple behaviours.

### **BOID BEHAVIOURS** ###
ALIGNMENT: Directs the position of the agent to steer towards the average facing direction of nearby flockmates.

COHESION: Directs the position of the agent to steer towards the average position of nearby flockmates.

SEPERATION: Directs the position of the agent to steer away from individual nearby flockmates, to avoid crowding.

SEEK: Directs the agent to move towards a desired position.

PURSUE: Like seek, but moves towards the predicted position of a moving target.

AVOID: Directs the agent to move away from a desired position.

FLEE: Like avoid, but moves away from the predicted position of a moving target.

### **SUPPORTING BEHAVIOURS** ###
RESTRICT TRAVEL DISTANCE: Restricts the agents of a flock from travelling too far away from a desired position.

### **FILTERS** ###
FLOCK GROUP FILTER: Filters all agents into their relevant flocks. This prevents multiple flocks from interacting as one.

PHYSICS LAYER FILTER: Filters a flock to check if it shares a layer with the selected layer. (*Used for detecting obstacles, assuming they have coliders.*)

---

*Stadaroj v0.1.0*

*Jazmin Fazzolari, AIE 2022*