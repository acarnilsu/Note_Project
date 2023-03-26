﻿using Note.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Note.BusinessLayer.Abstract
{
    public interface IAppNoteService:IGenericService<AppNote>
    {
        List<AppNote> TNotesWithAuthor(); 
        List<AppNote> TNotesWithAuthorId(string id);
    }
}
