import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet } from '@angular/router';
import { DepartmentService } from '../services/department.service';
import { EmployeeService } from '../services/employee.service';
import { FormsModule } from '@angular/forms';
import { StatusService } from '../services/status.service';
import { LeaveRequestService } from '../services/leaveRequest.service';
import { Attendance } from '../dto/attendance.dto';
import { AttendanceService } from '../services/attendance.service';
import { EmployeeDepartmentService } from '../services/employeeDepartment.service';
import { EmployeeDepartment } from '../dto/employeeDepartment.dto';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet, FormsModule],
  providers: [
    DepartmentService,
    EmployeeService,
    StatusService,
    LeaveRequestService,
    AttendanceService,
    EmployeeDepartmentService,
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export class AppComponent implements OnInit {
  title = 'frontend';

  departments!: any[];
  formDepartment = {
    name: '',
    description: '',
    address: '',
  };

  employees!: any[];
  formEmployee = {
    firstName: '',
    lastName: '',
    email: '',
    phoneNumber: '',
    position: '',
  };

  employeeDepartments!: any[];
  formEmployeeDepartment = {
    employeeId: 0,
    departmentId: 0,
  };

  attendances!: any[];
  formAttendance = {
    employeeId: 0,
    startDate: new Date().toISOString().split('T')[0],
    endDate: new Date().toISOString().split('T')[0],
  };

  leaveRequests!: any[];
  formLeaveRequest = {
    employeeId: 0,
    statusId: 0,
    startDate: new Date().toISOString().split('T')[0],
    endDate: new Date().toISOString().split('T')[0],
  };

  status!: any[];

  historyRandomId: number[] = [];
  selectedRandom!: number;

  constructor(
    private departmentService: DepartmentService,
    private employeeService: EmployeeService,
    private statusService: StatusService,
    private leaveRequestService: LeaveRequestService,
    private attendanceService: AttendanceService,
    private employeeDepartmentService: EmployeeDepartmentService
  ) {}

  async ngOnInit(): Promise<void> {
    this.departments = await this.departmentService.getAll();
    this.employees = await this.employeeService.getAll();
    this.attendances = await this.attendanceService.getAll();
    this.employeeDepartments = await this.employeeDepartmentService.getAll();
    this.leaveRequests = await this.leaveRequestService.getAll();

    this.status = await this.statusService.getAll();
  }

  public async createForm(entity: string) {
    let request;
    switch (entity) {
      case 'department':
        request = await this.departmentService.createDepartment(
          this.formDepartment
        );

        this.departments = await this.departmentService.getAll();
        break;
      case 'employee':
        request = await this.employeeService.create(this.formEmployee);

        this.employees = await this.employeeService.getAll();
        break;
      case 'attendance':
        request = await this.attendanceService.create(this.formAttendance);

        this.attendances = await this.attendanceService.getAll();
        break;

      case 'employeeDepartment':
        request = await this.employeeDepartmentService.create(
          this.formEmployeeDepartment
        );

        this.attendances = await this.attendanceService.getAll();
        break;

      case 'leaveRequest':
        request = await this.leaveRequestService.create(this.formLeaveRequest);

        this.leaveRequests = await this.leaveRequestService.getAll();
        break;
      default:
        break;
    }
    if (request) console.log(`Create ${entity} as success`);
  }

  public async edit(entity: string, id: number) {}

  public async delete(entity: string, id: any) {
    let request;
    switch (entity) {
      case 'department':
        request = await this.departmentService.delete(id);

        this.departments = await this.departmentService.getAll();
        break;
      case 'employee':
        request = await this.employeeService.delete(id);

        this.employees = await this.employeeService.getAll();
        break;
      case 'attendance':
        request = await this.attendanceService.delete(id);

        this.attendances = await this.attendanceService.getAll();
        break;
      case 'employeeDepartment':
        request = await this.employeeDepartmentService.delete(id);

        this.employeeDepartments =
          await this.employeeDepartmentService.getAll();
        break;
      case 'leaveRequest':
        request = await this.leaveRequestService.delete(id);

        this.leaveRequests = await this.leaveRequestService.getAll();
        break;
      default:
        break;
    }
  }

  setRandom() {
    if (this.selectedRandom !== undefined)
      this.historyRandomId.push(this.selectedRandom);

    if (this.historyRandomId.length === this.employees.length) {
      this.historyRandomId = [];
    }

    let random;
    do {
      random = Math.floor(Math.random() * this.employees.length);
    } while (this.historyRandomId.includes(random));

    this.selectedRandom = random;
  }
}
