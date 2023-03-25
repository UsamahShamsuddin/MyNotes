using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNotesUI.ViewModels
{
    public class ShellViewModel : Screen
    {
        public bool CanClear(string title, string note)
        {
            if (String.IsNullOrWhiteSpace(title) || String.IsNullOrWhiteSpace(note))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public void Clear(string title, string note)
        {
            TitleText = "";
            NoteText = "";
        }
    }
}
