using ASI.Basecode.Services.Interfaces;
using ASI.Basecode.Services.Services;
using ASI.Basecode.WebApp.Mvc;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ASI.Basecode.WebApp.Controllers
{
    [Route("api/[controller]/[action]")]
    [AllowAnonymous]
    public class KnowledgeBaseController : ControllerBase<KnowledgeBaseController>
    {
        private readonly IKnowledgeBaseService _knowledgeBaseService;

        public KnowledgeBaseController(
            IHttpContextAccessor httpContextAccessor,
            ILoggerFactory loggerFactory,
            IConfiguration configuration,
            IMapper mapper,
            IKnowledgeBaseService knowledgeBaseService
        ) : base(httpContextAccessor, loggerFactory, configuration, mapper)
        {
            _knowledgeBaseService = knowledgeBaseService;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
