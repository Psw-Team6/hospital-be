﻿using System;

namespace HospitalAPI.Dtos.Response
{
    public class MedicineExaminationResponse
    {
        public Guid Id { get; set; }
        public  string Name { get; set; }
        public int Amount { get; set; }
    }
}