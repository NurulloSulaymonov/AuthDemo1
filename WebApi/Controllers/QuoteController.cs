using System.Net;
using Domain.Dtos;
using Domain.Response;
using Infrastructure.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("[controller]")]
public class QuoteController  :ControllerBase
{
    private readonly IQuoteService _quoteService;

    public QuoteController(IQuoteService quoteService)
    {
        _quoteService = quoteService;
    }

    [HttpGet("get-quotes")]
    public async Task<Response<List<GetQuoteDto>>> GetQuotes()
    {
        return await _quoteService.GetQuotes();
    }
    
    
    [HttpPost("add-quote")]
    public async Task<IActionResult> AddQuote([FromBody]AddQuoteDto quoteDto)
    {
        if (ModelState.IsValid)
        {
            var response = await _quoteService.AddQuote(quoteDto);
            return StatusCode(response.StatusCode, response);
        }
        else
        {
            var errors = ModelState.SelectMany(e => e.Value.Errors.Select(er=>er.ErrorMessage)).ToList();
            var response  =  new Response<GetQuoteDto>(HttpStatusCode.BadRequest, errors);
            return StatusCode(response.StatusCode, response);
        }
        
    }
    
    
    [HttpPut("update-quote")]
    public async Task<Response<GetQuoteDto>> UpdateQuote(AddQuoteDto quoteDto)
    {
        return await _quoteService.UpdateQuote(quoteDto);
    }
    
    [HttpDelete("delete-quote")]
    public async Task<IActionResult> AddQuote(int  quoteId)
    {
      var response = await _quoteService.DeleteQuote(quoteId);
      return StatusCode(response.StatusCode, response);
    }
}