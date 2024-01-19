export interface LeaveRequest {
  id?: number;
  employeeId: number;
  statusId: number;
  requestDate?: Date;
  startDate: Date;
  endDate: Date;
}
