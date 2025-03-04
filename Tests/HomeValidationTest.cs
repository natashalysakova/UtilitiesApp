
using ApiService.Validation;
using FluentValidation.TestHelper;
using Infrastructure.Models;

namespace Tests;
[TestClass]
public class HomeValidationTest
{
    Home minimalValidModel = new Home()
    {
        Name = "House",
        Area = 42,
        Building = "42",
        City = "Kyiv",
        Country = "Ukraine",
        Region = "Kyivska oblast",
        Street = "Ernsta"
    };

    [TestMethod]
    public void Should_Not_Have_Any_Errors_Full()
    {
        var model = new Home()
        {
            Name = "House",
            Area = 42,
            Building = "42",
            City = "Kyiv",
            Country = "Ukraine",
            Region = "Kyivska oblast",
            Street = "Ernsta",
            Apartment = "42",
            IsDefault = true,
            IsArchived = false
        };
        var result = new HomeValidator().TestValidate(model);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [TestMethod]
    public void Should_Not_Have_Any_Errors_Minimal()
    {
        var model = minimalValidModel;
        var result = new HomeValidator().TestValidate(model);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [TestMethod]
    [DataRow("")]
    [DataRow(null)]
    public void Should_Have_Error_When_Name_Is_Null_Or_Empty(string data)
    {
        var model = minimalValidModel;
        model.Name = data;
        var result = new HomeValidator().TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }

    [TestMethod]
    [DataRow("")]
    [DataRow(null)]
    public void Should_Have_Error_When_Country_Is_Null_Or_Empty(string data)
    {
        var model = minimalValidModel;
        model.Country = data;
        var result = new HomeValidator().TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Country);
    }

    [TestMethod]
    [DataRow("")]
    [DataRow(null)]
    public void Should_Have_Error_When_City_Is_Null_Or_Empty(string data)
    {
        var model = minimalValidModel;
        model.City = data;
        var result = new HomeValidator().TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.City);
    }

    [TestMethod]
    [DataRow("")]
    [DataRow(null)]
    public void Should_Have_Error_When_Region_Is_Null_Or_Empty(string data)
    {
        var model = minimalValidModel;
        model.Region = data;
        var result = new HomeValidator().TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Region);
    }

    [TestMethod]
    [DataRow("")]
    [DataRow(null)]
    public void Should_Have_Error_When_Street_Is_Null_Or_Empty(string data)
    {
        var model = minimalValidModel;
        model.Street = data;
        var result = new HomeValidator().TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Street);
    }

    [TestMethod]
    [DataRow("")]
    [DataRow(null)]
    public void Should_Have_Error_When_Building_Is_Null_Or_Empty(string data)
    {
        var model = minimalValidModel;
        model.Building = data;
        var result = new HomeValidator().TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Building);
    }


    [TestMethod]
    [DataRow("")]
    [DataRow("   ")]

    public void Should_Have_Error_When_Apartment_Is_Empty(string data)
    {
        var model = minimalValidModel;
        model.Apartment = data;
        var result = new HomeValidator().TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Apartment);
    }

    [TestMethod]
    public void Should_Have_Error_When_IsArchived_Is_True()
    {
        var model = minimalValidModel;
        model.IsArchived = true;
        var result = new HomeValidator().TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.IsArchived);
    }

    [TestMethod]
    [DataRow(0)]
    [DataRow(-1)]
    public void Should_Have_Error_When_Area_Is_Less_Or_Equal_Zero(double value)
    {
        var model = minimalValidModel;
        model.Area = value;
        var result = new HomeValidator().TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Area);
    }
}