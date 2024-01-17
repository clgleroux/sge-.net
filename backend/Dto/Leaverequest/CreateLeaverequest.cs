namespace backend.Dto.Leaverequest
{
    public class CreateLeaverequest
    {
        public int EmployeeId { get; set; }

        public int StatusId { get; set; }

        public DateOnly RequestDate { get; set; }

        public DateOnly StartDate { get; set; }

        public DateOnly EndDate { get; set; }
    }
}
