using Caliburn.Micro;
using MyNotesUI.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;

namespace MyNotesUI.ViewModels
{
    public class ShellViewModel : Screen
    {
        private ObservableCollection<NoteModel> _notes = new ObservableCollection<NoteModel>();
        private int _id;
        private string _title;
        private string _note;

        public ObservableCollection<NoteModel> Notes
        {
            get { return _notes; }
            set { _notes = value; }
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; NotifyOfPropertyChange(() => Id); }
        }


        public string Title
        {
            get { return _title; }
            set { _title = value; NotifyOfPropertyChange(() => Title); }
        }

        public string Note
        {
            get { return _note; }
            set { _note = value; NotifyOfPropertyChange(() => Note); }
        }

        public void Load()
        {
            foreach (var note in Notes)
            {
                if (Id == note.Id)
                {
                    Title = note.Title;
                    Note = note.Note;
                }
            }
        }

        public void Save()
        {
            Notes.Add(new NoteModel { Id = Id, Title = Title, Note = Note, CreatedDate = DateTime.Now });
            Id++;
        }

        public void Clear()
        {
            Title = "";
            Note = "";
        }

        public void Delete()
        {
            for (int i = 0; i < Notes.Count; i++)
            {
                if (Id == Notes[i].Id)
                {
                    Notes.Remove(Notes[i]);
                }
            }
        }
    }
}
