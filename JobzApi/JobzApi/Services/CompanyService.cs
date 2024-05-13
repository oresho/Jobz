using JobzApi.ExceptionHandler.CustomExceptions;
using JobzApi.Models.Dtos.Requests;
using JobzApi.Models.Dtos.Responses;
using JobzApi.Models.Entities;
using JobzApi.Services.Interfaces;

namespace JobzApi.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly IRepositoryService<Company> _companyRepository;
        public CompanyService(IRepositoryService<Company> companyRepository) 
        { 
            _companyRepository = companyRepository;
        }

        public static CompanyResponse MapToCompanyResponse(Company company)
        {
            return new CompanyResponse()
            {
                Id = company.Id,
                Name = company.Name,
                Description = company.Description,
                ContactEmail = company.ContactEmail,
                ContactPhone = company.ContactPhone
            };
        }

        public async Task<GenericApiResponse> CreateCompanyAsync(CreateCompanyRequest companyRequest)
        {
            var duplicateCompany = _companyRepository.FindByCondition(x => x.Name == companyRequest.Name).FirstOrDefault();
            if (duplicateCompany != null)
            {
                throw new BadRequestException("A company with this name already exists.");
            }

            var company = MapToCompany(companyRequest);

            using var transactionObject = await _companyRepository.GetTransactionObject();
            try
            {
                await _companyRepository.CreateAsync(company);
                await transactionObject.CommitAsync();
            }
            catch (Exception ex)
            {
                await transactionObject.RollbackAsync();
                throw new InternalServerException(ex.Message);
            }
            return new GenericApiResponse { Success = true, Message = "Successfully created new Company", StatusCode = 201 };
        }

        public static Company MapToCompany(CreateCompanyRequest companyRequest)
        {
            return new Company()
            {
                Name = companyRequest.Name, 
                Description = companyRequest.Description,
                ContactEmail = companyRequest.ContactEmail,
                ContactPhone = companyRequest.ContactPhone
            };
        }

        public async Task<GenericApiResponse<PaginatedResponse<IEnumerable<CompanyResponse>>>> GetAllCompaniesAsync(int pageNo, int pageSize)
        {
            var company = _companyRepository.FindByCondition(x => true);
            var companyResponse = company.Skip((pageNo - 1) * pageSize).Take(pageSize).Select(MapToCompanyResponse);
            int total = company.Count() % pageSize == 0 ? (company.Count() / pageSize) : (company.Count() / pageSize) + 1;
            var paginatedResponse = new PaginatedResponse<IEnumerable<CompanyResponse>>()
            {
                CurrentPage = pageNo,
                TotalPages = total,
                TotalResult = company.Count(),
                Data = companyResponse
            };
            return new GenericApiResponse<PaginatedResponse<IEnumerable<CompanyResponse>>>() { Success = true, Message = "Succesfully Gotten Companies", StatusCode = 200, Data = paginatedResponse };
        }

        public async Task<GenericApiResponse<IEnumerable<CompanyResponse>>> GetCompaniesByNameAsync(string name)
        {
            var company = _companyRepository.FindByCondition(x => x.Name.ToLower().Contains(name.ToLower())).Select(MapToCompanyResponse);
            return new GenericApiResponse<IEnumerable<CompanyResponse>>() { Success = true, Message = "Succesfully Gotten Companies", StatusCode = 200, Data = company };
        }
    }
}
