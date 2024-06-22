# Smart Parking Management System
![IMG-20240622-WA0005](https://github.com/senanurincekara/SmartParkingSystem-Arduino/assets/97362569/53715507-1cca-4787-88e9-0e9a03c0480d)
![IMG-20240622-WA0008](https://github.com/senanurincekara/SmartParkingSystem-Arduino/assets/97362569/e955230b-b1d7-41c9-bb06-fd292f2309e8)

## Project Purpose
The aim of this project is to develop a smart parking management system to ensure the efficient use of the parking lot and to provide convenience to drivers.

## Table of Contents
- [Project Purpose](#project-purpose)
- [Project Features](#project-features)
- [Hardware Used](#hardware-used)
- [Software Used](#software-used)
- [Screenshots destkop app](#screenshots)
- [Installation and Setup](#installation-and-setup)

## Project Features
The operations carried out within the project environment are as follows:

- Monitor the occupancy status of the parking lot in real-time and provide information to drivers via LED indicators.
- Automatically control the parking lot gate.
- Allow entry of a new car based on the occupancy status of the parking lot.
- Record the entry and exit times of vehicles and monitor them in a database.
- Calculate the fee to be paid when a vehicle exits the parking lot.
- Monitor and control the parking lot status with a desktop application.

## Hardware Used
- **ESP32 WROOM:** Used as the main control unit in project management.
- **2 HC-SR04 Ultrasonic Distance Sensors:**
  - The first sensor is located at the entrance of the parking lot. When a car is detected here, the servo motor is activated, and the gate opens.
  - The second sensor is located at the exit of the parking lot. When a car is detected here, the relevant car information is deleted from the database.
- **Tower Pro SG90 Mini (9g) Servo Motor:** Used at the entrance of the parking lot detected by the first distance sensor. Based on the information pulled from the database, if the parking lot is not full, the servo motor activates, and the gate opens.
- **2 5mm LEDs:** Green and red LEDs are used in the project. These LEDs indicate whether the parking lot is full or not. If the parking lot is full, the red LED lights up; otherwise, the green LED lights up.

## Software Used
- **Arduino IDE:** Used to program the ESP32.
- **Visual Studio:** Used to develop the desktop application.
- **C#:** Used as the programming language for the desktop application.
- **MSSQL:** Used as the database to record entry and exit times of vehicles and monitor the parking lot status.

## Screenshots destkop app
![IMG-20240622-WA0002](https://github.com/senanurincekara/SmartParkingSystem-Arduino/assets/97362569/6b3bde6f-eb79-43e1-a8d6-f09996357eb0)
![IMG-20240622-WA0003](https://github.com/senanurincekara/SmartParkingSystem-Arduino/assets/97362569/1dc8eeb0-b971-487a-8b61-9879dff0842f)
![IMG-20240622-WA0006](https://github.com/senanurincekara/SmartParkingSystem-Arduino/assets/97362569/c5b9e37f-ff93-4ee6-a55f-77e4576a7111)
![IMG-20240622-WA0007](https://github.com/senanurincekara/SmartParkingSystem-Arduino/assets/97362569/8546a9d7-afb1-47ea-b74a-eceee87c2b37)
![IMG-20240622-WA0009](https://github.com/senanurincekara/SmartParkingSystem-Arduino/assets/97362569/fc606247-6e48-40f9-8870-59dee395ace3)
![IMG-20240622-WA0010](https://github.com/senanurincekara/SmartParkingSystem-Arduino/assets/97362569/ca065727-c553-4645-87b8-dedc490da6f2)
![IMG-202406![IMG-20240622-WA0012](https://github.com/senanurincekara/SmartParkingSystem-Arduino/assets/97362569/f64359e9-f054-40fd-8d87-b1f9e8f53233)
22-WA0011](https://github.com/senanurincekara/SmartParkingSystem-Arduino/assets/97362569/052d8f31-480b-4b4c-8bf5-2cbf1385f775)

## Installation and Setup
1. Program the ESP32 using Arduino IDE.
2. Connect the HC-SR04 Ultrasonic Distance Sensors and the Servo Motor to the ESP32.
3. Connect the green and red LEDs to the ESP32.
4. Set up the MSSQL database and create the necessary tables.
5. Develop the desktop application using Visual Studio and configure it to monitor the parking lot status.
6. Run the project and monitor the real-time status of the parking lot.

