CODE CHALLENGE
--------------------
Write a Windows app that lists all the drives and logical volumes on the user's system. Upon clicking a button, the app should search through all the files on the selected drive/volume and find all directories that directly contain individual files larger than 10 MB. Processing should be done in multiple threads in parallel to increase performance. It should be possible to pause and resume the search by pressing a button.

Any discovered directory should be immediately shown in a separate result list in the main window while the search is still running. The app should also display two numbers for each found directory: the count of all files (of any size) in the directory and its sub-directories, as well as the combined size of these files.

The app should monitor all listed result directories and their subdirectories for changes in file count or file sizes. Monitoring should start immediately when the result is added to the list and continue after the search has finished. The file count and file size sum in the result list should be updated immediately whenever these numbers change. The result list entry should be removed when the directory is deleted.

The app should remain responsive at all times. Any changes in the data or the result list must not cause any flickering or disruptions to the layout.

Technical requirements:

Programming language: Use C# or C++.

UI framework: Free to choose. You may incorporate third-party UI component libraries, as long as they adhere to the third-party library restrictions mentioned below..

Platform: The application must run on Windows 11. It would be advantageous to have compatibility with earlier versions of Windows.

Third-party libraries: Feel free to use any open-source libraries that are helpful. If you employ third-party libraries, include them directly in the project folder to ensure that the project can be built without relying on external dependencies.

IDE / Project files: The project must be buildable using Microsoft Visual Studio, Visual Studio Code, or any of the tools provided by JetBrains (CLion, Rider, Fleet).