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
    public class AlbumService: IAlbumService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AlbumService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task AddAsync(AlbumModel model)
        {
            ModelsValidation.AlbumModelValidation(model);
            var mappedAlbum = _mapper.Map<Album>(model);

            await _unitOfWork.AlbumRepository.AddAsync(mappedAlbum);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int modelId)
        {
            await _unitOfWork.AlbumRepository.DeleteByIdAsync(modelId);
            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<AlbumModel>> GetAllAsync()
        {
            IEnumerable<Album> unmappedAlbums = await _unitOfWork.AlbumRepository.GetAllWithDetailsAsync();
            return _mapper.Map<IEnumerable<AlbumModel>>(unmappedAlbums);
        }

        public async Task<AlbumModel> GetByIdAsync(int id)
        {
            var unmappedAlbum = await _unitOfWork.AlbumRepository.GetByIdWithDetailsAsync(id);
            return _mapper.Map<AlbumModel>(unmappedAlbum);
        }

        public async Task<IEnumerable<AlbumModel>> GetByUserIdAsync(int userId)
        {
            IEnumerable<Album> unmappedAlbums = await _unitOfWork.AlbumRepository.GetAllWithDetailsAsync();
            return _mapper.Map<IEnumerable<AlbumModel>>(unmappedAlbums.Where(x=>x.UserId==userId));
        }

        public async Task<PhotoModel> GetFirstPhotoByAlbumId(int albumId)
        {
            var unmappedAlbum = await _unitOfWork.AlbumRepository.GetByIdWithDetailsAsync(albumId);
  
            var firstPhotoUnmapped = await _unitOfWork.PhotoRepository.GetByIdWithDetailsAsync(unmappedAlbum.AlbumPhotos.First().PhotoId);
            return _mapper.Map<PhotoModel>(firstPhotoUnmapped);

        }

        public async Task UpdateAsync(AlbumModel model)
        {
            ModelsValidation.AlbumModelValidation(model);
            var mapped = _mapper.Map<Album>(model);
            _unitOfWork.AlbumRepository.Update(mapped);
            await _unitOfWork.SaveAsync();
        }
    }
}
