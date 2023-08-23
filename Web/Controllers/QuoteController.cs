using Domain.Filters;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

public class QuoteController:Controller
{
    private readonly IQuoteService _quoteService;

    public QuoteController(IQuoteService quoteService)
    {
        _quoteService = quoteService;
    }

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Index(GetQuoteFilter filter)
    {
        var response = await _quoteService.GetQuotes(filter);
        return View(response);
    }
    
    
    
}