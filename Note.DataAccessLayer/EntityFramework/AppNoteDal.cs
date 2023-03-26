﻿using Microsoft.EntityFrameworkCore;
using Note.DataAccessLayer.Abstract;
using Note.DataAccessLayer.Concrete;
using Note.DataAccessLayer.Repository;
using Note.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Note.DataAccessLayer.EntityFramework
{
    public class AppNoteDal : GenericDal<AppNote>, IAppNoteDal
    {
        public AppNoteDal(Context context) : base(context)
        {
        }

        public List<AppNote> NotesWithAuthor()
        {

            return _context.AppNotes.Include(x => x.AppUser).ToList();


        }

        public List<AppNote> NotesWithAuthorId(string id)
        {
            return _context.AppNotes.Where(x => x.AppUserId == id).Include(x => x.AppUser).ToList();
        }
    }
}
