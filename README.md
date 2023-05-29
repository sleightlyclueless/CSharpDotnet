# MusicDB

## getting started

how to install and build our app, so that a new developer could get started

Some notes:
* The ER diagram and the database dump are within the `./mysql/` folder
* For a proper git history, please pay attention the other branches we worked on

### Step 1: Build the Docker container for the database
- Start Docker or the Docker service on your machine
- Open a terminal in `./mysql/`
- Run `docker-compose up -d`
- The database will be available at `localhost:3306`

### Step 2: Fill the database with sample data

In this example we use the *MySQL Workbench 8.0.33*. You could of course use any other MySQL client, like the `mysql` command line tool.

- Please download and install the MySQL Workbench from [here](https://dev.mysql.com/downloads/workbench/)
- Once the MySQL is started, click the `+` icon next to *MySQL Connections* and setup a new connection to our database. Choose any connection name for it, like `MusicDB`
    - Hostname: `127.0.0.1`
    - Port: `3306`
    - Username: `root`
    - Password: `root`
- Now, lets fill the database with our needed data structure and some sample data. For this we use the `./mysql/musicdb-dump.sql`. There are two options, pick any:
  1. Click on the icon that pictures a folder and has SQL written on it in the top left and insert the content of the `musicdb-dump.sql` script, then click on the *lightning* icon to run and create the data structure and samples.
  2. Go to `Server -> Data Import -> Check "Import from Self-Contained File` and pick the `musicdb-dump.sql` script, switch to *Import Progress* tab and run

> **The database should be ready to use now! :partying_face:**


### Step 3: Run the back-end
- Run the `http` configuration of the `./backend/Backend.csproj`, you can do so by
	- open the project within Visual Studio 2022 (with all needed dependencies installed) and running the `http` configuration from there
	- use the command line tool like `dotnet run --configuration http`
- The Swagger test page should be available at http://localhost:5073/swagger/index.html now

> **The back-end should be up and ready now! :sunglasses:**


### Step 4: Run the WPF APP
- Open the `./frontend/frontend.csproj` with your Visual Studio 2022 (with all needed dependencies installed)
    - You might need to install the NuGet package `Newtonsoft.Json`
- Run the `frontend` configuration, which should the app and lets you interact with the back-end and thus the database-

> **Everything is working now, awesome! :star_struck:**


## Point Table
| Task | Points |
|------|--------|
|Task 1|    2   |
|Task 2|    3   |
|Task 3|    5   |
|Task 4|    1   |
|Task 5|    1   |
|Task 6|    3   |
|Task 7|   wip  |
