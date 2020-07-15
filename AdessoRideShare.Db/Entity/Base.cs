using AdessoRideShare.Db.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AdessoRideShare.Db.Entity
{
    public interface IBase
    {
        ERecordState RecordState { get; set; }
        public decimal RecordId { get; set; }
        public string Code { get; set; }
        DateTime? CreatedDateTime { get; set; }
        public int Deleted { get; set; }
        void SetRecordState(ERecordState recordState);
        void SetRecordId();
    }
    public class Base : IBase
    {
        private static int autoId = 0;
        [NotMapped]
        public ERecordState RecordState { get; set; }
        [Key]
        public decimal RecordId { get; set; }
        public string Code { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public int Deleted { get; set; } = 0;

        public void SetRecordState(ERecordState recordState)
        {
            this.RecordState = recordState;
        }
        public Base()
        {
            if (this.RecordState == ERecordState.Added)
            {
                autoId = ++autoId;
                RecordId = autoId;
            }
        }

        public void SetRecordId()
        {
            if (this.RecordState == ERecordState.Added)
            {
                autoId = ++autoId;
                RecordId = autoId;
            }
        }
    }
}
