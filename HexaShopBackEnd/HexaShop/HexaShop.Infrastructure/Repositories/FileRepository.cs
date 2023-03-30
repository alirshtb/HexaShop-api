using HexaShop.Application.Constracts.InfrastructureContracts;
using HexaShop.Common.CommonDtos;
using HexaShop.Common;
using Microsoft.AspNetCore.Hosting;
using HexaShop.Common.Dtos;

namespace HexaShop.Infrastructure.Repositories
{
    public class FileRepository : IFileRepository
    {
        private readonly IWebHostEnvironment _env;

        public FileRepository(IWebHostEnvironment webHostEnvironment)
        {
            _env = webHostEnvironment;
        }


        /// <summary>
        /// Uplaod a file through base64 type
        /// </summary>
        /// <param name="fileDto"></param>
        /// <returns></returns>
        public ResultDto<string> UploadImageThroughBase64(FileDto<string> fileDto, string branchName)
        {
            if (string.IsNullOrWhiteSpace(fileDto.FileExtension))
            {
                fileDto.FileExtension = ".jpg";
            }

            if (!IsValidFile(fileDto))
            {
                return new ResultDto<string>()
                {
                    IsSuccess = false,
                    Message = fileDto.FileExtension.ToString(),
                    ResultData = fileDto.FileExtension.ToString(),
                    Reason = Common.FailureReason.InvalidFileExtension
                };
            }

            // --- set base path for images --- //
            var basePath = _env.WebRootPath + "\\" + "Images" + "\\" + branchName;

            if (!Directory.Exists(basePath))
            {
                Directory.CreateDirectory(basePath);
            }

            // --- special path for every image --- //
            var fileName = DateTime.Now.Ticks.ToString() + "-" + fileDto.Name.ToUpper() + fileDto.FileExtension;


  
            var savePath = Path.Combine(basePath, fileName);

            // --- convert base64string to array of bytes --- //
            var fileBytes = Convert.FromBase64String(fileDto.File);


            try
            {
                // --- write file --- //
                File.WriteAllBytes(savePath, fileBytes);

                return new ResultDto<string>()
                {
                    IsSuccess = true,
                    Message = ApplicationMessages.FileUploaded,
                    ResultData = savePath
                };
            }
            catch (Exception)
            {
                return new ResultDto<string>()
                {
                    IsSuccess = false,
                    Message = fileDto.FileExtension.ToString(),
                    ResultData = fileDto.FileExtension.ToString(),
                    Reason = Common.FailureReason.UnSuccessful
                };
            }

        }





        /// <summary>
        /// validate file extension.
        /// </summary>
        /// <param name="fileDto"></param>
        /// <returns></returns>
        private bool IsValidFile(FileDto<string> fileDto)
        {
            var isValid = true;

            if (fileDto.FileExtension.ToLower() != ".jpg" &&
               fileDto.FileExtension.ToLower() != ".jpeg" &&
               fileDto.FileExtension.ToLower() != ".png")
            {
                isValid = false;
            }

            return isValid;
        }



    }
}
