# SolidEdge-VarHandler

Francesco Arfilli 2025

Solid Edge VarHandler helps you do engineering analysis on parts and assemblies in motion.  You can create scripted animations, trace out keypoint paths in 2D or 3D, check interference through a range of motion, and more.

It works by stepping through values you define on variables you select.  The variables can be processed sequentially or in parallel, or a combination of both.

![Main Form](./Media/Form_VarHandler.png)

**Setup**

To get started, open the file in Solid Edge.  Then, prepare your analysis using the following steps and options.  

- ![Reset](./Resources/icons8_replay_16.png)  *Reset*.  Clears the list and reloads variables that have Autotune enabled (see below).

- ![Add](./Resources/icons8_add_16.png)  *Add*.  Opens the Select Variables dialog.  You can choose variables and/or dimensions, either created by the user and/or by the system.  Once selected, variables are presented in a panel for checking/editing.  (For read-only variables, editing is disabled).

![Slider](./Media/UC_Slider.png)

  - *Range*.  Set the desired range by editing the numbers on the track bar.

  - ![Loop](./Resources/icons8_repeat_16.png)  *Loop*.  Cycle continuously from low to high and back. 

  - ![Play](./Resources/icons8_circled_play_16.png)  *Play*.  Click to move to the end of the range.  Click again to move back.

  - ![Settings](./Resources/icons8_settings_16.png)  *Settings*.  Set the number of steps between low and high.

  - ![Autotune](./Resources/icons8_checked_checkbox_16.png)  *Autotune*.  Enable this option to save range information to the file.

  - ![Remove](./Resources/icons8_close_16.png)  *Remove*.  Delete the variable from the form.

  - *Expose name*.  Change the variable's expose name by double-clicking the name at the top of the panel.

  - *Current value*.  Change the variable's current value by dragging the track bar indicator, or editing the value in the displayed formula.

- ![Excel](./Resources/icons8_data_sheet_16_extended.png)  *Export*.  Saves the results to excel for plotting or further analysis.

![Tracker](./Media/UC_Tracker.png)

- ![Tracker](./Resources/icons8_center_of_gravity_16_edited.png)  *Track*.  Enables key point tracing.  For model files, key points are defined with a Coordinate System.  For drawings, they are defined with a Block.  In either case, the item must be named "Tracker".

  - ![Trace](./Resources/icons8_plot_16.png)  *Trace*.  Records the keypoint motion path with a spline curve.  Click to enable.  (It looks like this ![Trace](./Resources/TraceSelected_16.png) when enabled.)

  - *Closed curve*.  Enable this option to create a closed trace curve.

  - ![settings](./Resources/icons8_settings_16.png)  *Settings*.  Set the number of decimal points to use in the report.

  - ![Remove](./Resources/icons8_close_16.png)  *Remove*.  Delete the tracker.

- ![Update](./Resources/icons8_Update_Done_16.png)  *Update*.  Runs the Update Document command after each step.  This is necessary to visualize  changes that occur beneath the top level of the file. 

- ![Save Images](./Resources/SaveAsImage.png)  *Save Images*.  Saves an image at each step.

- ![Check Interference](./Resources/Interference_16.png)  *Check Interference*.  Runs the Check Interference command at each step.

- ![Workflow](./Resources/icons8_workflow_16.png)  *Workflow*.  Opens the Workflow panel (see next).

**WorkFlow**

A workflow is a sequence of "events".  Each event changes each variable from its start value to its end value in a certain number of steps.  

![WorkFlow Form](./Media/UC_Workflow.png)

The variables are those added during setup.  The start value is from the previous step, or from initialization.  The end value is set in the event panel.  (If the start and end are the same, which is common, no change occurs).  The steps parameter sets the number of analysis points for the event.

At each step in each event, any analysis you set up (trace, check interference, etc.) will be performed.

The workflow controls are detailed below.

