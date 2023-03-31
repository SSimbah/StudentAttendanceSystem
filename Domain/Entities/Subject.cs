﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Subject
    {
        [Key]
        [ScaffoldColumn(false)]
        public int SubjectID { get; set; }

        [DisplayName("Subject Code")]
        public string? SubjectCode { get; set; }

        [DisplayName("Subject Name")]
        public string? SubjectName { get; set; }

        public string SubjectNameExt     // code - name
        {
            get
            {
                return SubjectCode + " - " + SubjectName;
            }
        }

    }
}