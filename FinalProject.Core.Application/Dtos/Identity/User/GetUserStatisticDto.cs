﻿
namespace FinalProject.Core.Application.Dtos.Identity.User
{
    public class GetUserStatisticDto
    {
        public int AmountOfProperties { get; set; }
        public int AmountOfActiveAgentUsers { get; set; }
        public int AmountOfInActiveAgentUsers { get; set; }
        public int AmountOfActiveClienUsers { get; set; }
        public int AmountOfInActiveClientUsers { get; set; }
        public int AmountOfActiveDeveloperUsers { get; set; }
        public int AmountOfInActiveDeveloperUsers { get; set; }
    }
}
