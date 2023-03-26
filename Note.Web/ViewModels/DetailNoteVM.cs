using Note.EntityLayer.Concrete;

namespace Note.Web.ViewModels
{
    public class DetailNoteVM
    {
        public string ID { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime CreatedDate { get; set; }

        public string AppUserId { get; set; }

        public string ImageUrl { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

    }
}
