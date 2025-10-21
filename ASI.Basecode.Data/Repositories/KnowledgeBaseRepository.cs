using ASI.Basecode.Data.Interfaces;
using Basecode.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASI.Basecode.Data.Repositories
{
    public class KnowledgeBaseRepository : BaseRepository, IKnowledgeBaseRepository
    {
        public KnowledgeBaseRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
