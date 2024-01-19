import axios from 'axios';
import { config } from '../../config/config.default';
import { Attendance } from '../dto/attendance.dto';

export class AttendanceService {
  private BASE_URL: string = config.backend;

  async getAll(): Promise<Attendance[]> {
    const response = await axios.get<Attendance[]>(
      `${this.BASE_URL}/api/attendances`
    );
    return response.data;
  }

  async getAttendanceById(id: number): Promise<Attendance> {
    return (
      await axios.get<Attendance>(`${this.BASE_URL}/api/attendances/${id}`)
    ).data;
  }

  async create(attendance: Attendance): Promise<Attendance> {
    return await axios.post(`${this.BASE_URL}/api/attendances`, attendance);
  }

  async updateAttendance(attendance: Attendance): Promise<void> {
    return await axios.put(
      `${this.BASE_URL}/api/attendances/${attendance.id}`,
      attendance
    );
  }

  async delete(id: number): Promise<void> {
    return await axios.delete(`${this.BASE_URL}/api/attendances/${id}`);
  }
}
