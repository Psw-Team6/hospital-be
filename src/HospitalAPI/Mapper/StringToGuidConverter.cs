using System;
using AutoMapper;

namespace HospitalAPI.Mapper
{
    public class StringToGuidConverter : ITypeConverter<string, Guid>
    {
        public Guid Convert(string source, Guid destination, ResolutionContext context)
        {
            return Guid.Parse(source);
        }
    }
}