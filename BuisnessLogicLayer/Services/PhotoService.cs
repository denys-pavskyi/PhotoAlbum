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
    public class PhotoService: IPhotoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PhotoService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task AddAsync(PhotoModel model)
        {
            ModelsValidation.PhotoModelValidation(model);
            var mappedPhoto = _mapper.Map<Photo>(model);


            await _unitOfWork.PhotoRepository.AddAsync(mappedPhoto);
            
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int modelId)
        {
            await _unitOfWork.PhotoRepository.DeleteByIdAsync(modelId);
            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<PhotoModel>> GetAllAsync()
        {
            IEnumerable<Photo> unmappedPhotos = await _unitOfWork.PhotoRepository.GetAllWithDetailsAsync();
            return _mapper.Map<IEnumerable<PhotoModel>>(unmappedPhotos);
        }

        public async Task<PhotoModel> GetByIdAsync(int id)
        {
            var unmappedPhoto = await _unitOfWork.PhotoRepository.GetByIdWithDetailsAsync(id);
            return _mapper.Map<PhotoModel>(unmappedPhoto);
        }

        public async Task UpdateAsync(PhotoModel model)
        {
            ModelsValidation.PhotoModelValidation(model);
            var mapped = _mapper.Map<Photo>(model);
            _unitOfWork.PhotoRepository.Update(mapped);
            await _unitOfWork.SaveAsync();
        }
    }
}
