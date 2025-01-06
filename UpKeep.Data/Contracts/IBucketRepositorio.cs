using UpKeep.Data.DTO;

namespace UpKeep.Data.Contracts;

public interface IBucketRepositorio
{
    Task<S3ResponseDto> UploadFileAsync(S3Object s3Obj);
    Task<S3ResponseDto> DeleteFileAsync(S3Object s3Obj);
}