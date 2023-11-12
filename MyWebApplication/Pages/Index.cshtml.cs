using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using MyWebApplication.Data;
using MyWebApplication.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApplication.Pages
{
    [IgnoreAntiforgeryToken]
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        private readonly MyApplicationContext _context;

        private readonly KafkaProducerService<Null, string> _producer;

        public List<User> Users { get; set; }

        public IndexModel(ILogger<IndexModel> logger, MyApplicationContext context, KafkaProducerService<Null, string> producer)
        {
            _logger = logger;
            _context = context;
            _producer = producer;
        }

        public void OnGet()
        {
            Users = _context.Users.ToList();
        }


        public async Task OnPost()
        {
            Users = _context.Users.ToList();

            await _producer.ProduceAsync("sportsof", new Message<Null, string> { Value = "hi" });
        }
    }
}