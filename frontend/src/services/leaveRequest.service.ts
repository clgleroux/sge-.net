import axios from 'axios';
import { LeaveRequest } from '../dto/leaveRequest.dto';
import { config } from '../../config/config.default';

export class LeaveRequestService {
  private BASE_URL: string = config.backend;

  async getAll(): Promise<LeaveRequest[]> {
    const response = await axios.get<LeaveRequest[]>(
      `${this.BASE_URL}/api/leaverequests`
    );
    return response.data;
  }

  async getEmployeeById(id: number): Promise<LeaveRequest> {
    return (
      await axios.get<LeaveRequest>(`${this.BASE_URL}/api/leaverequests/${id}`)
    ).data;
  }

  async create(leaveRequest: LeaveRequest): Promise<LeaveRequest> {
    return await axios.post(`${this.BASE_URL}/api/leaverequests`, leaveRequest);
  }

  async update(leaveRequest: LeaveRequest): Promise<void> {
    return await axios.put(
      `${this.BASE_URL}/api/leaverequests/${leaveRequest.id}`,
      leaveRequest
    );
  }

  async delete(id: number): Promise<void> {
    return await axios.delete(`${this.BASE_URL}/api/leaverequests/${id}`);
  }
}
