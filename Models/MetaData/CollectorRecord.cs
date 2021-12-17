using System;
using System.ComponentModel.DataAnnotations;

namespace CommercialAgriEnterprise.Models
{
    [MetadataType(typeof(CollectorRecordMetaData))]
    public partial class CollectorRecord
    {
        internal sealed class CollectorRecordMetaData
        {
            [Required]
            [RegularExpression(@"^[A-Z]{3}\/[A-Z]{3}\/2\d{3}\-(?!00)[0-9]{2}\/[1-9][0-9]{0,7}$")]
            public string ReferenceNo { get; set; }
            [Required]
            [DataType(DataType.Date)]
            public DateTime DateOfMeeting { get; set; }
            [Required]
            [DataType(DataType.Time)]
            public TimeSpan TimeOfMeeting { get; set; }
            [DataType(DataType.Date)]
            public DateTime UpdatedDOM { get; set; }
            [DataType(DataType.Time)]
            public TimeSpan UpdatedTOM { get; set; }
        }
    }
}