# Backend Web API for RTUITLab

| Status | Master | Develop |
| ------ | ---- | ---- |
|  Build and  test  | [![CI](https://github.com/Inozpavel/RTUITLabBackend/actions/workflows/dotnet.yml/badge.svg?branch=master&event=push)](https://github.com/Inozpavel/RTUITLabBackend/actions/workflows/dotnet.yml)| [![CI](https://github.com/Inozpavel/RTUITLabBackend/actions/workflows/dotnet.yml/badge.svg?branch=dev&event=push)](https://github.com/Inozpavel/RTUITLabBackend/actions/workflows/dotnet.yml) |

Prerequriments

[Docker Desktop](https://www.docker.com/products/docker-desktop)

Run system locally using Docker images

To run project open console in folder with [docker-compose.yml](docker-compose.yml?raw=true) file and run this commands

1. Pull services images from Docker Hub

```cmd
docker-compose pull
```

2. Run services

```cmd
docker-compose up -d
```

After starting API will be available on [localhost:8000](http://localhost:8000)
and [localhost:8001](http://localhost:8001)

## Project Information

### Project technologies stack

Main framework

- ASP.Net Core

Database

- PostgreSQL

Message broker

- RabbitMQ (MassTransit)

Testing

- xUnit
- Mock
- AutoFixture

Packages

- AutoMapper
- Entity Framework Core for PostgreSQL
- JWT Bearer for authentication