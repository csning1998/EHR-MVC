
# Project Setup

This project requires a few environment variables to be set up for a successful connection to the SQL Server database. Follow these instructions to configure the environment:

## Setting Up the Environment Variables

1. **Locate the `ChangeMe.env` file**

   In the project root directory, you will find a file named `ChangeMe.env`. This file serves as an example for the required environment variables.

2. **Edit the Database Connection String**

   Open `ChangeMe.env` and replace `__(Replace with your Server Name)__` with the name of your SQL Server instance. 

   For example, if your SQL Server instance name is `MY_SERVER`, the connection string should look like this:
   
   ```plaintext
   DB_CONNECTION_STRING="Data Source=MY_SERVER\\SQLEXPRESS;Integrated Security=True;Persist Security Info=False;Pooling=False;Multiple Active Result Sets=False;Connect Timeout=60;Encrypt=True;Trust Server Certificate=True;Command Timeout=0"
   ```
   
   Remenber to copy the whole connection string from your corresponding DB properties in MS SSMS.

3. **Rename `ChangeMe.env` to `.env`**

   Once you've updated the `DB_CONNECTION_STRING`, rename `ChangeMe.env` to `.env`. The application will automatically load the environment variables from this file.

4. **Verify the Connection**

   Make sure your SQL Server instance is running and accessible. The application will use the connection string defined in `.env` to connect to the database.

## Troubleshooting

- **Login Issues**: If you encounter errors related to login, check if you’re using the correct SQL Server authentication mode (Windows Authentication or SQL Server Authentication) and update the connection string as needed.
- **Encryption Errors**: Ensure that your SQL Server instance supports encrypted connections. If not, set `Encrypt=False` in the connection string.

By following these steps, you should be able to configure the necessary environment variables for this project.