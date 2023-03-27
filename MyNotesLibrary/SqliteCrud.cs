using MyNotesLibrary.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNotesLibrary
{
    public class SqliteCrud
    {
        SqliteDataAccess db = new();
        private readonly string _connectionString;

        public SqliteCrud(string connectionString)
        {
            this._connectionString = connectionString;
        }

        public void CreateNote(NoteModel note)
        {
            string sql = "insert into Notes (Id, Title, Note, CreatedDate) values (@Id, @Title, @Note, @CreatedDate);";
            db.SaveData(sql, new { note.Id, note.Title, note.Note, note.CreatedDate }, _connectionString);
        }

        public List<NoteModel> ReadNotes()
        {
            string sql = "select Id, Title, Note, CreatedDate from Notes;";
            return db.LoadData<NoteModel, dynamic>(sql, new { }, _connectionString);
        }

        public void UpdateNote(NoteModel note)
        {
            string sql = "update Notes set Title = @Title, Note = @Note, CreatedDate=@CreatedDate where Id = @Id;";
            db.SaveData(sql, note, _connectionString);
        }

        public void DeleteNote(NoteModel note)
        {
            string sql = "delete from Notes where Id = @Id;";
            db.SaveData(sql, new { note.Id }, _connectionString);
        }
    }
}
