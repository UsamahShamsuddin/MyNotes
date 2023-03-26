using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNotesLibrary.Models
{
    public class NoteModel
    {
        public int Id { get; set; } = 0;
        public string Title { get; set; }
        public string Note { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
