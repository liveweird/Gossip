Reference Architecture
---

1. Gossip.Web - Web API for front-end JS apps
1. Gossip.Contract - application services' contract Gossip.Web is running against
1. Gossip.Application - implementation of application services (according to Gossip.Contract)
1. Gossip.Domain - business logic
1. Gossip.SQLite - interactions (implementation) with persistent database (SQLite)
1. Gossip.DynamoDb - interactions (implemenetation) with cloud blob storage (Amazon DynamoDb)
1. Tests ... - well ..., justs tests for corresponding layers