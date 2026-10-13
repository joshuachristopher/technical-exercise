namespace StargateAPI.Data
{
    public static class StargateSeeder
    {
        public static void Seed(StargateContext context)
        {
            if (context.People.Any())
            {
                return;
            }

            var shepard = new Person { Name = "Alan Shepard" };
            var changDiaz = new Person { Name = "Franklin Chang-Díaz" };
            var ride = new Person { Name = "Sally Ride" };
            context.People.AddRange(shepard, changDiaz, ride);

            context.AstronautDetails.Add(new AstronautDetail
            {
                Person = shepard,
                CurrentRank = "Rear Admiral",
                CurrentDutyTitle = "Commander",
                CareerStartDate = new DateTime(1959, 4, 9)
            });

            context.AstronautDuties.Add(new AstronautDuty
            {
                Person = shepard,
                Rank = "Rear Admiral",
                DutyTitle = "Commander",
                DutyStartDate = new DateTime(1959, 4, 9)
            });

            context.SaveChanges();
        }
    }
}
