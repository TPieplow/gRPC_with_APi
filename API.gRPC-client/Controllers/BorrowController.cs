using API.gRPC_client.Services;
using Grpc.Client;
using Microsoft.AspNetCore.Mvc;

namespace API.gRPC_client.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowController(GrpcService grpcService) : ControllerBase
    {
        private readonly GrpcService _grpcService = grpcService;

        [HttpPost("borrow")]
        public async Task<IActionResult> Borrow(BorrowRequest request)
        {
            try
            {
                var response = await _grpcService.BorrowAsync(request);
                if (response is not null)
                {
                    return Created("Successfully created", response);
                }
                return BadRequest("Inget roligt här inte");
            }
            catch (Exception) { return BadRequest(); }
        }

        [HttpPost("unborrow")]
        public async Task<IActionResult> UnBorrow(UnBorrowRequest request)
        {
            try
            {
                var response = await _grpcService.UnBorrowAsync(request);
                return Ok();
            }
            catch (Exception) { return BadRequest(); }
        }

    }
}
