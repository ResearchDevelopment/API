using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShadiWebApplication.Models.DTO
{
    public class Vocabulary
    {
        public long Id { get; set; }
        public string Word { get; set; }
        public int? PartOfSpeech { get; set; }
        public long? DefinitionLanguageId { get; set; }
        public long? WordLanguageId { get; set; }
        public string Defintion { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
    }
}
