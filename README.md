## Mutli-projector System

This is the projection system designed to be used in conjunction with IBM Quantum Flatworld, it builds heavily on Damian Ziaber's Space Unfolded work as well as UCL's OpenIllumiroom project, more information on these projects can be found in the links below:

https://github.com/lomqer/SpaceUnfolded

https://students.cs.ucl.ac.uk/2022/group33/#:~:text=UCL%20Open%2DIllumiroom%20V2%20displays,possibilities%20for%20creating%20immersive%20experiences.

It is currently only able to be run on MS Windows systems.

To use the system make sure you have installed all of the required packages as identified in requirements.txt. Please note that to use the experimental point cloud feature (menu option 4) you need to install Intel MiDaS in the root directory with the dpt_beit_large_512 model, the required files and installation instructions can be found here: https://github.com/isl-org/MiDaS.

Projector Calibration and Display Mirroring instructions:

To run the program, either run the main.py file in your chosen IDE or from the command line with:
```shell
python main.py
```
Once running, the main menu is navigated with the options presented on screen:

![](/figures/User_interface.JPG)

For first time setup, projector calibration (option 1) must be run with a connected webcam. Please note that this is currently only working with a maximum of two projectors. In your MS Windows Display settings, make sure that the option to extend your desktop is selected.
The projector projecting the left hand image must be set to display 2, and the projector projecting the right hand image must be set to display 3. The window that you want to mirror to the projectors must be set as display 1 forming a panorama from left to right with:

Display_to_be_mirrored (1) --> Projector_showing_left_half (2) --> Projector_showing_right_half (3).

Projector setup should resemble the diagram shown below:

![](/figures/hardware.png)

Alternatively the projectors can be setup as shown below or into the vertices of the room but please note that this type of setup remains untested and may produce unusual results:

![](/figures/projector_setup.png)

Ensure that the camera is able to see the entire projection area before calbration is run and ensure that the projectors are overalapping by approximately 5%. Once the projection calibration is running, do not block the camera or projectors at any point.

The gray code calibration sequence will run consecutively on each projector. The calibration process must run from the projector showing the left half of the image first (as you are looking at the projection display). If this is not the case, please make sure the displays are being extended correctly as specified in the instructions given above.

After each gray code sequence you will be prompted to draw a box setting the 'display contour'. This corresponds to the final calibrated output area of each projector. Ensure the display contours for each projector overlap to produce a cohesive image. Unfortunely at present you are unable to view the other projector's display contour when this is being selected, potentially making calibration a bit tedious and may need to be repeated several times to get a good output, press enter once the contour has been selected:

![](/figures/roi.jpg)

The display contour MUST be selected within the raw output of the projector as is represented by the projected white frame. Failure to keep the selected contour within the raw output will result in either an exception being thrown or poor calibration performance.

The entire calibration process can take several minutes and the resulting calibration maps can also take several minutes to save. When the process is complete you will be directed back to the main menu. You will only be required to re-calibrate the projectors if the hardware setup changes or moves. After calibration is complete the camera is not needed for display mirroring and can be disconnected. The camera is required for further projection calibration or if running the experimental camera calibration (menu option 3) and point cloud generation (menu option 4).

To mirror your screen to the calibrated projectors, select option 2. Please note that everything on your main display will be mirrored on the projectors.
To give a good cohesive image, the projectors may need to be manually adjusted VERY slightly for alignment; excessive movement will invalidate the calibration, keep alignment adjustments as minimal as possible (within a few centimetres). It is easiest to tell if the projectors are aligned properly by displaying text or an image on screen and looking at the overlapping region to determine if they are aligning as expected.

Examples of the aligned output are shown below:

![](/figures/rainbow.jpg)

![](/figures/flatworld.jpg)

To exit the display mirroring, you must move your mouse off of your screen to the right and click on the mirror window and press the escape key.

