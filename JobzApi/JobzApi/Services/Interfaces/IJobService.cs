using JobzApi.Models.Dtos.Requests;
using JobzApi.Models.Dtos.Responses;

namespace JobzApi.Services.Interfaces
{
    public interface IJobService
    {
        Task<GenericApiResponse> CreateJobAsync(CreateJobRequest createJobRequest);
        Task<GenericApiResponse<PaginatedResponse<IEnumerable<JobResponse>>>> GetAllJobsAsync(int pageNo, int pageSize);
        Task<GenericApiResponse<JobResponse>> GetJobAsync(int jobId);
        Task<GenericApiResponse> DeleteJobAsync(int jobId);
    }
}
