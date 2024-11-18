![image_002_0001](https://github.com/user-attachments/assets/ee17a28f-8d2b-439c-b850-c393dc160405)

# Cross Hatching Effect Post Processing
![Unity Version](https://img.shields.io/badge/Unity-2021.3%36LTS%2B-blueviolet?logo=unity)
![Unity Pipeline Support (Built-In)](https://img.shields.io/badge/BiRP_❌-darkgreen?logo=unity)
![Unity Pipeline Support (URP)](https://img.shields.io/badge/URP_✔️-blue?logo=unity)
![Unity Pipeline Support (HDRP)](https://img.shields.io/badge/HDRP_❌-darkred?logo=unity)
 
A hatching/cross-hatching post processing effect for Unity URP (2022.3.20f1) that I made for Serious Point Games as part of my studies in shader development.
You can refer to the effect's documentation for more info (should be in the repo and its release as a PDF file).

## Features
- Animated Hatchings
- Ability to be displayed with screen color or black & white
- Inverted hatching color
- Texture array for Hatch textures

## Example[s]
![image_001_0001](https://github.com/user-attachments/assets/59c9f9ca-150b-4724-b6aa-4625b542aa7d)
Hatching Effect (normal)

![image_002_0001](https://github.com/user-attachments/assets/ee17a28f-8d2b-439c-b850-c393dc160405)
Black & White

![image_004_0001](https://github.com/user-attachments/assets/7cb55e27-8094-49f0-913d-c8059580805c)
Inverted Hatching Color

## Installation
1. Clone repo or download the folder and load it into an unity project.
2. Create a material with the effect's shader graph, or use the provided one.
3. Add the render feature of the hatching effect to the Universal Renderer Data you are using.
4. Input the effect material (from 2.) into the material field in the effect's renderer feature.
5. If needed, you can change the effect's render pass event in its render feature under settings.

## Credits/Assets used
The code is based on IronStar Interactive's image effect shader. [GitHub Link](https://github.com/BattleDawnNZ/Image-Effects-with-Shadergraph) or [YouTube Video Link](https://www.youtube.com/watch?v=FpvJAG6R99k&t=6s)
<br>
<br>
Texture Array script by Dmitry Denisov. [GitHub Link](https://github.com/DmtDenisov/Texture-Array-Unity). It comes included with the project but you don't have to use or depend on it, you can 
make and use your own (or other ones). I only include this one as its the one that is most convenient for me and the one that I reference in the effect's documentation.

