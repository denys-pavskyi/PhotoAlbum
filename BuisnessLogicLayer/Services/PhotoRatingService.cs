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
    public class PhotoRatingService: IPhotoRatingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PhotoRatingService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task AddAsync(PhotoRatingModel model)
        {
            ModelsValidation.PhotoRatingModelValidation(model);
            var mappedPhotoRating = _mapper.Map<PhotoRating>(model);

            await _unitOfWork.PhotoRatingRepository.AddAsync(mappedPhotoRating);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int modelId)
        {
            await _unitOfWork.PhotoRatingRepository.DeleteByIdAsync(modelId);
            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<PhotoRatingModel>> GetAllAsync()
        {
            IEnumerable<PhotoRating> unmappedPhotoRatings = await _unitOfWork.PhotoRatingRepository.GetAllWithDetailsAsync();
            return _mapper.Map<IEnumerable<PhotoRatingModel>>(unmappedPhotoRatings);
        }

        public async Task<PhotoRatingModel> GetByIdAsync(int id)
        {
            var unmappedPhotoRating = await _unitOfWork.PhotoRatingRepository.GetByIdWithDetailsAsync(id);
            return _mapper.Map<PhotoRatingModel>(unmappedPhotoRating);
        }

        public async Task UpdateAsync(PhotoRatingModel model)
        {
            ModelsValidation.PhotoRatingModelValidation(model);
            var mapped = _mapper.Map<PhotoRating>(model);
            _unitOfWork.PhotoRatingRepository.Update(mapped);
            await _unitOfWork.SaveAsync();
        }

        public async Task<PhotoRatingModel> HasUserRankedPhoto(int userId, int photoId)
        {
            IEnumerable<PhotoRating> photoRatings = await _unitOfWork.PhotoRatingRepository.GetAllWithDetailsAsync();
            var photoRating = photoRatings.FirstOrDefault(x=> x.UserId==userId&& x.PhotoId==photoId);
            return _mapper.Map<PhotoRatingModel>(photoRating);
        }
    }
}
