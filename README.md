# SpyPoint Data application

A desktop application for managing SpyPoint account(s) and pictures.

## Installation

- Download the repository from github https://github.com/jbhunter52/SpypointData/archive/refs/heads/master.zip.
 Unzip the file, it will contain the source code and the latest compiled version in the Build folder. Make a shortcut to SpyPointData.exe or create a shortcut on your desktop to launch.

## Usage

- Click File...Edit Logins.  Click File...Add Login to add a new account with a username and password.

- All user information & pictures will be saved to the local drive only at C:\Users\USERID\AppData\Local\Spypoint.

- When closing the application all modifications will be saved. A save can be triggered manually from the menubar using File...Save.

- Upon loading the application or adding a new account login click File...Merge From Server to grab the latest photos from the server.  Upon completion the progress windows will read "Done".  Close the progress window.  The picture filter will be set to "New".  To view all pictures unselect "New" filter from the menubar using Filter...New.

- Pictures can be tagged using hotkeys while a picture is being viewed and pressing "d" for doe, "b" for buck.  Age can be tagged using hotkeys (1,2,3,4).

- Each picture has a location name tagged.  When a picture is downloaded from the server the default location name is the current name of the camera that took the photo in the SpyPoint app.  This can be modified by either selecting one picture (or more than one picture while holding the Shift key), and selection from the menubar Setting...Change Camera Name.

- Each camera and picture has a GPS location tagged. When the GPS of a camera is set in this application all photos downloaded from the server under this camera will inherit the camera GPS location.  To modify either a picture or camera GPS location select from the menubar Setting...Set Location Coordinates.  When the map is loaded use the mouse wheel to zoom in/out or pan by holding down the right mouse button.  The location is marked by the red crosshairs.  When the map is exited using the top-right X, the location will be tagged to the picture or camera.

- To label pictures with a BuckID, first open the BuckID window with View...BuckID.  To add a new BuckID name right-click in the tree view on the left at the bottom of the tree and select Add. BuckID's can also be renamed or deleted here by right-clicking on the BuckID and selecting rename/delete.  After modification the BuckID window can be closed.  After BuckID's are added, they are visible in the main window just above the top left of the map.  Single or multiple pictures can be selected and tagged with a BuckID from the drop-down selection.

- Using the menubar selection View...Camera Details a table of all cameras for all accounts will be displayed including infomation including name, model, battery %, signal, plan name, pictures left, days left, auto renew, multishot & delay settings, etc.  The page is read-only.

## Advanced Usage

- After pulling an sd card from a camera you can upload the "HD" pictures to keep record with better photo resolution. Current pictures and tags will be merged with the HD version from the sd card.  Insert the sd card in the computer, select in the tree menu the camera that took the pictures that will be uploaded, select File...Import Card Pics from the menubar, select a folder(s) that contain the pictures.
IMPORTANT!!! If you pick the wrong camera the original picture will not be found for merging, instead a duplicate in the selected camera will be created.
IMPORTANT!!! Make sure the camera pictures are updated from the server before merging sd card pictures. If not duplicates will be created when pictures are downloaded form the server.

- Manual pictures can be added that are not tied to SpyPoint cameras.  This could include pictures from a non-SpyPoint trail camera or any other observation pictures.  Pictures will be loaded in a ManualPics group, not tied to any specific camera.  Only jpeg/jpg pictures are supported.

- Export pictures.  Export picture(s) selected to a zip file.  Select pictures in the tree view and from the menubar select File...Export Pictures.  You will be prompted for save to name and location.
