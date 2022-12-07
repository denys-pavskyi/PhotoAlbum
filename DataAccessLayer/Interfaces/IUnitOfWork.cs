using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IUnitOfWork
    {
        IAlbumPhotoRepository AlbumPhotoRepository { get; }
        IAlbumRepository AlbumRepository { get; }
        IPhotoRatingRepository PhotoRatingRepository { get; }
        IPhotoRepository PhotoRepository { get; }
        IPhotoTagRepository PhotoTagRepository { get; }
        ITagRepository TagRepository { get; }
        IUserRepository UserRepository { get; }
        IReportRepository ReportRepository { get; }
        Task SaveAsync();
    }
}
