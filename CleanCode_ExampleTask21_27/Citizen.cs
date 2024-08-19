using System;

namespace CleanCode_ExampleTask21_27
{
    class Citizen
    {
        public Citizen(Passport passport, bool isAvailableVote)
        {
            Passport = passport ?? throw new ArgumentNullException(nameof(passport));

            IsAvailableVote = isAvailableVote;
        }

        public Passport Passport { get; private set; }
        public bool IsAvailableVote { get; private set; }
    }
}
