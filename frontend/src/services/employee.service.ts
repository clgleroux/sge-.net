import axios from 'axios';
import { Employee } from '../dto/employee.dto';
import { config } from '../../config/config.default';

export class EmployeeService {
  private BASE_URL: string = config.backend;

  async getAllEmployees(): Promise<Employee[]> {
    const response = await axios.get<Employee[]>(this.BASE_URL);
    return response.data;
  }

  async getEmployeeById(id: number): Promise<Employee> {
    return (await axios.get<Employee>(`${this.BASE_URL}/${id}`)).data;
  }

  async createEmployee(employee: Employee): Promise<Employee> {
    return await axios.post(this.BASE_URL, employee);
  }

  async updateEmployee(employee: Employee): Promise<void> {
    return await axios.put(`${this.BASE_URL}/${employee.id}`, employee);
  }

  async deleteEmployee(id: number): Promise<void> {
    return await axios.delete(`${this.BASE_URL}/${id}`);
  }
}
