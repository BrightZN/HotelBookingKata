using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HotelBookingKata.Tests
{
    public class CompanyServicesTests
    {
        private readonly InMemoryEmployeeRepository _employeeRepository;

        private readonly CompanyService _sut;

        public CompanyServicesTests()
        {

            _employeeRepository = new InMemoryEmployeeRepository();

            _sut = new CompanyService(_employeeRepository);
        }

        [Fact]
        public async Task AddEmployeeAsync_NewEmployeeId_AddsEmployee()
        {
            var companyId = new CompanyId(value: "h4ck");
            var employeeId = new EmployeeId(value: "RE-0001");

            await _sut.AddEmployeeAsync(companyId, employeeId);

            var savedEmployee = _employeeRepository.SavedEmployee;

            Assert.NotNull(savedEmployee);
        }

        [Fact]
        public async Task AddEmployeeAsync_ExistingEmployeeId_ThrowsException()
        {
            var companyId = new CompanyId(value: "h4ck");
            var employeeId = new EmployeeId(value: "RE-0001");

            var existingEmployee = new Employee(employeeId, companyId);

            _employeeRepository.SavedEmployee = existingEmployee;

            await Assert.ThrowsAsync<EmployeeAlreadyExistsException>(() => _sut.AddEmployeeAsync(companyId, employeeId));
        }

        [Fact]
        public async Task DeleteEmployeeAsync_ExistingEmployee_DeletesEmployeeWithBookingsAndPolicies()
        {
            var companyId = new CompanyId(value: "h4ck");
            var employeeId = new EmployeeId(value: "RE-0001");

            var existingEmployee = new Employee(employeeId, companyId);

            _employeeRepository.SavedEmployee = existingEmployee;

            await _sut.DeleteEmployeeAsync(employeeId);

            Assert.Null(_employeeRepository.SavedEmployee);
        }
    }

    internal class InMemoryEmployeeRepository : IEmployeeRepository
    {
        public Employee SavedEmployee { get; internal set; }

        public Task AddEmployeeAsync(Employee employee)
        {
            SavedEmployee = employee;

            return Task.CompletedTask;
        }

        public Task DeleteEmployeeByIdAsync(EmployeeId employeeId)
        {
            if (SavedEmployee != null && SavedEmployee.Id == employeeId)
            {
                SavedEmployee = null;

                return Task.CompletedTask;
            }


            throw new EmployeeNotFoundException();
        }

        public Task<bool> HasEmployeeWithIdAsync(EmployeeId employeeId)
        {
            return Task.FromResult(SavedEmployee != null && SavedEmployee.Id == employeeId);
        }
    }
}
