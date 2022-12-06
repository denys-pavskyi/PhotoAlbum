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

        public Task DeleteAsync(int modelId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<AlbumPhotoModel>> GetAllAsync()
        {
            IEnumerable<AlbumPhoto> unmappedAlbumPhotos = await _unitOfWork.AlbumPhotoRepository.GetAllWithDetailsAsync();
            return _mapper.Map<IEnumerable<AlbumPhotoModel>>(unmappedAlbumPhotos);
        }

        public Task<AlbumPhotoModel> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(AlbumPhotoModel model)
        {
            throw new NotImplementedException();
        }
    }
}
