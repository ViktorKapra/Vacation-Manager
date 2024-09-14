# Vacation Manager 

## Overview
The 'Vacation Manager' is an online platform designed to streamline the management of employee vacations and leaves within a company. 
It allows employees to submit vacation requests, managers to approve or reject them, and admins to control user accounts.
The system ensures an efficient and transparent process for both employees and administratos.
## Features
- Employee Dashboard: Allows employees to submit leave requests, view their leave balance, and track the status of their vacation requests.
- Manager Dashboard: Enables managers to view and manage vacation requests from their team, approve/reject requests, and view team leave calendars.
- Admin Panel: Provides a comprehensive view of all employee accounts.
- Leave Calendar: Displays a calendar view showing team and department leaves, helping avoid scheduling conflicts (will be added in the future).
- Role-Based Access Control: Custom roles for employees, managers, and admins to manage appropriate access and permissions
## Architecture
This platform is built using the MVC (Model-View-Controller) architecture, ensuring a clear separation of concerns between the User Interface (View), Application Logic (Controller), and Data Layer (Model).  
The Model represents the data and business logic of the platform, and it is tightly integrated with Microsoft SQL Server to store and retrieve data.  
The Controller acts as the intermediary between the View (user interface) and the Model (data layer). It processes incoming requests from users, communicates with the Model to retrieve or update data, and then selects the appropriate View to display the results.
