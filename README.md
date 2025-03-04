# Utilities App

This project is a Blazor application built with .NET 9, consisting of multiple services including a MySQL database, a migration service, an API service, and a web frontend. The application is designed to provide a comprehensive utility management system with various statistical views and data management capabilities.

## Running the Project with Docker Compose

To run the project using Docker Compose, follow these steps:

1. **Ensure Docker and Docker Compose are installed** on your machine. You can download and install them from the [Docker website](https://www.docker.com/get-started).

2. **Set up environment variables**:
   - Open `.env` file in the repository and replace `MYSQL_PASSWORD` and `MYSQL_ROOT_PASSWORD` with **your own strong passwords**!
   - The rest of variables left untouched, unless you know what you are doing.

3. **Build and run the services**:
   - Open a terminal and navigate to the root directory of the project.
   - (optional) Set varable REGISTRY_URL in `.env` file to pull image from registry i.e. `ghcr.io/natashalysakova/` to use pre-build images, instead of building your own.
   - Run the following command to build and start all the services defined in the `docker-compose.yml` file:

```bash
docker compose up -d
```

4. **Access the application**:
   - Once the services are up and running, you can access the web frontend by navigating to `http://localhost:10002` in your web browser.

5. **Stopping the services**:
   - To stop the running services, use the following command:

```bash
docker compose down
```

6. **Updating to new version**:
   - Open a terminal and navigate to the root directory of the project.
   - To update existing containers with new version grom registry, use the following command:

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