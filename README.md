# SolidEdge-VarHandler

Inspired by a post on Siemens Community I decided to create this control center to easily evaluate variables range values

The actual version is limited and will perhaps be expanded per user requests


**Feature list:**
- Reload button ![Autotune](./Resources/icons8_replay_16.png)
- Add any system or user variables and dimensions ![Add](./Resources/icons8_add_16.png)
- Export results to excel when play a variable ![Excel](./Resources/icons8_data_sheet_16_extended.png)
- Point tracker, return the coordinate of a 3D coordinate system or a 2D block ![Tracker](./Resources/icons8_center_of_gravity_16_edited.png)
  - Trace button, it creates a spline curve when playing a variable ![Trace](./Resources/icons8_plot_16.png)
  - Closed curve, the spline will be a closed curve
  - Settings button for shown decimals ![settings](./Resources/icons8_settings_16.png)
  - Remove button ![Autotune](./Resources/icons8_close_16.png)
- Exposed name retrieved from the variable table (if available)
- Ability to vary Exposed name from the variable table (double click on the title to prompt)
- Minimum and Maximum range retrieved from the variable table (if available)
- Ability to vary Minimum and Maximum range from the user interface (click on values to prompt)
- Loop button, the trackbar will move from one end to the other and reverse continuously ![Autotune](./Resources/icons8_repeat_16.png)
- Play button, the trackbar will move to the end ![Autotune](./Resources/icons8_circled_play_16.png)
- Settings button to select the number of steps to perform on play ![settings](./Resources/icons8_settings_16.png)
- Checkbox to auto-retrieve the variable on reload (**Autotune** on comments field) ![Autotune](./Resources/icons8_checked_checkbox_16.png)
- Remove button ![Autotune](./Resources/icons8_close_16.png)
- Taskbar to easily change values
- Manuale vary the value (click on value to prompt)
- ReadOnly variables supported
- Works on any Solid Edge environment (par, psm, asm, dft)


Release versions [here](https://github.com/farfilli/SolidEdge-VarHandler/releases):
- 0.1 Very initial and rude one
- 0.2 Better error handling, variable selector and stay on top
- 0.3 Decimal support for initial value, Play button, Loop button, Stay on top, Manual value edit
- 0.4 Support for System and User Variable and Dimensions, export results to excel, settings button
- 0.5 Correctly handled the range values, enabled 2D\3D tracker
- 0.6 Multiple variable selection, tracker widget, and ability to trace the tracker with a spline curve
  
**Known limits:**
- ~~Only user variables supported~~
- mm, degree, and scalar units supported, other units will result in unpredicted values

**Videos**
- Video in action [here](https://www.youtube.com/watch?v=krcpQPdgGos&t=3s&ab_channel=FrancescoArfilli)
- New video [here](https://www.youtube.com/watch?v=krcpQPdgGos&t=3s&ab_channel=FrancescoArfilli)
- 2D Tracker tracing a spline [trace](https://www.youtube.com/watch?v=YH6zwButRlo&ab_channel=FrancescoArfilli)
- 3D Tracker in assembly [here](https://youtu.be/T-k3u4ftC2k?si=VSHl7Id2dQuqqkK0)

**Example files**
- Example file of the tracker [here](./2DVarHandler.zip)
- An example assembly is provided [here](./Crane.zip)

**User UI:**

![MainForm](./MainForm.png)
![Variable selector](./VarSelector.png)
![Trace](./2DTracker.png)
![Crane](./Crane.png)
![Variable table](./VarTable.png)

Exported results in excel after play
![export](./export.png)
