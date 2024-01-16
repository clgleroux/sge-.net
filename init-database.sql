CREATE DATABASE sge;

-- Création de la table Department
CREATE TABLE Department (
    department_id SERIAL PRIMARY KEY,
    name VARCHAR(255) NOT NULL,
    description VARCHAR(255),
    address VARCHAR(255)
);

-- Création de la table Employee
CREATE TABLE Employee (
    employee_id SERIAL PRIMARY KEY,
    first_name VARCHAR(255) NOT NULL,
    last_name VARCHAR(255) NOT NULL,
    email VARCHAR(100) NOT NULL,
    phone_number VARCHAR(20),
    position INT NOT NULL
);

-- Table intermédiaire pour la relation plusieurs-à-plusieurs entre Department et Employee
CREATE TABLE DepartmentEmployee (
    department_id INT REFERENCES Department(department_id),
    employee_id INT REFERENCES Employee(employee_id),
    PRIMARY KEY (department_id, employee_id)
);

-- Création de la table Attendance
CREATE TABLE Attendance (
    attendance_id SERIAL PRIMARY KEY,
    employee_id INT REFERENCES Employee(employee_id),
    start_date DATE NOT NULL,
    end_date DATE NOT NULL
);

-- Création de la table Status
CREATE TABLE Status (
    status_id SERIAL PRIMARY KEY,
    status_name VARCHAR(255) NOT NULL
);

INSERT INTO Status (status_name)
    VALUES ('pending'), ('rejected'), ('accepted');

-- Création de la table LeaveRequest
CREATE TABLE LeaveRequest (
    leave_request_id SERIAL PRIMARY KEY,
    employee_id INT REFERENCES Employee(employee_id),
    status_id INT REFERENCES Status(status_id),
    request_date DATE NOT NULL,
    start_date DATE NOT NULL,
    end_date DATE NOT NULL
);
