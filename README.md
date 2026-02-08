docker cleanup commands --> 
docker compose down -v
docker container prune -f
docker image prune -f
docker compose up --build -d

Catalog Microservices - vertical clice architecture

1. MediatR for CQRS
2. Carter for API Endpoints - routing and handling http requests
3. Marten for Postgresql - DocumentDB
4. Mapster for Object Mapping
5. FluentValidation
