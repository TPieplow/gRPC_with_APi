using Grpc.Core;
using Grpc.Server.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Grpc.Server.Services;

public class BorrowService(DataContext context) : Server.BorrowService.BorrowServiceBase
{
    private readonly DataContext _context = context;

    public override async Task<BorrowResponse> Borrow(BorrowRequest request, ServerCallContext context)
    {
        if (!await _context.BorrowRequests.AnyAsync(x => x.Name == request.Name))
        {
            _context.BorrowRequests.Add(request);
            await _context.SaveChangesAsync();
            return new BorrowResponse()
            {
                Message = $"Borrow request successful"
            };
        }
        return new BorrowResponse()
        {
            Message = "This book is not available"
        };
    }

    public override async Task<UnBorrowResponse> UnBorrow(UnBorrowRequest request, ServerCallContext context)
    {
        var borrowRequest = await _context.BorrowRequests.FirstOrDefaultAsync(x => x.Name == request.Name);

        if (borrowRequest is not null)
        {
            _context.BorrowRequests.Remove(borrowRequest);
            await _context.SaveChangesAsync();
            return new UnBorrowResponse()
            {
                Message = "The book is unborrowed"
            };
        }
        return new UnBorrowResponse()
        {
            Message = "You havent borrowed this book"
        };
    }
}
