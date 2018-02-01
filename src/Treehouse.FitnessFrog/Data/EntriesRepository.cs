using System;
using System.Collections.Generic;
using System.Linq;
using Treehouse.FitnessFrog.Models;

namespace Treehouse.FitnessFrog.Data
{
    /// <summary>
    /// The repository for entries.
    /// 
    /// Note: The code in this class is not to be considered a "best practice" 
    /// example of how to do data persistence, but rather as workaround for
    /// not having a database to persist data to.
    /// </summary>
    public class EntriesRepository
    {
        /// <summary>
        /// Returns a collection of entries.
        /// </summary>
        /// <returns>A list of entries.</returns>
        public List<Entry> GetEntries()
        {
            return Data.Entries
                .Join(
                    Data.Activities, // The inner collection
                    e => e.ActivityId, // The outer selector
                    a => a.Id,  // The inner selector
                    (e, a) =>  // The result selector
                    {
                        e.Activity = a; // Set the entry's Activity property
                        return e; // Return the entry
                    }
                    )
                .OrderByDescending(e => e.Date)
                .ThenByDescending(e => e.Id)
                .ToList();
        }

        /// <summary>
        /// Returns a single entry for the provided ID.
        /// </summary>
        /// <param name="id">The ID for the entry to return.</param>
        /// <returns>An entry.</returns>
        public Entry GetEntry(int id)
        {
            Entry entry = Data.Entries
                .Where(e => e.Id == id)
                .SingleOrDefault();

            // Ensure that the activity property is not null.
            if (entry.Activity == null)
            {
                entry.Activity = Data.Activities
                    .Where(a => a.Id == entry.ActivityId)
                    .SingleOrDefault();
            }

            return entry;
        }

        /// <summary>
        /// Adds an entry.
        /// </summary>
        /// <param name="entry">The entry to add.</param>
        public void AddEntry(Entry entry)
        {
            // Get the next available entry ID.
            int nextAvailableEntryId = Data.Entries
                .Max(e => e.Id) + 1;

            entry.Id = nextAvailableEntryId;

            Data.Entries.Add(entry);
        }

        /// <summary>
        /// Updates an entry.
        /// </summary>
        /// <param name="entry">The entry to update.</param>
        public void UpdateEntry(Entry entry)
        {
            // Find the index of the entry that we need to update.
            int entryIndex = Data.Entries.FindIndex(e => e.Id == entry.Id);

            if (entryIndex == -1)
            {
                throw new Exception(
                    string.Format("Unable to find an entry with an ID of {0}", entry.Id));
            }

            Data.Entries[entryIndex] = entry;
        }

        /// <summary>
        /// Deletes an entry.
        /// </summary>
        /// <param name="id">The ID of the entry to delete.</param>
        public void DeleteEntry(int id)
        {
            // Find the index of the entry that we need to delete.
            int entryIndex = Data.Entries.FindIndex(e => e.Id == id);

            if (entryIndex == -1)
            {
                throw new Exception(
                    string.Format("Unable to find an entry with an ID of {0}", id));
            }

            Data.Entries.RemoveAt(entryIndex);
        }
    }
}