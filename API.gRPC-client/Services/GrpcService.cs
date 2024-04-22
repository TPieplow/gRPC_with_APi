using Grpc.Client;
using Grpc.Net.Client;

namespace API.gRPC_client.Services;

public class GrpcService
{
    private readonly GrpcChannel _channel;
    private readonly BorrowService.BorrowServiceClient _client;

    public GrpcService(string serverAddress)
    {
        _channel = GrpcChannel.ForAddress(serverAddress);
        _client = new BorrowService.BorrowServiceClient(_channel);
    }

    public async Task<BorrowResponse> BorrowAsync(BorrowRequest request)
    {
        var reponse = await _client.BorrowAsync(request);
        if (reponse is not null)
        {
        return reponse;
        }
        return new BorrowResponse
        {
            Message = "Ingen respons till dig här inte"
        };
    }

    public async Task<UnBorrowResponse> UnBorrowAsync(UnBorrowRequest request)
    {
        var response = await _client.UnBorrowAsync(request);
        return response;
    }
}
