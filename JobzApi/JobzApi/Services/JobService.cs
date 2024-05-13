using JobzApi.ExceptionHandler.CustomExceptions;
using JobzApi.Models.Dtos.Requests;
using JobzApi.Models.Dtos.Responses;
using JobzApi.Models.Entities;
using JobzApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JobzApi.Services
{
    public class JobService : IJobService
    {
        private readonly IRepositoryService<Job> _jobRepository;

        public JobService(IRepositoryService<Job> jobRepository)
        {
            _jobRepository = jobRepository;
        }

        public async Task<GenericApiResponse> CreateJobAsync(CreateJobRequest createJobRequest)
        {
            var duplicateJob = _jobRepository.FindByCondition(x => x.Title == createJobRequest.Title &&
                                                x.Description == createJobRequest.Description &&
                                                x.Location == createJobRequest.Location &&
                                                x.Type == createJobRequest.Type &&
                                                x.Salary == createJobRequest.Salary &&
                                                x.CompanyId == createJobRequest.CompanyId).FirstOrDefault();
            if (duplicateJob != null)
            {
                throw new BadRequestException("Duplicate jobs are not allowed");
            }

            var job = MapToJob(createJobRequest);
            using var transactionObject = await _jobRepository.GetTransactionObject();
            try 
            {
                await _jobRepository.CreateAsync(job);
                await transactionObject.CommitAsync();
            }
            catch(Exception ex) 
            {
                await transactionObject.RollbackAsync();
                throw new InternalServerException(ex.Message);
            }
            return new GenericApiResponse { Success = true, Message = "Successfully created new Job", StatusCode = 201 };
        }

        private static Job MapToJob(CreateJobRequest createJobRequest)
        {
            return new Job()
            {
                Title = createJobRequest.Title,
                Type = createJobRequest.Type,
                Description = createJobRequest.Description,
                Location = createJobRequest.Location,
                Salary = createJobRequest.Salary,
                CompanyId = createJobRequest.CompanyId,
            };
        }

        public async Task<GenericApiResponse<PaginatedResponse<IEnumerable<JobResponse>>>> GetAllJobsAsync(int pageNo, int pageSize)
        {
            var jobs = _jobRepository.FindByCondition(x => true).OrderByDescending(x => x.Created).Include(x => x.Company);
            var jobResponse = jobs.Skip((pageNo - 1) * pageSize).Take(pageSize).Select(MapToJobResponse);
            int total = jobs.Count() % pageSize == 0 ? (jobs.Count()/ pageSize) : (jobs.Count() / pageSize) + 1;
            var paginatedResponse = new PaginatedResponse<IEnumerable<JobResponse>>()
            {
                CurrentPage = pageNo,
                TotalPages = total,
                TotalResult = jobs.Count(),
                Data = jobResponse
            };
            return new GenericApiResponse<PaginatedResponse<IEnumerable<JobResponse>>> () { Success = true, Message = "Succesfully Gotten Jobs", StatusCode = 200, Data = paginatedResponse };
        }

        public static JobResponse MapToJobResponse(Job job)
        {
            return new JobResponse() 
            { 
                Id = job.Id,
                Title = job.Title,
                Type = job.Type,
                Description = job.Description,
                Location = job.Location,
                Salary = job.Salary,
                Company = CompanyService.MapToCompanyResponse(job.Company)
            };
        }

        public async Task<GenericApiResponse<JobResponse>> GetJobAsync(int jobId)
        {
            var job = _jobRepository.FindByCondition(x => x.Id == jobId).Include(x => x.Company).FirstOrDefault();
            if(job == null)
            {
                throw new ResourceNotFoundException("Job with this ID does not exist");
            }

            return new GenericApiResponse<JobResponse> { Success = true, Message = "Successfully gotten Job", StatusCode = 200, Data = MapToJobResponse(job) };
        }

        public async Task<GenericApiResponse> DeleteJobAsync(int jobId)
        {
            var job = _jobRepository.FindByCondition(x => x.Id == jobId).Include(x => x.Company).FirstOrDefault();
            if (job == null)
            {
                throw new ResourceNotFoundException("Job with this ID does not exist");
            }
            await _jobRepository.DeleteAsync(job);
            return new GenericApiResponse { Success = true, Message = "Successfully Deleted Job", StatusCode = 200 };
        }
    }
}
