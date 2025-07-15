#Standard-Temp-Project

This project is a standard .NET 9 template built using Clean Architecture principles.
It separates concerns across distinct layers such as API, Application, Domain, Infrastructure, and Persistence, making the codebase easier to maintain and scale.
The Repository Pattern is used to abstract data access logic, keeping it clean and testable.
A custom exception handling middleware is included to manage errors in a consistent and centralized way across the application.

To simplify configuration, each layer provides its own extension method for registering services, which keeps Program.cs clean and modular. 
The structure is well-organized for building real-world enterprise applications and can be easily extended with features like JWT authentication, Swagger, and unit testing.

This template is ideal for developers looking to start a scalable .NET backend project with a clean and maintainable codebase.
In the future it will include more features like Authentication and Authorization also.
