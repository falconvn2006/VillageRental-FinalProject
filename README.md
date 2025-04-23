# Village Rentals
A final project for CPRG-211 at SAIT
## What is this project about? 

### Introduction
Village Rentals is a desktop application that can help rental businesses add and manage customer information, equipment information, rental information. All of this data can be store in a remote or local database

The application is built using Maui Blazor with MySqlConnector to connect to a database

### Main functionalities
You can create and add customer information to the system. When you need to edit the information of a customer, you can use the find functionality and find the user based on the id, last name and email. Choose the result of the user you want to find then click on it and you can edit and save the new information. This also applies to equipment, category and rental management pages

Finally, the application can generate a report table showing a list of all clients, equipments and rental informations

## How to run the project 
Before building and run the project, make sure to create an ``appsettings.json`` file in the root of the project. The content of the json file should be:

```json
{
    "DATABASE_SERVER_ADDRESS" : "<Database Address>",
    "USERNAME" : "<Username to login the database>",
    "PASSWORD" : "<Password to login the database>",
    "DATABASE_NAME" : "<Name of the database>"
}
```

However, if you want to connect the database locally. Please run the ``create_example_data.sql`` file, it will create all the tables for the application and add some samples data to the tables