# PathDrift
A simple ASP.NET distributed system that reads and display coordinates 

The system is made up of two services:
CoordinatesClient
CoordinatesProcessor

CoordinatesClient is a console application that when run will 
1. Load a csv of cooardinates 
2. Post the cooardinates to the CoordinatesProcessor using gRPC

The CoordinatesProcessor is a Blazor web app that will 
1. Receive a list of coordinates from the CoordinatesClient
2. Render the cooardinates in raw, 2D and 3D formats

Running the app locally
The project startup is configured to run both applications. 
