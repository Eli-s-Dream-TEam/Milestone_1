
# FlightSimulator


## Main objectives:
1. Using .Net Framework to create a GUI application for flight simulation.
2. Implementing the project using mvvm architecture.
3. Implementing a TCP client to send data to a third-party simulation app - "FlightGear".
## Summary
"FlightSimulator" is a tool for analyzing and researching a given flight report using two displays:
- Simulation display - displaying animation through FlightGear.
- Analyzing display - displaying updating information about different kinds of flight parameters according to the user's choice.
## Application Features:
1. Controllable timeline.
2. Adjustable animation Speed.
3. Altitude gauge.
4. Speed gauge.
5. Flight's direction.
6. Plane Control Wheel (Yoke).
7. Principal axes (Yaw,Pitch, Roll)
8. Selection of a specific flight parameter for research.
9. Updating Data About The Selected Flight Parameter:
	- It's value.
	- The most correlated flight parameter value.
	- Regression line of the two parameters. (the other being the correlated parameter)
	- last thirty seconds values of the two parameters.
10. Anomaly detection algorithm chosen by the user.
11. Anomalies information (when, where, which parameters)

![enter image description here](https://northeurope1-mediap.svc.ms/transform/thumbnail?provider=spo&inputFormat=jpg&cs=fFNQTw&docid=https://livebiuac-my.sharepoint.com:443/_api/v2.0/drives/b!vy-rPJF01kiLaNkWI4rTnqA-AxYsmFRCql_QXMSgueuCnP9vWXdIQ6sO0_yzvyal/items/01Y7Q5WH5ZIT7MW5MYORFYEBVLH4VJLLI4?version=Published&access_token=eyJ0eXAiOiJKV1QiLCJhbGciOiJub25lIn0.eyJhdWQiOiIwMDAwMDAwMy0wMDAwLTBmZjEtY2UwMC0wMDAwMDAwMDAwMDAvbGl2ZWJpdWFjLW15LnNoYXJlcG9pbnQuY29tQGY0YmFkN2VhLWQyMDMtNDMzMy05ZjM4LTRmNDk5YzZjNmY2ZSIsImlzcyI6IjAwMDAwMDAzLTAwMDAtMGZmMS1jZTAwLTAwMDAwMDAwMDAwMCIsIm5iZiI6IjE2MTg0MTI0MDAiLCJleHAiOiIxNjE4NDM0MDAwIiwiZW5kcG9pbnR1cmwiOiJ5RWhaeXNQTXRON0FUWnhQZC9ubXV2YUlRSGhnY1dKL1JwSUZUaXpQMmc0PSIsImVuZHBvaW50dXJsTGVuZ3RoIjoiMTE5IiwiaXNsb29wYmFjayI6IlRydWUiLCJ2ZXIiOiJoYXNoZWRwcm9vZnRva2VuIiwic2l0ZWlkIjoiTTJOaFlqSm1ZbVl0TnpRNU1TMDBPR1EyTFRoaU5qZ3RaRGt4TmpJek9HRmtNemxsIiwic2lnbmluX3N0YXRlIjoiW1wia21zaVwiXSIsIm5hbWVpZCI6IjAjLmZ8bWVtYmVyc2hpcHx5YXJvbi5ob3Jza3lAbGl2ZS5iaXUuYWMuaWwiLCJuaWkiOiJtaWNyb3NvZnQuc2hhcmVwb2ludCIsImlzdXNlciI6InRydWUiLCJjYWNoZWtleSI6IjBoLmZ8bWVtYmVyc2hpcHwxMDAzMjAwMDY5N2RjYWYwQGxpdmUuY29tIiwidHQiOiIwIiwidXNlUGVyc2lzdGVudENvb2tpZSI6IjMifQ.VzVuZloveDhxelBNckpubXJDRjJEaVVEaDAvOHNFUmdNbFNnNk9GMnlpaz0&encodeFailures=1&width=2561&height=1238&srcWidth=&srcHeight=)



## Folder Structure
```
.Milestone_1
├── FlightSimulator               
|   ├── Helper          	      # Calculation and parsing classes.
|   ├── Model         		      # Model part of the mvvm architecture.
|   |   ├── DataModel.cs	      # The central model of the UI
|   |   ├── DLLModel.cs		      # Model of the anomalies detection algorithms.
|   |   ├── SocketModel.cs	      # Model for the communication with FlightGear.
|   ├── Properties     		      # Generated setting for the project.
|   ├── View         		      # View part of the mvvm - different displays in the project.
|   |   ├── ...			      # The different components of the view part(time bar, graphs, gauges etc..) of the mvvm.
|   ├── ViewModel         	      # ViewModel for each of the different views in the project. 
|   |   ├── ...			      # Corresponding view model for each of the different views.
|   ├── public         		      # icons and pictures used.
|   ├── packages.config		      # config file for needed packages.
|   ├── App.xml			      # Settings for some of the view components.
├── packages		              # Packages used in the project. 
├── plugins			      # Folder for the anomaly detection algorithms (dll files).

```

## Requirements
- Visual Studio 2019 installed.
- FlightFear 2020.3.8 (For windows 7,8,10)
- Windows
## Compiling and Running
1. Clone/download this repo.
2. open "FlightSimulator.sln" in Visual studio and build the project. (This will download the required packages - specified in "packages.config" file, see above in "Folder Structure").
3. Download [FlightGear](https://sourceforge.net/projects/flightgear/files/release-2020.3/FlightGear-3020.3.8.exe/download)
4. Configure FlightGear setting as shown below. (see the highlight section)
5. Click on the "fly" Icon to run FlightGear with the correct setting you've set in the previous step.
6. Run the FlightSimulator app (from visual studio or the .exe file in FlightSimulator/bin/Debug/FlightSimulator.exe)

![enter image description here](https://northeurope1-mediap.svc.ms/transform/thumbnail?provider=spo&inputFormat=jpg&cs=fFNQTw&docid=https://livebiuac-my.sharepoint.com:443/_api/v2.0/drives/b!vy-rPJF01kiLaNkWI4rTnqA-AxYsmFRCql_QXMSgueuCnP9vWXdIQ6sO0_yzvyal/items/01Y7Q5WHYDGIOPPHXEHVFZCDF4OHBPIA3X?version=Published&access_token=eyJ0eXAiOiJKV1QiLCJhbGciOiJub25lIn0.eyJhdWQiOiIwMDAwMDAwMy0wMDAwLTBmZjEtY2UwMC0wMDAwMDAwMDAwMDAvbGl2ZWJpdWFjLW15LnNoYXJlcG9pbnQuY29tQGY0YmFkN2VhLWQyMDMtNDMzMy05ZjM4LTRmNDk5YzZjNmY2ZSIsImlzcyI6IjAwMDAwMDAzLTAwMDAtMGZmMS1jZTAwLTAwMDAwMDAwMDAwMCIsIm5iZiI6IjE2MTg0MTI0MDAiLCJleHAiOiIxNjE4NDM0MDAwIiwiZW5kcG9pbnR1cmwiOiJ5RWhaeXNQTXRON0FUWnhQZC9ubXV2YUlRSGhnY1dKL1JwSUZUaXpQMmc0PSIsImVuZHBvaW50dXJsTGVuZ3RoIjoiMTE5IiwiaXNsb29wYmFjayI6IlRydWUiLCJ2ZXIiOiJoYXNoZWRwcm9vZnRva2VuIiwic2l0ZWlkIjoiTTJOaFlqSm1ZbVl0TnpRNU1TMDBPR1EyTFRoaU5qZ3RaRGt4TmpJek9HRmtNemxsIiwic2lnbmluX3N0YXRlIjoiW1wia21zaVwiXSIsIm5hbWVpZCI6IjAjLmZ8bWVtYmVyc2hpcHx5YXJvbi5ob3Jza3lAbGl2ZS5iaXUuYWMuaWwiLCJuaWkiOiJtaWNyb3NvZnQuc2hhcmVwb2ludCIsImlzdXNlciI6InRydWUiLCJjYWNoZWtleSI6IjBoLmZ8bWVtYmVyc2hpcHwxMDAzMjAwMDY5N2RjYWYwQGxpdmUuY29tIiwidHQiOiIwIiwidXNlUGVyc2lzdGVudENvb2tpZSI6IjMifQ.VzVuZloveDhxelBNckpubXJDRjJEaVVEaDAvOHNFUmdNbFNnNk9GMnlpaz0&encodeFailures=1&width=612&height=677&srcWidth=612&srcHeight=677)

## Additional Links
- [Project's UML  diagram.](https://github.com/Eli-s-Dream-TEam/Milestone_1/blob/main/UMLDiagram.pdf)
- [Short instructional video about the project.](https://www.youtube.com/watch?v=OkbNiaYKxJ4)
