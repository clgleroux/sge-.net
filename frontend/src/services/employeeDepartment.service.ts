import axios from 'axios';
import { config } from '../../config/config.default';
import { EmployeeDepartment } from '../dto/employeeDepartment.dto';

export class EmployeeDepartmentService {
  private BASE_URL: string = config.backend;

  async getAll(): Promise<EmployeeDepartment[]> {
    const response = await axios.get<EmployeeDepartment[]>(
      `${this.BASE_URL}/api/employeedepartments`
    );
    return response.data;
  }

  async create(
    employeeDepartment: EmployeeDepartment
  ): Promise<EmployeeDepartment> {
    return await axios.post(
      `${this.BASE_URL}/api/employeedepartments`,
      employeeDepartment
    );
  }

  async delete(
    employeeDepartment: EmployeeDepartment
  ): Promise<EmployeeDepartment> {
    return await axios.delete(
      `${this.BASE_URL}/api/employeedepartments/${employeeDepartment.employeeId}/departments/${employeeDepartment.departmentId}`
    );
  }
}