- ![Open](./Resources/icons8_opened_folder_16.png) *Open*.  Loads a saved workflow. See below for more information on `*.csv` files.
- ![Save](./Resources/icons8_save_16.png) *Save*.  Saves the current workflow to disk.
- ![Close](./Resources/icons8_close_window_16.png) *Close*.  Closes the current workflow.
- ![Add](./Resources/icons8_add_16.png) *Add*.  Adds an event to the current workflow.
- ![Rewind](./Media/video_beginning_16.png) *Go to start*.  Sets the current event to the first event of the workflow.
- ![Play](./Media/video_play_16.png)/![Play](./Media/video_pause_16.png) *Play/Pause*.  Runs the events from the current event to the end.  While playing, it becomes the Pause button.  Note the current event will finish before pausing playback.
- ![End](./Media/video_ending_16.png) *Go to end*.  Sets the current event to the last event in the workflow.
- ![Step](./Media/video_step_16.png) *Step*.  Plays the next event and stops.
- ![Skip](./Resources/icons8-skip-16.png) *Skip*.  Sets the event number where play starts.
- ![Settings](./Resources/icons8_settings_16.png)  *Settings*.  Set the number of steps for the event.  (This is located on each event panel.  It is a per-event control.)

Format for `*.csv` files

The rows in the file contain variables, the columns represent events.  Here is an example.
```
[REF]Notes   Whatever  Whatever  Whatever
[REF]Event   1         2         3
Steps        20        5         20
robot_x      33
robot_y      11.75     20
...
```
Only changes to the variable need to be entered.  The parser will fill in blanks with previous values.  Any line starting with [REF] is ignored.  The `Steps` variable is required.

Example video of a Workflow in action [<ins>here</ins>](https://youtu.be/JcF9NA-WjCA).

**Releases**

All released versions [<ins>here</ins>](https://github.com/farfilli/SolidEdge-VarHandler/releases).

**Release notes**

- 0.1 Very initial and rude one

- 0.2 Better error handling, variable selector and stay on top

- 0.3 Decimal support for initial value, Play button, Loop button, Stay on top, Manual value edit

- 0.4 Support for System and User Variable and Dimensions, export results to excel, settings button

- 0.5 Correctly handled the range values, enabled 2D\3D tracker

- 0.6 Multiple variable selection, tracker widget, and ability to trace the tracker with a spline curve

- 0.7 Update document option, Workflows
  
- 1.0  All unit types supported, save as image and check interference at each step, double-variable values support, workFlow csv import\export, workFlow Step, Stop, Skip controls
	
- (Future release)  Save selected program settings between sessions, cache variables to improve workflow load times, improved consistency/reliability of workflow playback controls, added example videos, 

**Known limits**
- ~~Only user variables supported~~
- ~~Supported units are `mm`, `in`, `degree`, `second`, and `scalar`.  Other units will result in a warning.~~

**Example videos**
- Video in action [<ins>here</ins>](https://www.youtube.com/watch?v=krcpQPdgGos&t=3s&ab_channel=FrancescoArfilli).
- A second video in action [<ins>here</ins>](https://youtu.be/izA-oFQAoVA?si=1aqEMtKb33YCjdMl).
- 2D Tracker tracing a spline [<ins>here</ins>](https://www.youtube.com/watch?v=YH6zwButRlo&ab_channel=FrancescoArfilli).
- 3D Tracker in assembly [<ins>here</ins>](https://youtu.be/T-k3u4ftC2k?si=VSHl7Id2dQuqqkK0).
- Fun robot building the Tower of Hanoi [<ins>here</ins>](https://www.youtube.com/shorts/3aFwVJU6RIc)
- Numerical analysis example [<ins>here</ins>](https://www.youtube.com/watch?v=WG0EQESZJ2A) 

**Example files**

The following links open in GitHub.  To download, click the download button as shown.

![](./Media/github_download.png)

- Example file of the 2D tracker [<ins>here</ins>](./2DVarHandler.zip).
- An example assembly is provided [<ins>here</ins>](./Crane.zip).
- An example interference check is provided [<ins>here</ins>](./Examples/interference.zip).
- An example motor sizing calculation is provided [<ins>here</ins>](./Examples/motor_sizing.zip).

**Example images**

2D Trace

![Trace](./2DTracker.png)

Assembly

![Crane](./Crane.png)

Populated variable table

![Variable table](./VarTable.png)

Exported results in excel after processing

![export](./export.png)

