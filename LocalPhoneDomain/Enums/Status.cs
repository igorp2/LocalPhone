using System;

namespace LocalPhoneDomain
{
    public class Status : Attribute
    {
        public Status(string description, char abbreviation, int value)
        {
            Description = description;
            Abbreviation = abbreviation;
            Value = value;
        }

        public string Description { get; }
        public char Abbreviation { get; }
        public int Value { get; }
    }

    public enum ModelStatuses
    {
        [Status("inactive", 'I', 0)] INACTIVE = 0,
        [Status("active", 'A', 1)] ACTIVE = 1,
    }

    public enum CustomerStatuses
    {
        [Status("inactive", 'I', 0)] INACTIVE = 0,
        [Status("active", 'A', 1)] ACTIVE = 1,
        [Status("pending", 'P', 2)] PENDING = 2,
        [Status("verified", 'V', 3)] VERIFIED = 3
    }
}
