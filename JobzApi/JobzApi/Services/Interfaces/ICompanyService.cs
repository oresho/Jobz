using JobzApi.Models.Dtos.Requests;
using JobzApi.Models.Dtos.Responses;

namespace JobzApi.Services.Interfaces
{
    public interface ICompanyService
    {
        Task<GenericApiResponse> CreateCompanyAsync(CreateCompanyRequest companyRequest);
        Task<GenericApiResponse<PaginatedResponse<IEnumerable<CompanyResponse>>>> GetAllCompaniesAsync(int pageNo, int pageSize);
        Task<GenericApiResponse<IEnumerable<CompanyResponse>>> GetCompaniesByNameAsync(string name);
    }
}
