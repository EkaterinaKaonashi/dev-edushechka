﻿using System.ComponentModel.DataAnnotations;
using static DevEdu.API.Common.ValidationMessage;

namespace DevEdu.API.Models.InputModels
{
    public class MaterialInputModel
    {
        [Required(ErrorMessage = ContentRequired)]
        public string Content { get; set; }
    }
}