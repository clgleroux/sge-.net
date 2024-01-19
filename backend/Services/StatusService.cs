using System.Xml;
using backend.Dto.Status;
using backend.Entities;
using backend.Repositories;

namespace backend.Services
{
    public class StatusService
    {
        private readonly StatusRepository _statusRepository;

        public StatusService(StatusRepository statusRepository)
        {
            _statusRepository = statusRepository;
        }

        public async Task<List<ReadStatus>> GetStatuses()
        {
            var statuses = await _statusRepository.GetStatusesAsync();

            List<ReadStatus> readStatus = new List<ReadStatus>();

            foreach (var status in statuses)
            {
                readStatus.Add(
                    new ReadStatus() { Id = status.StatusId, Name = status.StatusName, }
                );
            }

            return readStatus;
        }

        public async Task<ReadStatus> GetStatusByIdAsync(int statusId)
        {
            var status = await _statusRepository.GetStatusByIdAsync(statusId);

            if (status is null)
                throw new Exception(
                    $"Echec de recupération des informations d'un status car il n'existe pas : {statusId}"
                );

            return new ReadStatus() { Id = status.StatusId, Name = status.StatusName, };
        }
    }
}
