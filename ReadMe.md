# Reference Architecture
---

## How to run

```
dotnet restore
dotnet build
cd Gossip.SQLite
dotnet ef database update
cd ..
dotnet test
```

## Composition

1. Gossip.Web - Web API for front-end JS apps
1. Gossip.Contract - application services' contract Gossip.Web is running against
1. Gossip.Application - implementation of application services (according to Gossip.Contract)
1. Gossip.Domain - business logic
1. Gossip.SQLite - interactions (implementation) with persistent database (SQLite)
1. Gossip.DynamoDb - interactions (implemenetation) with cloud blob storage (Amazon DynamoDb)
1. Tests ... - well ..., justs tests for corresponding layers

## TODO

1. Re-write shit to Dapper, so idea of ORM-agnisticism is proven
1. Dependency resolver / binder
1. Error handling (exceptions)
1. Validation
1. Logging
1. Authorization
1. Authentication
1. XSRF
1. XSS (Encoding)
1. Dockerize deployment (multi-instance)
1. DB snapshots for integration tests
1. input params + output envelope for Web API