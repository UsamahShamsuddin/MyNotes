using Caliburn.Micro;
using Microsoft.Extensions.Configuration;
using MyNotesLibrary;
using MyNotesLibrary.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace MyNotesUI.ViewModels
{
    public class ShellViewModel : Screen
    {
        SqliteCrud sql = new SqliteCrud(GetConnectionString());
        private ObservableCollection<NoteModel> _notes = new ObservableCollection<NoteModel>();
        private string _id;
        private string _title;
        private string _note;

        public ShellViewModel()
        {
            LoadFromDB();
        }

        public ObservableCollection<NoteModel> Notes
        {
            get { return _notes; }
            set { _notes = value; }
        }

        public string Id
        {
            get { return _id; }
            set
            {
                _id = value;
                NotifyOfPropertyChange(() => Id);
                NotifyOfPropertyChange(() => CanClear);
                NotifyOfPropertyChange(() => CanSave);
                NotifyOfPropertyChange(() => CanUpdateById);
                NotifyOfPropertyChange(() => CanLoadById);
                NotifyOfPropertyChange(() => CanDeleteById);
            }
        }

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                NotifyOfPropertyChange(() => Title);
                NotifyOfPropertyChange(() => CanClear);
                NotifyOfPropertyChange(() => CanSave);
                NotifyOfPropertyChange(() => CanUpdateById);
            }
        }

        public string Note
        {
            get { return _note; }
            set
            {
                _note = value;
                NotifyOfPropertyChange(() => Note);
                NotifyOfPropertyChange(() => CanClear);
                NotifyOfPropertyChange(() => CanSave);
                NotifyOfPropertyChange(() => CanUpdateById);
            }
        }

        private DateTime _createdDate;

        public DateTime CreatedDate
        {
            get { return _createdDate; }
            set { _createdDate = value; }
        }


        public void LoadFromDB()
        {
            Notes.Clear();

            var rows = sql.ReadNotes();
            foreach (var row in rows)
            {
                Notes.Add(row);
            }
        }

        public bool CanLoadById
        {
            get
            {
                if (String.IsNullOrWhiteSpace(Id))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        public void LoadById()
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

        public bool CanClear
        {
            get
            {
                if (String.IsNullOrWhiteSpace(Id) && String.IsNullOrWhiteSpace(Title) && String.IsNullOrWhiteSpace(Note))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        public void Clear()
        {
            Id = "";
            Title = "";
            Note = "";
        }

        public bool CanSave
        {
            get
            {
                int idCount = 0;
                for (int i = 0; i < Notes.Count; i++)
                {
                    if (Id == Notes[i].Id)
                    {
                        idCount++;
                    }
                }

                if (String.IsNullOrWhiteSpace(Id) || String.IsNullOrWhiteSpace(Title) || String.IsNullOrWhiteSpace(Note) || idCount > 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        public void Save()
        {
            Notes.Add(new NoteModel { Id = Id, Title = Title, Note = Note, CreatedDate = DateTime.Now });

            NoteModel note = new NoteModel
            {
                Id = Id,
                Title = Title,
                Note = Note,
                CreatedDate = DateTime.Now,
            };

            sql.CreateNote(note);
        }

        public bool CanUpdateById
        {
            get
            {
                if (String.IsNullOrWhiteSpace(Id) || String.IsNullOrWhiteSpace(Title) || String.IsNullOrWhiteSpace(Note))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        public void UpdateById()
        {
            NoteModel newNote = new();

            foreach (var note in Notes)
            {
                if (Id == note.Id)
                {
                    newNote.Id = Id;
                    newNote.Title = Title;
                    newNote.Note = Note;
                    newNote.CreatedDate = DateTime.Now;
                }
            }

            sql.UpdateNote(newNote);

            LoadFromDB();
        }

        public bool CanDeleteById
        {
            get
            {
                int idCount = 0;
                for (int i = 0; i < Notes.Count; i++)
                {
                    if (Id == Notes[i].Id)
                    {
                        idCount++;
                    }
                }

                if (String.IsNullOrWhiteSpace(Id) || idCount < 1)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        public void DeleteById()
        {
            foreach (var note in Notes)
            {
                if (Id == note.Id)
                {
                    sql.DeleteNote(note);
                }
            }

            LoadFromDB();
        }

        private static string GetConnectionString(string connectionStringName = "Default")
        {
            string output = "";

            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");

            var config = builder.Build();

            output = config.GetConnectionString(connectionStringName);

            return output;
        }
    }
}
