using GiraffeMissile.Application.Common.Interfaces;
using System;

namespace GiraffeMissile.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
