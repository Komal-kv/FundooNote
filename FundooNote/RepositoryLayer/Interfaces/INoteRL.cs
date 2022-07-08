﻿using DatabaseLayer.Note;
using RepositoryLayer.Services.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
    public interface INoteRL
    {
        Task AddNote(int UserId, NotePostModel notePostModel);
        Task<List<Note>> GetAllNote(int UserId);
        Task DeleteNote(int UserId, int noteId);
        Task<Note> GetNote(int UserId, int noteId);
        Task UpdateNote(int UserId, int noteId, NoteUpdateModel noteUpdateModel);
        Task ArchiveNote(int UserId, int noteId);
        Task PinNote(int UserId, int noteId);
        Task ReminderNote(int UserId, int noteId, DateTime dateTime);
        Task TrashNote(int UserId, int noteId);
        Task ChangeColorNote(int UserId, int noteId, string color);

    }
}
