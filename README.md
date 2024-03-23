# Work Tracker

Simple system for clock in and out with reports. Developed in .NET 6.

## Requisites
- .NET 6
- Docker
- Terraform

## How to run
    On the root folder, run the following commands:
    ```bash
    make run-database
    make run-debug
    ```

    Make sure to edit the variables on docker-compose file on `infra/local` folder.