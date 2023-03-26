using Note.BusinessLayer.Abstract;
using Note.DataAccessLayer.Abstract;
using Note.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Note.BusinessLayer.Concrete
{
    public class AppNoteService : GenericService<AppNote>, IAppNoteService
    {
        private readonly IAppNoteDal _appNoteDal;


        public AppNoteService(IGenericDal<AppNote> genericDal, IUnitOfWork unitOfWork, IAppNoteDal appNoteDal) : base(genericDal, unitOfWork)
        {
           _appNoteDal = appNoteDal;
        }

        public List<AppNote> TNotesWithAuthor()
        {
            return _appNoteDal.NotesWithAuthor();
        }

        public List<AppNote> TNotesWithAuthorId(string id)
        {
            return _appNoteDal.NotesWithAuthorId(id);
        }
    }
}
