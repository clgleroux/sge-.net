import axios from 'axios';
import { config } from '../../config/config.default';
import { Department } from '../dto/department.dto';

export class DepartmentService {
  private BASE_URL: string = config.backend;

  async getAllDepartments(): Promise<Department[]> {
    const response = await axios.get<Department[]>(
      `${this.BASE_URL}/api/departments`
    );
    return response.data;
  }

  async getDepartmentById(id: number): Promise<Department> {
    return (
      await axios.get<Department>(`${this.BASE_URL}/api/departments/${id}`)
    ).data;
  }

  async createDepartment(department: Department): Promise<Department> {
    return await axios.post(`${this.BASE_URL}/api/departments`, department);
  }

  async updateDepartment(department: Department): Promise<void> {
    return await axios.put(
      `${this.BASE_URL}/api/departments/${department.id}`,
      department
    );
  }

  async deleteDepartment(id: number): Promise<void> {
    return await axios.delete(`${this.BASE_URL}/api/departments/${id}`);
  }
}
