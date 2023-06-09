﻿using HexaShop.Application.Constracts.InfrastructureContracts;
using HexaShop.Common.CommonDtos;
using HexaShop.Common;
using Microsoft.AspNetCore.Hosting;
using HexaShop.Common.Dtos;
using HexaShop.Common.Constants;
using HexaShop.Common.CommonExtenstionMethods;

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
                fileDto.FileExtension = ImageExtensions.JPG;
            }

            if (!IsValidFile(fileDto))
            {
                ExceptionHelpers.ThrowException(ApplicationMessages.InValidFileExtension);
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

            try
            {
                // --- convert base64string to array of bytes --- //
                var fileBytes = Convert.FromBase64String(fileDto.File);

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
                    Message = ApplicationMessages.FailUpload,
                    ResultData = fileDto.FileExtension.ToString(),
                };
            }

        }

        /// <summary>
        /// delete a file using path
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public ResultDto<string> DeleteFile(string path)
        {
            if(!File.Exists(path))
            {
                ExceptionHelpers.ThrowException(ApplicationMessages.FileNotFound);
            }

            File.Delete(path);

            return new ResultDto<string>()
            {
                IsSuccess = true,
                Message = ApplicationMessages.FileDeleted,
                ResultData = path,
            };
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
