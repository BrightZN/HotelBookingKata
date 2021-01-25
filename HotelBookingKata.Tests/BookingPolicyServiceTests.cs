﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HotelBookingKata.Tests
{
    public class BookingPolicyServiceTests
    {
        private readonly InMemoryCompanyRepository _companyRepository;
        private readonly InMemoryEmployeeRepository _employeeRepository;

        private readonly BookingPolicyService _sut;

        public BookingPolicyServiceTests()
        {
            _companyRepository = new InMemoryCompanyRepository();
            _employeeRepository = new InMemoryEmployeeRepository();

            _sut = new BookingPolicyService(_companyRepository, _employeeRepository);
        }

        [Fact]
        public async Task SetCompanyPolicyAsync_ExistingCompanyWithoutPolicy_AddsPolicy()
        {
            var companyId = new CompanyId("h4ck");

            var roomTypes = new List<RoomType> { RoomType.Standard };

            var existingCompany = new Company(companyId);

            _companyRepository.SavedCompany = existingCompany;

            await _sut.SetCompanyPolicyAsync(companyId, roomTypes);

            Assert.True(existingCompany.CanBook(RoomType.Standard));
            Assert.False(existingCompany.CanBook(RoomType.Presidential));
        }

        [Fact]
        public async Task SetCompanyPolicyAsync_ExistingCompanyWithPolicy_UpdatesPolicy()
        {
            var companyId = new CompanyId("h4ck");

            var companyPolicy = new BookingPolicy(RoomType.Standard);

            var existingCompany = new Company(companyId, companyPolicy);

            _companyRepository.SavedCompany = existingCompany;

            await _sut.SetCompanyPolicyAsync(companyId, new List<RoomType> { RoomType.Standard, RoomType.Presidential });

            Assert.True(existingCompany.CanBook(RoomType.Standard));
            Assert.True(existingCompany.CanBook(RoomType.Presidential));
        }

        [Fact]
        public async Task SetEmployeePolicyAsync_ExistingEmployeeWithoutPolicy_AddsPolicy()
        {
            var companyId = new CompanyId("h4ck");
            var employeeId = new EmployeeId("RE-0182");

            var existingEmployee = new Employee(employeeId, companyId);

            var roomTypes = new List<RoomType> { RoomType.Standard };

            _employeeRepository.SavedEmployee = existingEmployee;

            await _sut.SetEmployeePolicyAsync(employeeId, roomTypes);

            Assert.False(existingEmployee.NoPolicy());
            Assert.True(existingEmployee.CanBook(RoomType.Standard));
        }

        [Fact]
        public async Task IsBookingAllowedAsync_RoomTypeCoveredByCompanyPolicyAndEmployeeWithoutPolicy_ReturnsTrue()
        {
            var companyId = new CompanyId("h4ck");
            var employeeId = new EmployeeId("RE-0182");

            var policy = new BookingPolicy(RoomType.Standard);

            var company = new Company(companyId, policy);
            var employee = new Employee(employeeId, companyId);

            _companyRepository.SavedCompany = company;
            _employeeRepository.SavedEmployee = employee;

            bool isBookingAllowed = await _sut.IsBookingAllowedAsync(employeeId, RoomType.Standard);

            Assert.True(isBookingAllowed);
        }

        [Fact]
        public async Task IsBookingAllowedAsync_RoomTypeNotCoveredByCompanyAndEmployeePolicy_ReturnsFalse()
        {
            var companyId = new CompanyId("h4ck");
            var employeeId = new EmployeeId("RE-0182");

            var policy = new BookingPolicy(RoomType.Standard);

            var employee = new Employee(employeeId, companyId, policy);
            var company = new Company(companyId, policy);

            _companyRepository.SavedCompany = company;
            _employeeRepository.SavedEmployee = employee;

            bool isBookingAllowed = await _sut.IsBookingAllowedAsync(employeeId, RoomType.Presidential);

            Assert.False(isBookingAllowed);
        }

        [Fact]
        public async Task IsBookingAllowed_RoomCoveredByEmployeePolicyAndNoCompanyPolicy_ReturnsTrue()
        {
            var companyId = new CompanyId("h4ck");
            var employeeId = new EmployeeId("RE-0182");

            var policy = new BookingPolicy(RoomType.Presidential);

            var employee = new Employee(employeeId, companyId, policy);
            var company = new Company(companyId);

            _companyRepository.SavedCompany = company;
            _employeeRepository.SavedEmployee = employee;

            bool isBookingAllowed = await _sut.IsBookingAllowedAsync(employeeId, RoomType.Presidential);

            Assert.True(isBookingAllowed);
        }
    }

    internal class InMemoryCompanyRepository : ICompanyRepository
    {
        public Company SavedCompany { get; internal set; }

        public Task<Company> GetCompanyByIdAsync(CompanyId companyId)
        {
            if (SavedCompany != null && SavedCompany.Id == companyId)
                return Task.FromResult(SavedCompany);

            throw new CompanyNotFoundException();
        }

        public Task SaveCompanyAsync(Company company)
        {
            SavedCompany = company;

            return Task.CompletedTask;
        }
    }
}
