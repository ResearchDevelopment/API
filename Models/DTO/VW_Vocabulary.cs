using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShadiWebApplication.Models.DTO
{
    public class VW_Vocabulary
    {
        public long? DefinitionLanguageId { get; set; }
        public int? PartOfSpeech { get; set; }
        public long? WordLanguageId { get; set; }
        public string Definition { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string Word { get; set; }
        public long Id { get; set; }
        public string Title { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }

    }
}
