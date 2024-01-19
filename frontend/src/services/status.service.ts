import axios from 'axios';
import { config } from '../../config/config.default';
import { Status } from '../dto/status.dto';

export class StatusService {
  private BASE_URL: string = config.backend;

  async getAll(): Promise<Status[]> {
    const response = await axios.get<Status[]>(`${this.BASE_URL}/api/status`);
    return response.data;
  }

  async getById(id: number): Promise<Status> {
    return (await axios.get<Status>(`${this.BASE_URL}/api/status/${id}`)).data;
  }
}
