# Brisbane-Airport-Application-Prototype  

Purpose: Creating a well-documented and easy-to-maintain application that handles planes arriving and departing, as well as passengers. Language used is C#.  
  (NOTE: This is currently a prototype, designed to be easily extendable and modifiable.)
  
### User Stories:  
  * 1a) As a user (traveller, frequent flyer, or flight manager), I would like to register with the application using my name, age, email address, mobile number, and password so that my account remains private and secure.  
  * 1b) As a user, I want to authenticate myself using my email and password so that I can access my private records and conduct transactions securely.  
  * 1c) As a user, I would like to view my registered details (name, age, email address, and mobile number) so that I can verify my information is recorded correctly.  
  * 1d) As a user, I want to be able to log out from the application so that no one else can access my account.  
  * 1e) As a user, I want to be able to change my password so that I can protect my account if it has been compromised.  
    
  * 2a) As a frequent flyer, I would like to register with my frequent flyer number and accrued points so that I can continue earning rewards for each of my flights.  
  * 2b) As a frequent flyer, I would like to view my frequent flyer number and points so that I can confirm my rewards are being tracked accurately.  
  
  * 3a) As a flight manager, I would like to register with my staff ID so that I can be identified in the system using my employer’s official credentials.  
  * 3b) As a flight manager, I would like to view my staff ID so that I can confirm the system has recorded my identity correctly.  
  * 3c) As a flight manager, I would like to register an arrival flight with its airline, flight code, departure city, plane ID, and scheduled arrival date and time so that the flight is available in the system.  
  * 3d) As a flight manager, I would like to register a departure flight with its airline, flight code, arrival city, plane ID, and scheduled departure date and time so that the flight is available in the system.  
  * 3e) As a flight manager, I would like each plane ID to be unique across all flights so that passengers are correctly matched to their plane.  
  * 3f) As a flight manager, I would like to view all scheduled flights (arrivals and departures) in chronological order so that I have a clear overview of airport operations.  
  
  * 4a) As a traveller or frequent flyer, I would like to book an arrival flight to Brisbane so that I can organise my travel plans.  
  * 4b) As a traveller or frequent flyer, I would like to book a departure flight to another city so that I can organise my travel plans.  
  * 4c) As a traveller, I would like to book a seat on a plane so that I have a confirmed place to sit during my journey.  
  * 4d) As a frequent flyer, I would like to book my preferred seat, even if another traveller has already selected it, so that I can benefit from my loyalty privileges.  
  * 4e) As a traveller, I would like to be automatically allocated the next available incremental seat on the row/column if my original seat is reassigned to a frequent flyer so that I can still complete my journey comfortably.  
  * 4f) As a traveller or frequent flyer, I would like to view a ticket of my booked arrival flight containing the flight code, departure city, arrival time, and seat so that I can prepare for my journey.  
  * 4g) As a traveller or frequent flyer, I would like to view a ticket of my booked departure flight containing the flight code, arrival city, departure time, and seat so that I can prepare for my journey.  
  * 4h) As a frequent flyer, I would like to view the number of points earned for each flight so that I can track my progress towards rewards.  
  * 4i) As a flight manager, I would like to update a flight’s status when it is delayed so that travellers and frequent flyers are kept informed.  
  * 4j) As a flight manager, I would like the system to automatically adjust departure times by the same duration when the corresponding arrival flight is delayed so that passengers receive accurate information.  
