# Set up the project
<br><br>

## Step 1: Build the docker container with the Database
- Move a console into the project/mysql/ location.
- Run ```docker-compose up -d``` 
- The database will now run at http://localhost:3306
<br><br>

## Step 2: Install MySQL Workbench 8.0.33
- Download and Install https://dev.mysql.com/downloads/workbench/
- Click on MySQL Connections "+" and set up connection (pick a name you like, e.g. "MusicDB"
    - User: root, Password: root
- Set up DB and Data structure with the files "/mysql/init.sql" and then "/mysql/sample_data.sql"
  - Option 1: Top Left click on "SQL+ Folder"-icon and insert the content of .sql scripts, then click on the "lightning"-icon to run and create the DB.
  - Option 2: Go to Server->Data Import-> Check "Import from Self-Contained File" and pick the according .sql script, switch to "Import Progress"-tab and run
<br><br>

### =====DB READY=====
<br><br>

## Step 3: Run the Swagger Backend
- Open the project/backend/Backend.csproj with your Visual Studio 2022 and all .NET components already installed
- Run the "http" config and a swagger window opens in your browser at http://localhost:5073/swagger/index.html
<br><br>

### =====BACKEND READY=====
<br><br>


## Step 4: Run the WPF APP
- Open the project/frontend/frontend.csproj with your Visual Studio 2022 and all .NET components already installed
    - You might need to add the extension newtonsoft.json at Extensions->Manage
- Run the "frontend" config and the App opens and interacts with the backend->db