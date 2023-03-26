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
            string sql = "insert into Notes (Id, Title, Note) values (@Id, @Title, @Note);";
            db.SaveData(sql, new { note.Id, note.Title, note.Note }, _connectionString);
        }

        public List<NoteModel> ReadNotes()
        {
            string sql = "select Id, Title, Note from Notes;";
            return db.LoadData<NoteModel, dynamic>(sql, new { }, _connectionString);
        }
    }
}
