using backend.Dto.Leaverequest;
using backend.Entities;
using backend.Repositories;

namespace backend.Services
{
    public class LeaverequestService
    {
        private readonly LeaverequestRepository _leaverequestRepository;

        public LeaverequestService(LeaverequestRepository leaverequestRepository)
        {
            _leaverequestRepository = leaverequestRepository;
        }

        public async Task<ReadLeaverequest> CreateLeaverequestAsync(CreateLeaverequest leaverequest)
        {
            var leaverequestGetStart =
                await _leaverequestRepository.GetLeaverequestByStartDateAsync(
                    leaverequest.StartDate,
                    leaverequest.EmployeeId
                );
            var leaverequestGetEnd = await _leaverequestRepository.GetLeaverequestByStartDateAsync(
                leaverequest.EndDate,
                leaverequest.EmployeeId
            );

            if (leaverequestGetStart is not null || leaverequestGetEnd is not null)
            {
                throw new Exception(
                    $"Echec de création d'un leaverequest : Il existe déjà un leaverequest avec cette date"
                );
            }

            if (leaverequest.StartDate > leaverequest.EndDate)
            {
                throw new Exception(
                    $"Echec de création d'un leaverequest : EndDate inférieur à StartDare"
                );
            }

            var leaverequestTocreate = new Leaverequest()
            {
                EmployeeId = leaverequest.EmployeeId,
                StatusId = leaverequest.StatusId,
                RequestDate = DateOnly.FromDateTime(DateTime.Now),
                StartDate = leaverequest.StartDate,
                EndDate = leaverequest.EndDate,
            };

            var leaverequestCreated = await _leaverequestRepository.CreateLeaverequestAsync(
                leaverequestTocreate
            );

            return new ReadLeaverequest()
            {
                Id = leaverequestCreated.LeaveRequestId,
                EmployeeId = (int)leaverequestCreated.EmployeeId,
                StatusId = (int)leaverequestCreated.StatusId,
                RequestDate = leaverequestCreated.RequestDate,
                StartDate = leaverequestCreated.StartDate,
                EndDate = leaverequestCreated.EndDate,
            };
        }

        public async Task<List<ReadLeaverequest>> GetLeaverequests()
        {
            var leaverequests = await _leaverequestRepository.GetLeaverequestsAsync();

            List<ReadLeaverequest> readLeaverequests = new List<ReadLeaverequest>();

            foreach (var leaverequest in leaverequests)
            {
                readLeaverequests.Add(
                    new ReadLeaverequest()
                    {
                        Id = leaverequest.LeaveRequestId,
                        EmployeeId = (int)leaverequest.EmployeeId,
                        StatusId = (int)leaverequest.StatusId,
                        RequestDate = leaverequest.RequestDate,
                        StartDate = leaverequest.StartDate,
                        EndDate = leaverequest.EndDate,
                    }
                );
            }

            return readLeaverequests;
        }

        public async Task<ReadLeaverequest> GetLeaverequestByIdAsync(int leaverequestId)
        {
            var leaverequest = await _leaverequestRepository.GetLeaverequestByIdAsync(
                leaverequestId
            );

            if (leaverequest is null)
                throw new Exception(
                    $"Echec de recupération des informations d'un leverequest car il n'existe pas : {leaverequestId}"
                );

            return new ReadLeaverequest()
            {
                Id = leaverequest.LeaveRequestId,
                EmployeeId = (int)leaverequest.EmployeeId,
                StatusId = (int)leaverequest.StatusId,
                RequestDate = leaverequest.RequestDate,
                StartDate = leaverequest.StartDate,
                EndDate = leaverequest.EndDate,
            };
        }

        public async Task<Leaverequest> UpdateLeaverequestAsync(
            int leaverequestId,
            UpdateLeaverequest leaverequest
        )
        {
            var leaverequestUpdate =
                await _leaverequestRepository.GetLeaverequestByIdAsync(leaverequestId)
                ?? throw new Exception(
                    $"Echec de mise à jour d'un leaverequest : Il n'existe aucun leaverequest avec cet identifiant : {leaverequestId}"
                );

            if (leaverequest.StartDate > leaverequest.EndDate)
            {
                throw new Exception(
                    $"Echec de création d'un leaverequest : EndDate inférieur à StartDare"
                );
            }

            leaverequestUpdate.EmployeeId = leaverequest.EmployeeId;
            leaverequestUpdate.StatusId = leaverequest.StatusId;
            leaverequestUpdate.StartDate = leaverequest.StartDate;
            leaverequestUpdate.EndDate = leaverequest.EndDate;
            leaverequestUpdate.RequestDate = DateOnly.FromDateTime(DateTime.Now);

            await _leaverequestRepository.UpdateLeaverequestAsync(leaverequestUpdate);

            return leaverequestUpdate;
        }

        public async Task<Leaverequest> DeleteLeaverequestById(int leaverequestId)
        {
            var leaverequestGet =
                await _leaverequestRepository.GetLeaverequestByIdWithIncludeAsync(leaverequestId)
                ?? throw new Exception(
                    $"Echec de suppression d'un leevrequest : Il n'existe aucun leaverequest avec cet identifiant : {leaverequestId}"
                );

            return await _leaverequestRepository.DeleteLeaverequestByIdAsync(leaverequestId);
        }
    }
}
