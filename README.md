# FlightSimulator



## Main objectives:

1. Using .Net Framework to create a GUI application for flight simulation.
 2. Implementing the project using mvvm architecture.
 3. Implementing a TCP client to send data to third-party simulation app - FlightGear. 

## Summary
"FlighSimulator" is a tool for analyzing and researching a given flight report using two displays:
- Simulation display - displaying animation through the "FlightGear" app.
- Analyzing display - displaying updating information about different kind of flight paramaters according to the user's choice.

## Application Features: 

	1. Controlable timeline.
	2. Adjustable animation Speed.
	3. Altitude gauge.
	4. Speed gauge.
	5. Flight's direction.
	6. Plane control Wheel (Yoke).
	7. Principal axes (Yaw,Pitch,Roll)
	8. Selection of a spesific flight paramater for reasearch.
	9. Updating Data About The Selected Flight Paramater:
		- It's value.
		- It's most correleated flight paramater value.
		- Regression line of the two paramaters. (the other being the correlated paramater)
		- last thirty seconds values of the two paramaters.
	10. Anomaly detection algorithm choosed by user.
	11. Anomalies information (when, where, which paramaters)

![enter image description here](https://northeurope1-mediap.svc.ms/transform/thumbnail?provider=spo&inputFormat=jpg&cs=fFNQTw&docid=https://livebiuac-my.sharepoint.com:443/_api/v2.0/drives/b!vy-rPJF01kiLaNkWI4rTnqA-AxYsmFRCql_QXMSgueuCnP9vWXdIQ6sO0_yzvyal/items/01Y7Q5WH4U5DILPF3TCZDYMMVQ76BSRW4P?version=Published&access_token=eyJ0eXAiOiJKV1QiLCJhbGciOiJub25lIn0.eyJhdWQiOiIwMDAwMDAwMy0wMDAwLTBmZjEtY2UwMC0wMDAwMDAwMDAwMDAvbGl2ZWJpdWFjLW15LnNoYXJlcG9pbnQuY29tQGY0YmFkN2VhLWQyMDMtNDMzMy05ZjM4LTRmNDk5YzZjNmY2ZSIsImlzcyI6IjAwMDAwMDAzLTAwMDAtMGZmMS1jZTAwLTAwMDAwMDAwMDAwMCIsIm5iZiI6IjE2MTgzMDQ0MDAiLCJleHAiOiIxNjE4MzI2MDAwIiwiZW5kcG9pbnR1cmwiOiJ5RWhaeXNQTXRON0FUWnhQZC9ubXV2YUlRSGhnY1dKL1JwSUZUaXpQMmc0PSIsImVuZHBvaW50dXJsTGVuZ3RoIjoiMTE5IiwiaXNsb29wYmFjayI6IlRydWUiLCJ2ZXIiOiJoYXNoZWRwcm9vZnRva2VuIiwic2l0ZWlkIjoiTTJOaFlqSm1ZbVl0TnpRNU1TMDBPR1EyTFRoaU5qZ3RaRGt4TmpJek9HRmtNemxsIiwic2lnbmluX3N0YXRlIjoiW1wia21zaVwiXSIsIm5hbWVpZCI6IjAjLmZ8bWVtYmVyc2hpcHx5YXJvbi5ob3Jza3lAbGl2ZS5iaXUuYWMuaWwiLCJuaWkiOiJtaWNyb3NvZnQuc2hhcmVwb2ludCIsImlzdXNlciI6InRydWUiLCJjYWNoZWtleSI6IjBoLmZ8bWVtYmVyc2hpcHwxMDAzMjAwMDY5N2RjYWYwQGxpdmUuY29tIiwidHQiOiIwIiwidXNlUGVyc2lzdGVudENvb2tpZSI6IjMifQ.blpvTjNEQnloUEc5K3pvQzdnVG40QTVpdFBnTWpDUHg0OWhKYUtoSDU1cz0&encodeFailures=1&width=2561&height=1238&srcWidth=&srcHeight=)


## Folder Structure
```
.Milestone_1
├── FlightSimulator               
|   ├── Helper          		  # Calculation and parsing classes.
|   ├── Model         		      # Model part of the mvvm architecture.
|   |   ├── DataModel.cs		  # The centeral model of the UI
|   |   ├── DLLModel.cs			  # Model of the anomalies detection algorithms.
|   |   ├── SocketModel.cs		  # Model for the communication with FlightGear.
|   ├── Properties     		      # Generated setting for the project.
|   ├── View         		      # View part of the mvvm - different displays in the project.tests 
|   |   ├── ...					  # The different components of the view part(timebar, graphs, gauges etc..) of the mvvm.
|   ├── ViewModel         		  # ViewModel for each of each of the different views in the project. 
|   |   ├── ...					  # Corresponding view model for each of the different views.
|   ├── public         		      # icons and pictures used.
|   ├── packages.config			  # config file for needed packeges.
|   ├── App.xml			  		  # Settings for some of the view components.
├── packages					  # Packages used in the project. 
├── plugins						  # Folder for the anomaly detection algorithms (dll files).

```


