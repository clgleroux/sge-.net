import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet } from '@angular/router';
import { DepartmentService } from '../services/department.service';
import { EmployeeService } from '../services/employee.service';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet, FormsModule],
  providers: [DepartmentService, EmployeeService],
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

  employeeDepartment!: any[];
  formEmployeeDepartment = {
    employeeId: '',
    departmentId: '',
  };

  attendances!: any[];
  formAttendances = {
    employeeId: '',
    startDate: '',
    endDate: '',
  };

  leaveRequests!: any[];
  formLeaveRequest = {
    employee_id: '',
    status_id: '',
    request_date: new Date(),
    start_date: '',
    end_date: '',
  };

  status!: any[];

  constructor(
    private departmentService: DepartmentService,
    private employeeService: EmployeeService
  ) {}

  async ngOnInit(): Promise<void> {
    console.log('init');
    this.departments = await this.departmentService.getAllDepartments();
  }

  public async createForm(entity: string) {
    let request;
    switch (entity) {
      case 'department':
        request = await this.departmentService.createDepartment(
          this.formDepartment
        );

        break;
      default:
        break;
    }
    if (request) console.log(`Create ${entity} as success`);
  }

  public async edit(entity: string, id: number) {}

  public async delete(entity: string, id: number) {}
}
