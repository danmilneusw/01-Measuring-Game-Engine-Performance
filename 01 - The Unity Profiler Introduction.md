# 01 - Introduction to Profiling
## Performance Budget
While FPS (frames per second) may be the metric that end users are aware of, it is not the most suitable measure for optimizing game performance. Frames are comprised of lots of function calls which each take milliseconds (ms) to complete. The time spent on individual function calls can be measured and we can spot which ones are using up the majority of the hardware by seeing which ones take the most time to complete. To optimise game performance, it is essential to focus on reducing the time spent on function calls, as this directly impacts FPS.

The performance budget is the total time (ms) spent for a frame to process. This budget can be calculated as:

Budget (ms) = 1000 ms / target FPS

For **30 FPS**, your budget is **33.33 ms**

For **60 FPS**, your budget is **16.66 ms**

For **90 FPS**, your budget is **11.11 ms**

Developers often set a performance budget that they are aiming for based on the type of game and hardware their game will run on. For example, this could be 90 FPS for VR games, 60 FPS for a first person shooter, or 30 FPS for a handheld console. Once they have agreed on an FPS, they know what their performance budget is.

## Profiling Types
Profilers are typically instrumentation-based or sample-based.

An instrumentation-based profiler inserts markers around every function call, allowing for capturing every process. This adds significantly more overhead than sample-based profiling, which takes a snapshot/sample every few milliseconds, but this does mean you get a more in-depth analysis and without missing any potential performance spikes.

It’s important to note that because of this overhead, the performance will be a little slower and may not be representative of the final performance after building and with the profiler disabled. So after optimising, you should use FPS as a final performance metric, as this can be measured without a profiler and with minimal overhead.

## Using the Unity Profiler
### 1. Open the Unity Project
Open up **01 - Unity Project** in Unity. This project contains three scenes I have made for experimenting with the profiler. We'll use the other two scenes later, but for now open scene **01 - Inefficient Cubes**. It should look like the following:

<div align="center">
  <a href="Images\01 - The Unity Profiler Introduction\09 - Inefficient Cubes Scene.png" target="_blank">
    <img src="Images\01 - The Unity Profiler Introduction\09 - Inefficient Cubes Scene.png" alt="Inefficient Cubes Scene" style="height:200px;"/>
  </a>
</div>

This scene contains 25 cubes each with a script attached that makes them rotate. This script is intentionally quite inefficient. Run the scene and open the Statistics window by selecting Stats as shown in the below image to quickly see the FPS.

<div align="center">
  <a href="Images\01 - The Unity Profiler Introduction\10 - Inefficient Cubes Scene with Stats.png" target="_blank">
    <img src="Images\01 - The Unity Profiler Introduction\10 - Inefficient Cubes Scene with Stats.png" alt="Inefficient Cubes Scene" style="height:200px;"/>
  </a>
</div>

If the FPS is ridiculously low, it's made Unity difficult to run for example, then disable some cubes using the inspector until performance improves:

<div align="center">
  <a href="Images\01 - The Unity Profiler Introduction\11 - Cube Disabled.png" target="_blank">
    <img src="Images\01 - The Unity Profiler Introduction\11 - Cube Disabled.png" alt="Disabling a Cube" style="height:75px;"/>
  </a>
</div>

Next to the FPS in parentheses, you can see the number of ms it took for each frame to complete. We can get an in-depth look at this in the profiler.


### 2. Profiling the Project
Unity uses an instrumentation-based profiler, so every process is captured over every frame. Open it at:

Window > Analysis > Profiler

If you'd like, you can drag the profiler to fit into the lower section, like seen below.

<div align="center">
  <a href="Images\01 - The Unity Profiler Introduction\12 - Positioning the Profiler.png" target="_blank">
    <img src="Images\01 - The Unity Profiler Introduction\12 - Positioning the Profiler.png" alt="Profiler Modules" style="height:300px;"/>
  </a>
</div>

#### Profiler Buttons
<div align="center">
  <a href="Images\01 - The Unity Profiler Introduction\05 - Profiler Buttons.png" target="_blank">
    <img src="Images\01 - The Unity Profiler Introduction\05 - Profiler Buttons.png" alt="Profiler Buttons" style="height:25px;"/>
  </a>
</div>

