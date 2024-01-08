# 02 - Time Slicing
## Time Slicing Introduction
Time Slicing is a fairly straight-forward optimisation technique that stems from the idea that we can spread computation over multiple frames.

Take the below example, we have four scripts that run every three frames. Looking at this in our profiler we will likely see performance spikes aligned with when these scripts run, as they all have to finish executing in a single frame.

<div align="center">
  <a href="Images\02 - Time Slicing\01 - Time Slicing.png" target="_blank">
    <img src="Images\02 - Time Slicing\01 - Time Slicing.png" alt="Time Slicing 1" style="height:100px;"/>
  </a>
</div>

<div align="center">
  <a href="Images\02 - Time Slicing\06 - Spikes.png" target="_blank">
    <img src="Images\02 - Time Slicing\06 - Spikes.png" alt="Spikes" style="height:110px;"/>
  </a>
</div>

In the below example, we simply shifted scripts three and four to run a frame later than scripts one and two. Assuming all four scripts require the same computation, then for some frames we just reduced the time required to run those frames by half.

<div align="center">
  <a href="Images\02 - Time Slicing\02 - Time Slicing.png" target="_blank">
    <img src="Images\02 - Time Slicing\02 - Time Slicing.png" alt="Time Slicing 2" style="height:100px;"/>
  </a>
</div>

Some scripts may be lower priority than other scripts. In the case below, we only needed to run the fourth script every six frames rather than every three frames, saving further computation.

<div align="center">
  <a href="Images\02 - Time Slicing\03 - Time Slicing.png" target="_blank">
    <img src="Images\02 - Time Slicing\03 - Time Slicing.png" alt="Time Slicing 3" style="height:100px;"/>
  </a>
</div>

## Time Slicing Examples
Sometimes time slicing is actually implemented without noticing. For example, if a player enters a new zone in a level, we know there would be a performance spike if we load all of that new zone at once. So we know we could load everything within a near-vicinity of the player, then gradually load the remainder of the zone as the player progresses.

But if we are aware that we actually just implemented an optimisation technique, we can consider using it in some more unique ways we might not have initially considered.

Imagine a game with multiple machine gun turrets that lock onto and follow the player's location, shooting at the player. We could run this script every frame for every turret or we could make half the turrets' scripts run on one frame, then the remaining half on the next frame, and repeat. This would cut computation by half. Furthermore, rather than randomly selecting the turrets into two halves, we could make the turrets closest to the player run their scripts over more frames and have the turrests further away run over fewer frames.

We could take the idea mentioned last to a more extreme scenario, such as animations. In the example below, we see some NPCs moving their arms at a slow frame rate. Time slicing has been implemented here. Excusing the quality of the GIF, see if you can notice how before reading on.

<div align="center">
  <a href="Images\02 - Time Slicing\05 - Animations Demo.gif" target="_blank">
    <img src="Images\02 - Time Slicing\05 - Animations Demo.gif" alt="Animations Demo" style="height:300px;"/>
  </a>
</div>
<div align="center">
  <a href="https://www.youtube.com/watch?v=iGah8RemjE0&t=435s">GIF source</a>
</div>

The quality of the animations has been prioritised based on their distance from the camera. The animations furthest back only use about five steps to complete one whole rotation of the arms, whereas moving forward they use considerably more. Time slicing is particullarly useful when there's lots going on, for example, if you encountered these enemies in an intense, high combat scenario, you likely wouldn't notice the animations being less smooth if they're in the distance.

Time slicing has many applications. It's most commonly used to reduce the computation required for AI's that perform path-finding. For example, it might be noticeable to the player if an AI is taking a slightly irregular path because it is dynamically updating its location every 30 frames rather than every frame, however, this would be likely unnoticeable for AI's that aren't nearby to the player.

The key to effective time slicing is to prioritise tasks based on their importance and the impact they have on the game’s performance and player experience.

Some other applications include:
- Artificial Intelligence (AI): This includes elements like Behavior Trees, pathfinding algorithms, perception systems.
- Visuals: This can involve managing low frame-rate animations and manually optimizing the Level of Detail (LOD, we will cover this in a future session).
- General: Any check mechanisms in place, such as checking player position for event triggering.

## Time Slicing Unity Project
Next, we'll look at how an implementation of time slicing improving performance. Open the scene called '02 - Time Slicing'. It contains 1200 cubes that each move 0.01 units towards the red target cube every frame.

<div align="center">
  <a href="Images\02 - Time Slicing\04 - Time Slicing.png" target="_blank">
    <img src="Images\02 - Time Slicing\04 - Time Slicing.png" alt="Inefficient Cubes Scene" style="height:300px;"/>
  </a>
</div>

Run the scene and you'll see each cube updates simulatenously every frame, creating a big performance hit.

Take a moment to think about a way you could use time slicing to optimise this scene. **Hint:**
The scene contains grey markers that signify when the cubes are within 25, 75, and 125 units radius of the red target cube.

Highlight all the cubes. You should see two scripts implemented in the inspector of each cube (like shown below). Deselect CubeMover and select CubeMoverWithTimeSlicing. Run the scene again and you will see a performance improvement. It may be quite obvious now how this optimisation is implemented, but is it obvious from the perspective of the red target cube?

<div align="center">
  <a href="Images\02 - Time Slicing\07 - Scripts.png" target="_blank">
    <img src="Images\02 - Time Slicing\07 - Scripts.png" alt="Scripts" style="height:50px;"/>
  </a>
</div>

## Task
1. Profile using the CubeMover script and save the data. Refer to '01 - The Unity Profiler Introduction' for a reminder.
2. Profile using the CubeMoverWithTimeSlicing script and save the data. You can highlight all the cubes and select which script to use in the inspector.
3. Load both sets of data into the profiler and compare the impact of optimisation.
4. Take a look at the CubeMoverWithTimeSlicing script and take moment to think about how it works.
