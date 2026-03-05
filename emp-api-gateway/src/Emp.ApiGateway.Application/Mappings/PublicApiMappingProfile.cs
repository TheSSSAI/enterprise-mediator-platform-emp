using AutoMapper;
using Emp.ApiGateway.Application.DTOs.Internal;
using Emp.ApiGateway.Application.DTOs.Public;

namespace Emp.ApiGateway.Application.Mappings
{
    /// <summary>
    /// Defines mapping configurations between Internal Microservice DTOs and Public API ViewModels.
    /// This ensures decoupling between internal data structures and external contracts.
    /// </summary>
    public class PublicApiMappingProfile : Profile
    {
        public PublicApiMappingProfile()
        {
            // Project Mappings
            CreateMap<InternalProjectDto, PublicProjectDto>()
                .ForMember(dest => dest.ProjectId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ProjectName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.CurrentStatus, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.ClientName, opt => opt.MapFrom(src => src.ClientName))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate));

            // Financial Mappings
            CreateMap<FinancialSummaryDto, PublicFinancialSummaryDto>()
                .ForMember(dest => dest.TotalBudget, opt => opt.MapFrom(src => src.BudgetAmount))
                .ForMember(dest => dest.TotalInvoiced, opt => opt.MapFrom(src => src.InvoicedAmount))
                .ForMember(dest => dest.RemainingBudget, opt => opt.MapFrom(src => src.BudgetAmount - src.InvoicedAmount))
                .ForMember(dest => dest.CurrencyCode, opt => opt.MapFrom(src => src.Currency));
        }
    }
}