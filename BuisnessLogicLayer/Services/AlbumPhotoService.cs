using AutoMapper;
using BuisnessLogicLayer.Interfaces;
using BuisnessLogicLayer.Models;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuisnessLogicLayer.Validation;
using DataAccessLayer.Entities;

namespace BuisnessLogicLayer.Services
{
    public class AlbumPhotoService : IAlbumPhotoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public AlbumPhotoService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task AddAsync(AlbumPhotoModel model)
        {
            ModelsValidation.AlbumPhotoModelValidation(model);
            var mappedAlbumPhoto = _mapper.Map<AlbumPhoto>(model);

            await _unitOfWork.AlbumPhotoRepository.AddAsync(mappedAlbumPhoto);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int modelId)
        {
            await _unitOfWork.AlbumPhotoRepository.DeleteByIdAsync(modelId);
            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<AlbumPhotoModel>> GetAllAsync()
        {
            IEnumerable<AlbumPhoto> unmappedAlbumPhotos = await _unitOfWork.AlbumPhotoRepository.GetAllWithDetailsAsync();
            return _mapper.Map<IEnumerable<AlbumPhotoModel>>(unmappedAlbumPhotos);
        }

        public async Task<AlbumPhotoModel> GetByIdAsync(int id)
        {
            var unmappedAlbumPhoto = await _unitOfWork.AlbumPhotoRepository.GetByIdWithDetailsAsync(id);
            return _mapper.Map<AlbumPhotoModel>(unmappedAlbumPhoto  );
        }

        public async Task UpdateAsync(AlbumPhotoModel model)
        {
            ModelsValidation.AlbumPhotoModelValidation(model);
            var mapped = _mapper.Map<AlbumPhoto>(model);
            _unitOfWork.AlbumPhotoRepository.Update(mapped);
            await _unitOfWork.SaveAsync();
        }
    }
}
