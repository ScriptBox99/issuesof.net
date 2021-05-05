﻿using System.Linq;

using IssuesOfDotNet.Data;
using IssuesOfDotNet.Querying;

using Microsoft.AspNetCore.Mvc;

namespace IssuesOfDotNet.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class CompletionController : Controller
    {
        private readonly CrawledIndexCompletionService _completionService;

        public CompletionController(CrawledIndexCompletionService completionService)
        {
            _completionService = completionService;
        }

        [HttpGet]
        public CompletionResponse GetCompletions(string q, int pos)
        {
            var syntax = QuerySyntax.Parse(q);
            var result = _completionService.Provider.Complete(syntax, pos);

            if (result is null)
                return null;

            return new CompletionResponse
            {
                List = result.Completions.Take(50).ToArray(),
                From = result.Span.Start,
                To = result.Span.End
            };
        }

        public sealed class CompletionResponse
        {
            public string[] List { get; set; }
            public int From { get; set; }
            public int To { get; set; }
        }
    }
}
