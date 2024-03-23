# Work Tracker
[![build](https://github.com/leonardo-avila/work-tracker/actions/workflows/build.yml/badge.svg)](https://github.com/leonardo-avila/work-tracker/actions/workflows/build.yml)
[![Reliability Rating](https://sonarcloud.io/api/project_badges/measure?project=leonardo-avila_work-tracker&metric=reliability_rating)](https://sonarcloud.io/summary/new_code?id=leonardo-avila_work-tracker)


Simple system for clock in and out with reports. Developed in .NET 6.

## Requisites
- .NET 6
- Docker
- Terraform

## How to run
On the root folder, run the following commands:

```bash
    make run-database
    make run-api
```

Make sure to edit the variables on docker-compose file on `infra/local` folder.

You could see more details on the [README](src/README.md) of the API.