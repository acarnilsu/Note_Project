using Note.EntityLayer.Concrete;

namespace Note.Web.ViewModels
{
    public class AppNoteListVM
    {
        public string ID { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        public bool Status { get; set; }

        public AppUser AppUser { get; set; }

        public string AppUserId { get; set; }

        public string ImageUrl { get; set; }



    }
}
