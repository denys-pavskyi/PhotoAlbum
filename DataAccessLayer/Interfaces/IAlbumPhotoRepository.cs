﻿using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IAlbumPhotoRepository: IRepository<AlbumPhoto>
    {
        Task<IEnumerable<AlbumPhoto>> GetAllWithDetailsAsync();
        Task<AlbumPhoto> GetByIdWithDetailsAsync(int id);
    }
}
