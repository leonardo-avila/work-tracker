# Work Tracker Architecture

At this moment, work tracker is a monolith application. The architecture is simple and is composed by the following components:

- **API**: The main component of the application. It is responsible for handling the requests and responses from the clients.
- **Database**: The database is a MySQL instance that stores the data of the application.
- **Mailtrap**: The mailtrap is a service that is used to send emails in a development environment.
- **AWS Cognito**: The AWS Cognito is used to authenticate the users of the application.

We could see the diagram below to understand the architecture of the application:

<img width="571" alt="image" src="https://github.com/leonardo-avila/work-tracker/assets/29763488/c2cd5da0-bfd8-47f8-a5cb-e93b7ca27169">

After run terraform, the architecture will be like this:

<img width="795" alt="image" src="https://github.com/leonardo-avila/work-tracker/assets/29763488/41a89c6e-b008-4e50-95d9-2f3965e48bda">