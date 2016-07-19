using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using PPI.Core.Domain.Abstract;
using PPI.Core.Domain.Entities;

namespace PPI.Core.Web.Models
{
    public class PersonViewModel
    {
        public PagingInfo PagingInfo { get; set; }
        
        public IEnumerable<Person>People { get; set; }

        public string SearchString { get; set; }
        public int? eventFilter { get; set; }

        public List<PersonUploadStats> Stats { get; set; }
    }    

    public class PersonUploadStats  //per csv file
    {
        public PersonUploadStats()
        {
            this.RowsAdded = 0;
            this.RowsUpdated = 0;
            this.RowsFailed = 0;
        }

        public string FileName { get; set; }
        public string RowsFailedText { get; set; }
        public int RowsFailed { get; set; }
        public int RowsAdded { get; set; }
        public int RowsUpdated { get; set; }
        public List<PersonUploadRecord> AddedPersons { get; set; }
        public List<PersonUploadRecord> UpdatedPersons { get; set; }
        public List<PersonUploadRecord> FailedPersons { get; set; }

        public string RowsFailedOutput
        {
            get
            {
                if (!string.IsNullOrEmpty(RowsFailedText))
                {
                    return (RowsFailedText);
                }
                else
                {
                    return (RowsFailed.ToString());
                }
            }
        }
    }

    public class PersonUploadRecord
    {
        public bool ImportLevel;
        public int UploadRowNumber { get; set; }
        public string RawRow { get; set; }          //if exception during GetRecords, I don't have entities yet
        public Person ThePerson { get; set; }       //if no exception during GetRecords, I have an entity
        public string ErrorMessage { get; set; }

        public string PersonOutput
        {
            get
            {
                if (ThePerson == null)
                {
                    return (RawRow);
                    //return (string.Format("Row #{0}: {1}", UploadRowNumber, RawRow));
                }
                else
                {
                    return (string.Format("Row #{0}: {1} {2} ({3})", ThePerson.UploadRowNumber, ThePerson.FirstName, ThePerson.LastName, ThePerson.PrimaryEmail));
                }
            }
        }
    }
}