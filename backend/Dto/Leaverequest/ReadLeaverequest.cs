namespace backend.Dto.Leaverequest
{
    public class ReadLeaverequest
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }

        public int StatusId { get; set; }

        public DateOnly RequestDate { get; set; }

        public DateOnly StartDate { get; set; }

        public DateOnly EndDate { get; set; }
    }
}
