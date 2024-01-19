export interface LeaveRequest {
  id?: number;
  employeeId: number;
  statusId: number;
  requestDate?: string;
  startDate: string;
  endDate: string;
}
