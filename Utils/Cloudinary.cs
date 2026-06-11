using Backend.Settings;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;
using Backend.Application.CourseSection.DTOs;

namespace Backend.Utils
{
    public interface ICloudinaryUtils
    {
        Task<UploadResultDto> UploadFile(IFormFile file);
        Task<bool> DeleteFile(string publicId, bool flag);
    }

    public class CloudinaryUtils : ICloudinaryUtils
    {
        private readonly Cloudinary _cloudinary;

        public CloudinaryUtils(IOptions<CloudinarySettings> options)
        {
            var account = new Account(
                options.Value.CloudName,
                options.Value.ApiKey,
                options.Value.ApiSecret
            );

            _cloudinary = new Cloudinary(account);
        }

        public async Task<UploadResultDto> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new Exception("No file uploaded");

            using var stream = file.OpenReadStream();

            var uploadParams = new AutoUploadParams
            {
                File = new FileDescription(file.FileName, stream),
                Folder = "Studiness",

                Transformation = new Transformation()
                    .Quality("70")
                    .FetchFormat("auto")
            };

            var result = await _cloudinary.UploadAsync(uploadParams);

            if (result.Error != null)
                throw new Exception(result.Error.Message);

            return new UploadResultDto(result.SecureUrl.ToString(), result.PublicId);
        }

        public async Task<bool> DeleteFile(string publicId, bool flag)
        {
            if (string.IsNullOrWhiteSpace(publicId))
                return false;

            var deleteParams = new DeletionParams(publicId)
            {
                ResourceType = flag ? ResourceType.Video : ResourceType.Image
            };

            var result = await _cloudinary.DestroyAsync(deleteParams);

            return result.Result == "ok";
        }
    }

    
}