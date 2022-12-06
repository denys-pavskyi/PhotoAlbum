using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace BuisnessLogicLayer.Validation
{
    [Serializable]
    public class InternetPhotoAlbumException:Exception
    {

        public InternetPhotoAlbumException()
        { }

        public InternetPhotoAlbumException(string message)
            : base(message)
        { }

        public InternetPhotoAlbumException(string message, Exception innerException)
            : base(message, innerException)
        { }

        protected InternetPhotoAlbumException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
