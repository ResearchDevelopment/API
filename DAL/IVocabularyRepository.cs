using ShadiWebApplication.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShadiWebApplication.DAL
{
    public interface IVocabularyRepository
    {
        List<VW_Vocabulary> GetVocabs(int page, int pageSize);

    }
}
