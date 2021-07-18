using BookWorld.DAL.Abstract;
using BookWorld.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookWorld.BLL.Abstract
{
    public interface IBookRepository: IGeneralRepository<Books>
    {
    }
}
