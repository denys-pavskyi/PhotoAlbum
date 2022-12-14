using BuisnessLogicLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLogicLayer.Interfaces
{
    public interface IAlbumService : ICrud<AlbumModel>
    {
        Task<IEnumerable<AlbumModel>> GetByUserIdAsync(int userId);
        Task<PhotoModel> GetFirstPhotoByAlbumId(int albumId);
        Task<IEnumerable<PhotoModel>> GetAlbumPhotosByAlbumId(int albumId);
    }
}