The buttons and fields shown above, from left to right, allow you to:
- Select to test in Play Mode or Edit Mode. Play Mode will profile the standalone game whereas Edit Mode will include profiling of any Editor extensions ([learn more here](https://docs.unity3d.com/Packages/com.unity.test-framework@2.0/manual/edit-mode-vs-play-mode-tests.html)).
- Set to record profiling information. Profiling won't occur unless this is selected. You can set this to off, run the game, then set this on and off at specific points to profile for specific periods of your game.
- Move to the frame before the one currently selected.
- Move to the frame after the one currently selected.
- Move to the current frame.
- See the number of the current frame out of the total frames recorded.
- Clear the profiling data.

I recommend also enabling Deep Profile, this injects profiler instrumentation around every script method to record all function calls. Without this enabled, you will be able to profile to the script only, but with it enabled you will be able to see the time spent on specific functions within that script.

#### Profiler Modules
The profiler is split into Profiler Modules. The ones you'll most likely frequent are CPU Usage and GPU Usage.

<div align="center">
  <a href="Images\01 - The Unity Profiler Introduction\06 - Profiler Modules.png" target="_blank">
    <img src="Images\01 - The Unity Profiler Introduction\06 - Profiler Modules.png" alt="Profiler Modules" style="height:500px;"/>
  </a>
</div>

#### Run the Game
Ensure Play Mode is selected. The usage windows can't be zoomed, they are fixed to scale to the maximum value. Because of this, I recommend letting the game run for a couple of seconds and then hitting record to avoid the likely usage spike in the intital frames when the game begins, making the usage easier to read. The Profiler captures the last 300 frames, you can increase this in the settings, but this will come with more overhead. For now, keep the default value.

Make a recording in the Profiler, select a random frame and take a look at the information in the Hierarchy. Before moving on, see if you can find the two functions from the InefficientCubeRotation script that are causing perfromance issues.

In this screenshot, we can see the two functions from our script that are causing the problem:

<div align="center">
  <a href="Images\01 - The Unity Profiler Introduction\13 - Viewing the Hierarchy.png" target="_blank">
    <img src="Images\01 - The Unity Profiler Introduction\13 - Viewing the Hierarchy.png" alt="Viewing the Hierarchy" style="height:300px;"/>
  </a>
</div>

These two functions are the RotateCube and PrintRotation functions. You can see that this is called 25 times (once for each cube) and that functions, like Transform.Rotate(), that we have set to run 1,000 times per frame, means there is a total of 25,000 calls.

Change from Hierarchy view to the Timeline view. In this view, we can get a more visual representation of the hierarchy. We can even see the many sub-functions used in the PrintRotation function, such as for converting the vector information to a string and so on, just to print this information to the log. A lot happens when printing to the console, it is a very slow process.

<div align="center">
  <a href="Images\01 - The Unity Profiler Introduction\14 - Viewing the Timeline.png" target="_blank">
    <img src="Images\01 - The Unity Profiler Introduction\14 - Viewing the Timeline.png" alt="Viewing the Timeline" style="height:200px;"/>
  </a>
</div>

The Profiler is split into many modules, such as:
- CPU Usage
- GPU Usage
- Rendering
- Memory
- Audio
- Video
- Physics

Sometimes you can predict that GPU usage will be low. This scene doesn't use shaders, textures, have complex shadows or do anything GPU intensive for example. Sometimes it's worth double-checking for any GPU usage you might have missed by enabling GPU usage, this does come with an overhead, so disable it after checking. As seen below, I found GPU usage in this scene to be very low. If you want to check the GPU usage, you may need to disable graphic jobs at Edit > Project Settings > Graphic Jobs.

<div align="center">
  <a href="Images\01 - The Unity Profiler Introduction\15 - GPU Usage is Low.png" target="_blank">
    <img src="Images\01 - The Unity Profiler Introduction\15 - GPU Usage is Low.png" alt="GPU Usage is Low" style="height:20px;"/>
  </a>
</div>

If you notice a performance spike (a sudden drop in performance), this may actually be due to the overhead of the Editor.

<div align="center">
  <a href="Images\01 - The Unity Profiler Introduction\07 - EditorLoop is Majority Usage in a Spike.png" target="_blank">
    <img src="Images\01 - The Unity Profiler Introduction\07 - EditorLoop is Majority Usage in a Spike.png" alt="Gfx.WaitForPresentOnGfxThread" style="height:300px;"/>
  </a>
</div>

This can be avoided by profiling on the target platform (next section).

#### Profiling on the Target Platform
Profiling in the editor is useful for rapid iteration during the optimisation process. However, it is recommended to profile on your target platform as this is the only way to isolate the game's CPU, GPU, and memory usage from the editor's usage. This is out of scope for today, but you can learn more [here](https://docs.unity3d.com/Manual/profiler-profiling-applications.html). If you do this, it's always worth noting that you will have to go to Project Settings > Quality Settings > Vsync Count, and set Vsync Count to 'Don't sync'. You should also turn off the FPS lock.

#### Rendering API Calls
As you profile more projects, you may start to notice some common occurances, such as Gfx.WaitForPresentOnGfxThread, like shown below.

<div align="center">
  <a href="Images\01 - The Unity Profiler Introduction\02 - Gfx.WaitForPresentOnGfxThread.png" target="_blank">
    <img src="Images\01 - The Unity Profiler Introduction\02 - Gfx.WaitForPresentOnGfxThread.png" alt="Gfx.WaitForPresentOnGfxThread" style="height:300px;"/>
  </a>
</div>

You may see ‘Gfx.WaitForPresent’ in the profiler and wonder why it takes up a chunk of the budget. This is a constraint imposed by the game engine. The CPU and GPU operations run in parallel, but that doesn’t mean one can’t be waiting for operations to complete on another before running. In the following example, we are CPU-bound; the GPU is waiting for the CPU to complete its operations and didn't receive the code needed to render the frame that need to be passed to the graphic rendering APIs. The GPU didn’t have the data it needed yet to be able to render the next frame and the GPU was ‘waiting’, causing a delay.

<div align="center">
  <a href="Images\01 - The Unity Profiler Introduction\18 - CPU Bound.png" target="_blank">
    <img src="Images\01 - The Unity Profiler Introduction\18 - CPU Bound.png" alt="CPU Bound" style="height:200px;"/>
  </a>
</div>
<div align="center">
  <a href="https://www.youtube.com/watch?v=uXRURWwabF4">Image source</a>
</div>

To prevent this, consider optimisation techniques to reduce having to pass data between the CPU and GPU (more on this another time).

Alternatively, if we are GPU-bound, the CPU is waiting for the GPU to complete its operations.

#### Saving and Comparing Profiling Data
Use the original (inefficient) script and make a recording. You can save this data using the save button at the top right of the profiler if you wanted. Instead, open the Profile Analyzer at Window > Analysis > Profile Analyzer. In this tool, you can compare frames within the same recording or even compare frames between multiple recordings.

Select the Compare option and select the blue Pull Data button. This will move the data from the profiler to the analyzer.

<div align="center">
  <a href="Images\01 - The Unity Profiler Introduction\17 - Loading Data into the Profile Analyser.png" target="_blank">
    <img src="Images\01 - The Unity Profiler Introduction\17 - Loading Data into the Profile Analyser.png" alt="Loading Data into the Profile Analyser" style="height:200px;"/>
  </a>
</div>

Make another recording, this time with the PrintRotation script and loop commented out, except for the rotation line. This should look like this:

```
using System.Collections;
using UnityEngine;

public class InefficientCubeRotation : MonoBehaviour
{
    // Private variable for rotation speed
    private float rotationSpeed = 10000f;

    // Update method is called once per frame
    void Update()
    {
        // Call RotateCube method in each frame
        RotateCube();
        // Call PrintRotation method in each frame
        // PrintRotation();
    }

    // Define RotateCube method
    void RotateCube()
    {
        // Run the loop 1000 times
        // for (int i = 0; i < 1000; i++)
        // {
            // Rotate the cube around the up axis at the speed of rotationSpeed per second
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        // }
    }

    // Define GetUpAxisRotation method
    // void PrintRotation()
    // {
    //     // Get the rotation of the up axis of the cube
    //     Quaternion rotation = transform.rotation;
    //     // Print the rotation to the console
    //     Debug.Log("Up axis rotation: " + rotation.eulerAngles);
    // }
}
```

Go back to the Profile Analyzer and pull this data using the red button. You will now be able to compare the performance differences side by side. Below, you can see how much longer the player loop runs for when using the inefficient script.

<div align="center">
  <a href="Images\01 - The Unity Profiler Introduction\16 - Profile Analyser.png" target="_blank">
    <img src="Images\01 - The Unity Profiler Introduction\16 - Profile Analyser.png" alt="Profile Analyzer" style="height:300px;"/>
  </a>
</div>

