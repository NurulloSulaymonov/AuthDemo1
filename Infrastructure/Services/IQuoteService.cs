using Domain.Dtos;
using Domain.Response;

namespace Infrastructure.Services;

public interface IQuoteService
{
    Task<Response<List<GetQuoteDto>>> GetQuotes();
    Task<Response<GetQuoteDto>> GetQuoteById(int id);
    Task<Response<GetQuoteDto>> AddQuote(AddQuoteDto quoteDto);
    Task<Response<GetQuoteDto>> UpdateQuote(AddQuoteDto quoteDto);
    Task<Response<bool>> DeleteQuote(int id);
    
}