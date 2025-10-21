using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using prog6212_part2_ST10456157.Models;
using prog6212_part2_ST10456157.Models.prog6212_part2_ST10456157.Data;
using Microsoft.EntityFrameworkCore;
using Xunit;



namespace prog6212_part2_ST10456157_unittest
{
    public class ClaimTests
    {
        [Fact]
        public void CalculateTotal_ShouldMultiplyHoursAndRate()
        {
            // Arrange
            var claim = new Claim
            {
                HoursWorked = 10,
                HourlyRate = 250
            };

            // Act
            var total = claim.HoursWorked * claim.HourlyRate;

            // Assert
            Assert.Equal(2500, total);
        }

        [Fact]
        public void DefaultStatus_ShouldBePending()
        {
            // Arrange
            var claim = new Claim();

            // Assert
            Assert.Equal("Pending", claim.Status);
        }

        [Fact]
        public async Task SaveClaim_ToInMemoryDatabase_ShouldWork()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "ClaimTestDb")
                .Options;

            using var context = new ApplicationDbContext(options);
            var claim = new Claim
            {
                LecturerName = "John Doe",
                HoursWorked = 5,
                HourlyRate = 200
            };

            // Act
            context.Claims.Add(claim);
            await context.SaveChangesAsync();

            // Assert
            var savedClaim = await context.Claims.FirstOrDefaultAsync(c => c.LecturerName == "John Doe");
            Assert.NotNull(savedClaim);
            Assert.Equal("John Doe", savedClaim.LecturerName);
            Assert.Equal("Pending", savedClaim.Status);
        }
    }

}
