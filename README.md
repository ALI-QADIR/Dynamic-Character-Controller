# Dynamic Character Controller

## Project Description:

The Dynamic Character Controller is a versatile tool for action RPG game developers built in Unity 3D. It utilizes Dynamic Rigidbody, a powerful physics engine, and the new Input System to create a seamless player experience. The controller also employs Cinemachine and Blend Trees for fluid animation transitions.

One of the most notable features of this controller is its complete customizability. Developers have the ability to remap the keys and switch out the character model and animations to suit their specific needs. The included model and animations are downloaded from Mixamo, but developers are welcome to use their own.

> **Note**: Please note that this project is a work in progress and still in the alpha version. As such, there are some known issues that will be addressed in future updates. But with its range of features and customizability, the Dynamic Character Controller is an excellent starting point for any action RPG game developer.

## Features:

The character controller is designed for action RPGs and comes with the following features:

-   The character can walk, run, jump, look around, dodge, and block in all directions.
-   The camera is a freelook camera that follows the character.
-   The player prioritizes block over all other controls (except dodge and look around).
-   The player prioritizes dodge over all other controls (except look around).

## Current Mapped Keys:

The character controller comes with pre-mapped keys. However, you can remap the keys using the new Input System in Unity(For more details please refer to "Usage Istructions"). The pre-mapped keys are as follows:

-   Move around using the W, A, S, and D keys.
-   Look around using the mouse.
-   Jump using the Space key.
-   Walk slowly by holding down the Left Shift key.
-   Block using the Left Control key.
-   Dodge using the Left Alt key and a directional key.

> **Note**: If you encounter any issues during usage, please refer to the "Known Issues" section of this README file.

## Installation Instructions:

Below are the install instructions.

1. Make sure you have Unity 2021.3.18f1 installed, as this is the version the project was built in (latest version as of March 2023).
2. Install the new Input System version 1.4.4 and Cinemachine version 2.8.9 from the Unity Package Manager.
3. Download the complete project native unity package (.unitypackage) from my itch.io page by following these steps:
    1. Go to [my itch](https://aliqadir.itch.io/dynamic-character-controller "Itch Link").
    2. Click on the download button for the complete project package.
    3. Save the file to your computer.
    4. In Unity, go to Assets -> Import Package -> Custom Package.
    5. Select the downloaded .unitypackage file and click Import.
4. Alternatively, If you prefer, you can also clone the repository directly into your Unity project by following these steps:
    1. Open your Unity project and navigate to the Assets folder.
    2. Right-click on the Assets folder and select "Show in Explorer" (Windows) or "Show in Finder" (Mac).
    3. Click the green "Code" button at the top of the repository page.
    4. Select "Clone with HTTPS".
    5. Copy the URL that appears.
    6. In the Explorer/Finder window, right-click and select "Git Bash Here" (Windows) or "Open Terminal" (Mac).
    7. In the Git Bash/Terminal window, type "git clone" followed by a space, and then paste the URL you copied earlier.
    8. Press Enter to clone the repository into your Unity project folder.

## Usage Instructions:

Go to the player prefab. In the Player Locomotion script, make sure you have chosen the appropriate ground layer for your project as the character uses layers for ground check functionality.

1. Remap the keys as you like using the new input system:
    1. Inside the Unity Editor, browse to the directory Assets > Input System.
    2. Double click on the PlayerControls (not the C# script).
    3. Map the controls as you like.
2. Change the Character Model:
    1. Inside the Unity Editor, browse to the directory Assets > Prefabs and drag the player prefab into the scene.
    2. In the Hierarchy, expand the player prefab and delete/disable Knight D Pelegrini.
    3. Add your rigged character model. If you are using the given animations, rig the model using Mixamo.
    4. Add the Animator component onto your character model.
    5. Click the player game object, expand the Animation Manager script component, and drag and drop the character model from the hierarchy onto the Animator field in the component.
3. Change the Animations:
    1. Inside the Unity Editor, browse to Assets > Art > Animations and double click on the Humanoid Animator controller. This will open the Animator tab.
    2. Change the animations as you like. Note: The character animations are controlled using the blend trees.
        > **Note**: This project is a work in progress and has some known issues mentioned in the project description. This is an alpha version of the character controller. As such, the animations may not be appropriate for all situations. For example, the dodge animation may not be appropriate since it was not found on Mixamo. However, the animations are a good starting point for any action RPG game developer.

> **Note**: If you encounter any issues with the installation process or during usage, please refer to the "Known Issues" section of this README file or contact me for assistance.

## Issues:

### Fixed Issues:

Resolved issues which are not yet published in the latest release.

-   Fixed the issue where the character would not jump when on slopes.
-   Fixed the issue where the jump animation would play when the character was running/walking a slope.

### Known Issues:

These are some of the known issues. If you find any more issues, feel free to report.

-   Occasionally, the character collider may become stuck in walls.
-   The character jumps when you stop running on a slope.
-   The character jumps down slopes rather than running, no issue when you are walking down the slope slowly.
-   The dodge animation may not be appropriate since it was not found on Mixamo.

## Future updates:

These are the planned future updates. (I am currently working on som other project and hence the fututre updates will be a little late).

-   Add logic so that the player could walk up/down stairs.
-   Implement attack combinations that allow for up to three attacks in sequence.
-   Fix any known issues that arise during development and testing.
-   Introduce health, hurt, dodge, and block logic to enhance the character's gameplay experience.
-   Add attack logic to enable the character to attack enemies.

## Contributing guidelines:

If you wish to contribute to the project, please follow the general contribution guidelines as outlined below:

-   Fork the repository and create a branch from the main branch
-   Make your contributions and commit your changes to your branch
-   Submit a pull request detailing the changes made
-   Wait for your changes to be reviewed and merged

## License information:

This project is licensed under the MIT license, which can be found on the GitHub repository. The MIT license allows for the use, modification, and distribution of the code for both commercial and non-commercial purposes.

## Contact information:

For any bug reports, issues, or help regarding the setup of the project, please feel free to contact me on [LinkedIn](https://www.linkedin.com/in/ali-qadir-1509b1226/ "LinkedIn Profile"), Instagram [@oily.oli](https://www.instagram.com/oily.oli/ "Insta @oily.oli"), or mail me at [ali.qadir.007@outlook.com](mailto:ali.qadir.007@outlook.com?subject=[GitHub]%20Dynamic%20Character%20Controller%20Issue "Mail to Ali Qadir"). My contact information can be found on my GitHub profile also.
Alternatively you can leave comments on my [itch page](https://aliqadir.itch.io/dynamic-character-controller "Itch Link") too.
