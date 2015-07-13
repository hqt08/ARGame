# ARGame
An Augmented Reality Game with an interplay of both virtual and real world elements, where you depend on finding the right colored objects to reach your objective, for iOS (iPad) using MetaioSDK.

## Downloads ##
1) Due to github file size limits, please download metaiosdk iOS plugin and add it to the plugins folder under the Unity assets.
http://dev.metaio.com/sdk/getting-started/
2) Also download the usual metaioman tracker image
http://dev.metaio.com/fileadmin/user_upload/documents/sdk/markerless.pdf

## Setup from XCode Build ##
1) Open the compiled .xcodeproject
2) Go to Targets > Build Phases > Link Binary With Libraries
3) Drag and Drop metaiosdk to the list
4) If not already setup, also add libc++.dylib, libxml2.2.dylib, Security.framework and CoreImage.framework
5) Click play and the app should run! You will need to use the tracker image in the game.

## Setup from Unity ##
1) Download the source code
2) Go to Build Settings, select iOS
3) Go to Player Settings > Other Settings > Graphics APIs, ensure only OpenGLES2 is used (not Metal)
4) Also so set Target Device to iPad only and Architecture as ARMv7
5) Click Build in Build Settings and this should generate a XCode project
6) Follow the above steps in "Setup from XCode Build"
