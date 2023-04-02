using HexaShop.Common.CommonDtos;
using HexaShop.Common.Dtos;

namespace HexaShop.Application.Constracts.InfrastructureContracts
{
    public interface IFileRepository
    {
        ResultDto<string> UploadImageThroughBase64(FileDto<string> fileDto, string branchName);
        ResultDto<string> DeleteFile(string path);
    }
}
