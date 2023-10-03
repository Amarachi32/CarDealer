using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car.BLLayer.Interfaces
{
    public interface IPhotoServices
    {
        Task<string> AddPhotoAsync([FromForm] IFormFile file);
        Task<DeletionResult> DeletePhotoAsync(string publicId);
    }
}
