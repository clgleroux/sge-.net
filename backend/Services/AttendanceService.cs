using backend.Dto.Attendance;
using backend.Entities;
using backend.Repositories;

namespace backend.Services
{
  public class AttendanceService
  {
    private readonly AttendanceRepository _attendanceRepository;

    public AttendanceService(AttendanceRepository attendanceRepository)
    {
      _attendanceRepository = attendanceRepository;
    }

    public async Task<ReadAttendance> CreateAttendanceAsync(CreateAttendance attendance)
    {


      var attendanceGetStart = await _attendanceRepository.GetAttendanceByStartDateAsync(attendance.StartDate, attendance.EmployeeId);
      var attendanceGetEnd = await _attendanceRepository.GetAttendanceByStartDateAsync(attendance.EndDate, attendance.EmployeeId);

      if (attendanceGetStart is not null || attendanceGetEnd is not null)
      {
        throw new Exception(
            $"Echec de création d'un attendance : Il existe déjà un attendance avec cette date"
        );
      }

      if (attendance.StartDate > attendance.EndDate)
      {
        throw new Exception(
            $"Echec de création d'un département : Il existe déjà un département avec ce nom {attendance.StartDate}"
        );
      }


      var attendanceTocreate = new Attendance()
      {
        EmployeeId = attendance.EmployeeId,
        StartDate = attendance.StartDate,
        EndDate = attendance.EndDate,
      };

      var attendanceCreated = await _attendanceRepository.CreateAttendanceAsync(
          attendanceTocreate
      );

      return new ReadAttendance()
      {
        Id = attendanceCreated.AttendanceId,
        EmployeeId = (int)attendanceCreated.EmployeeId,
        StartDate = attendanceCreated.StartDate,
        EndDate = attendanceCreated.EndDate,
      };
    }

    public async Task<List<ReadAttendance>> GetAttendances()
    {
      var attendances = await _attendanceRepository.GetAttendancesAsync();

      List<ReadAttendance> readAttendances = new List<ReadAttendance>();

      foreach (var attendance in attendances)
      {
        readAttendances.Add(
            new ReadAttendance()
            {
              Id = attendance.AttendanceId,
              EmployeeId = (int)attendance.EmployeeId,
              StartDate = attendance.StartDate,
              EndDate = attendance.EndDate,
            }
        );
      }

      return readAttendances;
    }

    public async Task<ReadAttendance> GetAttendanceByIdAsync(int attendanceId)
    {
      var attendance = await _attendanceRepository.GetAttendanceByIdAsync(attendanceId);

      if (attendance is null)
        throw new Exception(
            $"Echec de recupération des informations d'un département car il n'existe pas : {attendanceId}"
        );

      return new ReadAttendance()
      {
        Id = attendance.AttendanceId,
        EmployeeId = (int)attendance.EmployeeId,
        StartDate = attendance.StartDate,
        EndDate = attendance.EndDate,
      };
    }

    public async Task<Attendance> UpdateAttendanceAsync(
        int attendanceId,
        UpdateAttendance attendance
    )
    {
      var attendanceUpdate =
          await _attendanceRepository.GetAttendanceByIdAsync(attendanceId)
          ?? throw new Exception(
              $"Echec de mise à jour d'un département : Il n'existe aucun attendance avec cet identifiant : {attendanceId}"
          );

      if (attendance.StartDate > attendance.EndDate)
      {
        throw new Exception(
            $"Echec de création d'un département : Il existe déjà un département avec ce nom {attendance.StartDate}"
        );
      }

      attendanceUpdate.EmployeeId = attendance.EmployeeId;
      attendanceUpdate.StartDate = attendance.StartDate;
      attendanceUpdate.EndDate = attendance.EndDate;

      return await _attendanceRepository.UpdateAttendanceAsync(attendanceUpdate);
    }

    public async Task<Attendance> DeleteAttendanceById(int attendanceId)
    {
      var attendanceGet =
          await _attendanceRepository.GetAttendanceByIdWithIncludeAsync(attendanceId)
          ?? throw new Exception(
              $"Echec de suppression d'un département : Il n'existe aucun attendance avec cet identifiant : {attendanceId}"
          );



      return await _attendanceRepository.DeleteAttendanceByIdAsync(attendanceId);
    }
  }
}
