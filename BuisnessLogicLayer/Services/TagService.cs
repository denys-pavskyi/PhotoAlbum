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
    public class TagService:ITagService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TagService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task AddAsync(TagModel model)
        {
            ModelsValidation.TagModelValidation(model);
            var mappedTag = _mapper.Map<Tag>(model);

            await _unitOfWork.TagRepository.AddAsync(mappedTag);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int modelId)
        {
            await _unitOfWork.TagRepository.DeleteByIdAsync(modelId);
            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<TagModel>> GetAllAsync()
        {
            IEnumerable<Tag> unmappedTags = await _unitOfWork.TagRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<TagModel>>(unmappedTags);
        }

        public async Task<TagModel> GetByIdAsync(int id)
        {
            var unmappedTag = await _unitOfWork.TagRepository.GetByIdAsync(id);
            return _mapper.Map<TagModel>(unmappedTag);
        }

        public async Task UpdateAsync(TagModel model)
        {
            ModelsValidation.TagModelValidation(model);
            var mapped = _mapper.Map<Tag>(model);
            _unitOfWork.TagRepository.Update(mapped);
            await _unitOfWork.SaveAsync();
        }
    }
}
