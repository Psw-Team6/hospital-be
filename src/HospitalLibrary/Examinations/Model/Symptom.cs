using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace HospitalLibrary.Examinations.Model
{
    public class Symptom
    {
        private IEnumerable<Examination> _examinations;
        public  Guid Id { get; set; }
        public string Description { get; set; }

        public IEnumerable<Examination> Examinations
        {
            get => _examinations as ReadOnlyCollection<Examination>;
            private set => _examinations = value;
        }
    }
}