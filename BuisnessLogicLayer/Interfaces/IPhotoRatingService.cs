using BuisnessLogicLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLogicLayer.Interfaces
{
    public interface IPhotoRatingService : ICrud<PhotoRatingModel>
    {
        Task<PhotoRatingModel> HasUserRankedPhoto(int userId, int photoId);
    }
}
