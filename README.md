# Utilities App

The application is designed to provide a comprehensive utility services management system.
This is a Blazor application built with .NET 9, consisting of multiple services including an API service, a migration service and a web frontend. 

<img width="1000" alt="Main view" src="https://github.com/user-attachments/assets/c4d09252-105c-4228-86d1-1ea73e2f6e0a" />
<img width="1000" alt="Check view" src="https://github.com/user-attachments/assets/bceee779-5b26-49c9-94d7-48c267b359f3" />

## AppHost Project
The AppHost project is an Aspire project. It can be used to deploy and run the entire application stack in a containerized environment (like Azure). It's not needed if docker is used. Otherwise use `http` or `https` launch profiles to get fancy Aspire dashboard and some basic telemetry.
<img width="1000" alt="Aspire dashboard" src="https://github.com/user-attachments/assets/372bf749-cde8-42a1-ab20-639402ddf1d2" />

## Running the Project with Docker Compose

To run the project using Docker Compose, follow these steps:

1. **Ensure Docker and Docker Compose are installed** on your machine. You can download and install them from the [Docker website](https://www.docker.com/get-started).

2. **Set up environment variables**:
   - Open `.env` file in the repository and replace `MYSQL_PASSWORD` and `MYSQL_ROOT_PASSWORD` with **your own strong passwords**!
   - The rest of variables left untouched, unless you know what you are doing.

3. **Build and run the services**:
   - Open a terminal and navigate to the root directory of the project.
   - (optional) Set varable `REGISTRY_URL` in `.env` file to pull image from registry i.e. `ghcr.io/natashalysakova/` to use pre-build images, instead of building your own.
   - Run the following command to build and start all the services defined in the `docker-compose.yml` file:

```bash
docker compose up -d
```

4. **Access the application**:
   - Once the services are up and running, you can access the web frontend by navigating to `http://localhost:10002` in your web browser.
   - `migrationservice` will be shut down after initial run, that's fine, it's needed only for database initialization.

5. **Stopping the services**:
   - To stop the running services, use the following command:

```bash
docker compose down
```

6. **Updating to new version**:
   - Open a terminal and navigate to the root directory of the project.
   - To update existing containers with new version grom registry (if `REGISTRY_URL` is set in `.env` file), use the following command:
```bash
docker compose down # stop and remove the container
docker compose pull # pull new image from registry
docker compose up -d # restart services
```
   - To update existing containers with new version build locally, use the following command:
 ```bash
docker compose down #stop and remove the container
git pull
docker compose up -d --build
```
