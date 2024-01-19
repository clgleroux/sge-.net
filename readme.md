# SGE .net

### Database postgres

docker-compose up --force-recreate --build -d

### Migration database

dotnet ef dbcontext scaffold "Host=localhost;Port=5432;Database=postgres;User Id=postgres;Password=example;" Npgsql.EntityFrameworkCore.PostgreSQL -o Entities --context-dir Infrastructures/Database/ -c ManageEmployeeDbContext -d -f

## Backend

Version : .net 8

BDD : Postgres

### Route

- DEPARTMENT /api/Departments
  -- CRUD department (example: IT)

- EMPLOYEE /api/Employees
  -- CRUD employee of organization

- EMPLOYEEDEPARTMENT /api/EmployeeDepartments
  -- CRD relation between employee and department (don't update just delete)

- ATTENDANCE /api/Attendances
  -- CRUD attendance of employee

- LEAVEREQUEST /api/Leaverequests
  -- CRUD leave request of employee

- STATUS /api/Status
  -- Get Only (status immuable)

[SWAGGER](https://swagger.io/) is available to get the documentation

## Frontend

Version : Node V20
This project was generated with [Angular CLI](https://github.com/angular/angular-cli) version 17.0.3.

### Development server

Run `ng serve` for a dev server. Navigate to `http://localhost:4200/`. The application will automatically reload if you change any of the source files.

### Code scaffolding

Run `ng generate component component-name` to generate a new component. You can also use `ng generate directive|pipe|service|class|guard|interface|enum|module`.

### Build

Run `ng build` to build the project. The build artifacts will be stored in the `dist/` directory.

### Running unit tests

Run `ng test` to execute the unit tests via [Karma](https://karma-runner.github.io).

### Running end-to-end tests

Run `ng e2e` to execute the end-to-end tests via a platform of your choice. To use this command, you need to first add a package that implements end-to-end testing capabilities.

### Further help

To get more help on the Angular CLI use `ng help` or go check out the [Angular CLI Overview and Command Reference](https://angular.io/cli) page.
