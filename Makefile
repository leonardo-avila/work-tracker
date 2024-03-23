run-database:
	cd infra/local; docker-compose up -d worktracker-database;

run-api:
	cd infra/local; docker-compose up -d worktracker-api;

drop-api:
	cd infra/local; docker-compose down worktracker-api;

run-debug:
	cd src/adapters/driver/worktracker.api; dotnet run;