# Track_And_Field

A recreation of the classic arcade game Track and Field, originally released in 1983. This project, developed from scratch, marks my inaugural journey in game development using Unity.

<img src ="https://github.com/Npelletier91/Track_And_Field/assets/129113700/d13ea3ab-4d3c-4060-ba67-18a39135f012" width="400" height ="250">

## Overview

This Unity-based game replicates the essence of the original Track and Field game. Players engage in a race where they control the character's movements and actions to achieve the highest score.

## Controls
[Player Script Files in Github](https://github.com/Npelletier91/Track_And_Field/blob/main/Assets/Track%20and%20Field/Scripts/Player100MeterDash.cs)

The game is controlled using the following keyboard inputs:

- **Left/Right Arrow**: Alternate between pressing these arrows to simulate running.
- **Up Arrow**: Jump command.


Example Code:
```csharp
if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                audiosource.PlayOneShot(Running, .8f);

                currentSpeed += adjustedAcceleration * Time.deltaTime;
                currentSpeed = Mathf.Min(currentSpeed, maxSpeed);

Vector2 newVelocity = new Vector2(currentSpeed, myRigidbody2D.velocity.y);
            myRigidbody2D.velocity = newVelocity;
```

## Scoring System

The scoring system is based on speed and time taken to finish the race. Here's an example calculation:

Example Code:
```csharp
float score = 5000 - (timer * 150);
previousScore = score;

scoreText.text = score.ToString("0000");
```

## Player Animation
Utilized Unity's animation triggers to enhance gameplay experience, incorporating sound effects and collision interactions.

## Design Elements
The background design draws inspiration from the aesthetics of the original game. Animated elements, such as cheering audience members, add depth to the visual experience.

## How to Run

Clone Repository: Clone this repository to your local machine.
Open with Unity: Open the project using Unity.
Run the Game: Launch the game from the Unity editor or build the game for your platform of choice.
