using AutoMapper;
using BuisnessLogicLayer.Interfaces;
using BuisnessLogicLayer.Models;
using BuisnessLogicLayer.Validation;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLogicLayer.Services
{
    public class ReportService: IReportService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ReportService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task AddAsync(ReportModel model)
        {
            ModelsValidation.ReportModelValidation(model);
            var mappedReport = _mapper.Map<Report>(model);

            await _unitOfWork.ReportRepository.AddAsync(mappedReport);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int modelId)
        {
            await _unitOfWork.ReportRepository.DeleteByIdAsync(modelId);
            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<ReportModel>> GetAllAsync()
        {
            IEnumerable<Report> unmappedReports = await _unitOfWork.ReportRepository.GetAllWithDetailsAsync();
            return _mapper.Map<IEnumerable<ReportModel>>(unmappedReports);
        }

        public async Task<ReportModel> GetByIdAsync(int id)
        {
            var unmappedReport = await _unitOfWork.ReportRepository.GetByIdWithDetailsAsync(id);
            return _mapper.Map<ReportModel>(unmappedReport);
        }

        public async Task UpdateAsync(ReportModel model)
        {
            ModelsValidation.ReportModelValidation(model);
            var mapped = _mapper.Map<Report>(model);
            _unitOfWork.ReportRepository.Update(mapped);
            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<ReportModel>> GetReportsOnReview()
        {
            IEnumerable<Report> unmappedReports = await _unitOfWork.ReportRepository.GetAllWithDetailsAsync();
            return _mapper.Map<IEnumerable<ReportModel>>(unmappedReports.Where(x => x.Status==ReportStatus.OnReview));
        }
    }
}
