using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShadiWebApplication.DAL;
using ShadiWebApplication.Models.DTO;

namespace ShadiWebApplication.Controllers.API
{

    [Route("api/[controller]")]
    [ApiController]
    public class VocabularyController : ControllerBase
    {
        private Logger.ILogger Logger { get; }
        private IVocabularyRepository VocabularyRepository { get; }
        public VocabularyController(Logger.ILogger logger, IVocabularyRepository vocabularyRepository)
        {
            Logger = logger;
            VocabularyRepository = vocabularyRepository;

        }
        [HttpGet]
        public IActionResult GetVocabs()
        {
            var x = this.VocabularyRepository.GetVocabs(1, 10);
            return Ok(x);
            ////
        }

    }

}

