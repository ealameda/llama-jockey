Create a path in virtual reality for 3D objects to move along. Use your hands to grab objects to place along the path. Try out this Unity project to experiment with virtual reality experiences using the Leap Motion controller and an Oculus Rift or HTC Vive.

##Features include:   

* Lay waypoints to create a path
  - Pinch to create a waypoint.
* Grab and move 3D objects with your hands.
* Place an object along a path
  - Object placed along the path will move continuously along the path from the first waypoint to the last and back again.
* Voice chat with LipSync
  - Wear headphones.
  - Watch your friend's avatar's mouth move as they talk.
* Avatar  
   - Hand tracking using Leap Motion.
* Multiplayer networking (Note: not all features currently support multiplayer)
    - Handled by Photon Unity Networking. See below to set this up before running.
* High Five

##Hardware requirements:
* Oculus or HTC Vive
* Leap Motion sensor (mounted on front of headset)
* Unity 5.4
* Minimum system requirements: 
  - Windows 10 PC
  - Intel Core i5- 4590 
  - 8 GB RAM
  - Geforce GTX 970 or greater
  - 3x USB 3.0
* Make sure your graphics card, oculus, htc vive and leap motion drivers and firmware are all up to date.


##Photon Network setup before running app:
Before running the project, you must add your own Photon IDs to the PhotonServerSettings.asset. Select this file from the project directory Photon Unity Networking > Resources, and then enter your IDs in the inspector (be sure Photon Cloud is selected for Hosting).

Follow these instructions to obtain your IDs: 
* Photon App ID 
  - [https://doc.photonengine.com/en/realtime/current/getting-started/obtain-your-app-id] (https://doc.photonengine.com/en/realtime/current/getting-started/obtain-your-app-id)
* Photon Voice ID
  - Logged into your Photon account, click in the top right corner on Dashboard > Voice.
  - Select "Create New Vocie App" and enter any information.

##Notes:
* After adding your Photon App IDs, open the 'main' scene and run the project by hitting play in the editor. 
* Build executables are only currently working for Oculus, not the Vive.
* LipSync is currently only working properly for two players. 
