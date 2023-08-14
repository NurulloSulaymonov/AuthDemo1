using System.Net;
using AutoMapper;
using Domain.Dtos;
using Domain.Entities;
using Domain.Response;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class QuoteService : IQuoteService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public QuoteService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<Response<List<GetQuoteDto>>> GetQuotes()
    {
       
        // var linq = await (
        //     from q in _context.Quotes
        //     join c in _context.Categories on q.CategoryId equals c.Id
        //     select new GetQuoteDto()
        //     {
        //         Id = q.Id,
        //         AuthorName = q.Author,
        //         Title = q.QuoteText,
        //         CreatedAt = q.CreatedAt,
        //         ImageName = q.ImageName,
        //         CategoryName = c.Name
        //     }).ToListAsync();
        var quotes = _context.Quotes;
        var mapped = _mapper.Map<List<GetQuoteDto>>(quotes.ToList());
        return new Response<List<GetQuoteDto>>(mapped);
        // var efcore = await _context.Quotes.Select(q => new GetQuoteDto()
        // {
        //     Id = q.Id,
        //     AuthorName = q.Author,
        //     Title = q.QuoteText,
        //     CreatedAt = q.CreatedAt,
        //     ImageName = q.ImageName,
        //     CategoryName = q.Category.Name
        // }).ToListAsync();
        // return efcore;
        // return linq;
    }

    public async Task<Response<GetQuoteDto>> GetQuoteById(int id)
    {
    
        var quote = await _context.Quotes.FirstOrDefaultAsync(q => q.Id == id);
        var mapped = _mapper.Map<GetQuoteDto>(quote);
        
        return new Response<GetQuoteDto>(mapped) ;
    }

    public async Task<Response<GetQuoteDto>> AddQuote(AddQuoteDto quoteDto)
    {
        var quote = _mapper.Map<Quote>(quoteDto);

        await _context.Quotes.AddAsync(quote);
       await _context.SaveChangesAsync();

       var mapped = _mapper.Map<GetQuoteDto>(quote);
       return new Response<GetQuoteDto>(mapped);
    }

    public async Task<Response<GetQuoteDto>> UpdateQuote(AddQuoteDto quoteDto)
    {
        var quote = _mapper.Map<Quote>(quoteDto);
        _context.Quotes.Update(quote);
        await _context.SaveChangesAsync();
        
         var mapped = _mapper.Map<GetQuoteDto>(quote);
         return new Response<GetQuoteDto>(mapped);

    }

    public async Task<Response<bool>> DeleteQuote(int id)
    {
        var existing = await _context.Quotes.FindAsync(id);
        if (existing == null) return new Response<bool>(HttpStatusCode.BadRequest,"Quote not found");
        
        _context.Quotes.Remove(existing);
        await _context.SaveChangesAsync();
        return new Response<bool>(true);

    }
}