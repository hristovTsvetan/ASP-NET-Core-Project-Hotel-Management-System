﻿using HotelManagementSystem.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Models.RoomsType
{
    public class AddRoomTypeFormModel
    {
        [Required]
        [MaxLength(50)]
        [MinLength(3)]
        [RoomTypeNameForAdd]
        public string Name { get; set; }

        [Column(TypeName = "decimal(8, 2)")]
        [Range(10, 500)]
        public decimal Price { get; set; }

        [Range(1, 10)]
        [Display(Name ="Number of beds")]
        public int NumberOfBeds { get; set; }

        [Url]
        [RegularExpression(@"(http[s]*:\/\/)([a-z\-_0-9\/.]+)\.([a-z.]{2,3})\/([a-z0-9\-_\/._~:?#\[\]@!$&'()*+,;=%]*)([a-z0-9]+\.)(jpg|jpeg|png)")]
        public string Image { get; set; }

    }
}