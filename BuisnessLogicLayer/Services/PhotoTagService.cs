using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BuisnessLogicLayer.Interfaces;
using BuisnessLogicLayer.Models;
using DataAccessLayer.Interfaces;
using BuisnessLogicLayer.Validation;
using DataAccessLayer.Entities;

namespace BuisnessLogicLayer.Services
{
    public class PhotoTagService: IPhotoTagService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PhotoTagService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task AddAsync(PhotoTagModel model)
        {
            ModelsValidation.PhotoTagModelValidation(model);
            var mappedPhotoTag = _mapper.Map<PhotoTag>(model);

            await _unitOfWork.PhotoTagRepository.AddAsync(mappedPhotoTag);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int modelId)
        {
            await _unitOfWork.PhotoTagRepository.DeleteByIdAsync(modelId);
            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<PhotoTagModel>> GetAllAsync()
        {
            IEnumerable<PhotoTag> unmappedPhotoTags = await _unitOfWork.PhotoTagRepository.GetAllWithDetailsAsync();
            return _mapper.Map<IEnumerable<PhotoTagModel>>(unmappedPhotoTags);
        }

        public async Task<PhotoTagModel> GetByIdAsync(int id)
        {
            var unmappedPhotoTag = await _unitOfWork.PhotoTagRepository.GetByIdWithDetailsAsync(id);
            return _mapper.Map<PhotoTagModel>(unmappedPhotoTag);
        }

        public async Task UpdateAsync(PhotoTagModel model)
        {
            ModelsValidation.PhotoTagModelValidation(model);
            var mapped = _mapper.Map<PhotoTag>(model);
            _unitOfWork.PhotoTagRepository.Update(mapped);
            await _unitOfWork.SaveAsync();
        }
    }
}
