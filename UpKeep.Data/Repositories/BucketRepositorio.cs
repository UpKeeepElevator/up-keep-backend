using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Microsoft.Extensions.Options;
using Serilog;
using UpKeep.Data.Configuration;
using UpKeep.Data.Contracts;
using UpKeep.Data.DTO;
using S3Object = UpKeep.Data.DTO.S3Object;

namespace UpKeep.Data.Repositories;

public class BucketRepositorio : IBucketRepositorio
{
    private readonly IOptions<BucketConfig> _bucketOptions;

    public BucketRepositorio(IOptions<BucketConfig> storageOptions)
    {
        _bucketOptions = storageOptions;
    }

    private AmazonS3Client SetupClient()
    {
        //aws credentials
        var credentials =
            new BasicAWSCredentials(_bucketOptions.Value.AWSAccessKey, _bucketOptions.Value.AWSSecretKey);
        var config = new AmazonS3Config()
        {
            ServiceURL = _bucketOptions.Value.VultrUrl
        };

        return new AmazonS3Client(credentials, config);
    }

    public async Task<S3ResponseDto> UploadFileAsync(S3Object s3Obj)
    {
        Log.Information($"Uploading {s3Obj.Name} to {_bucketOptions.Value.S3Bucket}");

        var response = new S3ResponseDto();

        try
        {
            await using var memoryStream = new MemoryStream();
            await s3Obj.InputStream.CopyToAsync(memoryStream);
            var uploadRequest = new TransferUtilityUploadRequest()
            {
                InputStream = memoryStream,
                Key = s3Obj.Name,
                BucketName = _bucketOptions.Value.S3Bucket,
                CannedACL = S3CannedACL.PublicRead
            };

            //s3 client
            using var client = this.SetupClient();
            var transferUtility = new TransferUtility(client);
            await transferUtility.UploadAsync(uploadRequest);

            response.StatusCode = 200;
            response.Message = $"{s3Obj.Name} has benn upladed succesfully";
        }
        catch (AmazonS3Exception ex)
        {
            Log.Error($"an error has occured in {nameof(UploadFileAsync)} \n {(int)ex.StatusCode} :{ex.Message}");
            response.StatusCode = (int)ex.StatusCode;
            response.Message = ex.Message;
        }
        catch (Exception ex)
        {
            Log.Error(ex, $"an error has occured in {nameof(UploadFileAsync)}");
            response.StatusCode = 500;
            response.Message = ex.Message;
        }

        return response;
    }


    public async Task<S3ResponseDto> DeleteFileAsync(S3Object s3Obj)
    {
        var response = new S3ResponseDto();

        try
        {
            var deleteObjectRequest = new DeleteObjectRequest()
            {
                Key = s3Obj.Name,
                BucketName = _bucketOptions.Value.S3Bucket,
            };

            using var client = SetupClient();
            Log.Information($"Deleting {s3Obj.Name} From {_bucketOptions.Value.S3Bucket}");
            await client.DeleteObjectAsync(deleteObjectRequest);

            response.StatusCode = 200;
            response.Message = $"Deleted {s3Obj.Name} From {_bucketOptions.Value.S3Bucket}";
        }
        catch (Exception ex)
        {
            Log.Error(ex, $"an error has occured in {nameof(DeleteFileAsync)}");
            response.StatusCode = 500;
            response.Message = ex.Message;
        }


        return response;
    }
}