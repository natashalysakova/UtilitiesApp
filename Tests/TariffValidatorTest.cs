
using ApiService.Validation;
using FluentValidation.TestHelper;
using Infrastructure.Models;

namespace Tests;
[TestClass]
public class TariffValidatorTest
{
    static Tariff minimalValidFixedModel = new Tariff()
    {
        HomeId = Guid.NewGuid(),
        UtilityGroupId = Guid.NewGuid(),
        StartDate = DateTime.Now,
        Cost = 42,
        TariffType = TariffType.Fixed
    };

    static Tariff minimalValidHouseholdAreaModel = new Tariff()
    {
        HomeId = Guid.NewGuid(),
        UtilityGroupId = Guid.NewGuid(),
        StartDate = DateTime.Now,
        Cost = 42,
        TariffType = TariffType.HouseHoldArea
    };

    static Tariff minimalValidMetersModel = new Tariff()
    {
        HomeId = Guid.NewGuid(),
        UtilityGroupId = Guid.NewGuid(),
        StartDate = DateTime.Now,
        Cost = 42,
        TariffType = TariffType.Meters,
        Units = "qubic meter"
    };

    static Tariff fullValidFixedModel = new Tariff()
    {
        HomeId = Guid.NewGuid(),
        UtilityGroupId = Guid.NewGuid(),
        StartDate = DateTime.Now,
        Cost = 42,
        TariffType = TariffType.Fixed,
        EndDate = DateTime.Now.AddDays(3),
        UseFixedPay = true,
        FixedPay = 42,
        FixedPayName = "FixedPay",
    };

    static Tariff fullValidHouseholdAreaModel = new Tariff()
    {
        HomeId = Guid.NewGuid(),
        UtilityGroupId = Guid.NewGuid(),
        StartDate = DateTime.Now,
        Cost = 42,
        TariffType = TariffType.HouseHoldArea,
        UseFixedPay = true,
        FixedPay = 42,
        FixedPayName = "FixedPay",

    };

    static Tariff fullValidMetersModel = new Tariff()
    {
        HomeId = Guid.NewGuid(),
        UtilityGroupId = Guid.NewGuid(),
        StartDate = DateTime.Now,
        Cost = 42,
        TariffType = TariffType.Meters,
        Units = "qubic meter",
        UseLimits = true,
        Limits = new[]
        {
            new TariffLimit{ Limit = 100, CostAfterLimit= 69 }
        },
        UseAdditionalFee = true,
        AdditionalFeeCost = 42,
        AdditionalFeeName = "Additional Fee",
        UseFixedPay = true,
        FixedPay = 42,
        FixedPayName = "FixedPay",
        EndDate = DateTime.Today.AddDays(3),
    };


    [TestMethod]
    public void Should_Not_Have_Any_Errors_Meters()
    {
        var result = new TariffValidator().TestValidate(minimalValidMetersModel);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [TestMethod]
    public void Should_Not_Have_Any_Errors_HouseholdArea()
    {
        var result = new TariffValidator().TestValidate(minimalValidHouseholdAreaModel);
        result.ShouldNotHaveAnyValidationErrors();
    }
    [TestMethod]
    public void Should_Not_Have_Any_Errors_Fixed()
    {
        var result = new TariffValidator().TestValidate(minimalValidFixedModel);
        result.ShouldNotHaveAnyValidationErrors();
    }
    [TestMethod]
    public void Should_Not_Have_Any_Errors_Meters_Full()
    {
        var result = new TariffValidator().TestValidate(fullValidMetersModel);
        result.ShouldNotHaveAnyValidationErrors();
    }
    [TestMethod]
    public void Should_Not_Have_Any_Errors_HouseholdArea_Full()
    {
        var result = new TariffValidator().TestValidate(fullValidHouseholdAreaModel);
        result.ShouldNotHaveAnyValidationErrors();
    }
    [TestMethod]
    public void Should_Not_Have_Any_Errors_Fixed_Full()
    {
        var result = new TariffValidator().TestValidate(fullValidFixedModel);
        result.ShouldNotHaveAnyValidationErrors();
    }


    private static IEnumerable<object[]> InvalidDates()
    {
        return new List<object[]>
        {
            new object[] { DateTime.Today, false, DateTime.Today.AddDays(10), false},
            new object[] { DateTime.Today.AddDays(10), false, DateTime.Today.AddDays(11), false },
            new object[] { DateTime.Today.AddDays(10), false, null, false },
            new object[] { DateTime.Today, false, DateTime.Today.AddDays(-10), true},
            new object[] { DateTime.Today.AddDays(-10), false, DateTime.Today.AddDays(-11),true },
            new object[] { DateTime.MinValue, true, DateTime.MinValue, true },
            new object[] { null!, true, null!, false},
            new object[] { null!, true, DateTime.Today.AddDays(10), false},
        };
    }

    [DataTestMethod]
    [DynamicData(nameof(InvalidDates), DynamicDataSourceType.Method)]
    public void Should_Have_Errors_In_Dates(DateTime start, bool startError, DateTime? end, bool endError)
    {
        var model = minimalValidFixedModel;
        model.StartDate = start;
        model.EndDate = end;

        var result = new TariffValidator().TestValidate(minimalValidFixedModel);

        if (startError)
            result.ShouldHaveValidationErrorFor(x => x.StartDate);
        else
            result.ShouldNotHaveValidationErrorFor(x => x.StartDate);

        if (endError)
            result.ShouldHaveValidationErrorFor(x => x.EndDate);
        else
            result.ShouldNotHaveValidationErrorFor(x => x.EndDate);
    }

    [TestMethod]
    [DataRow(0)]
    [DataRow(-1)]
    public void Should_Have_Error_When_Cost_Is_Less_Or_Equal_Zero(int value)
    {
        var model = minimalValidFixedModel;
        model.Cost = value;
        var result = new TariffValidator().TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Cost);
    }

    [TestMethod]
    public void Should_Have_Error_When_Enum_Not_In_Range()
    {
        var model = minimalValidFixedModel;
        model.TariffType = (TariffType)99;
        var result = new TariffValidator().TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.TariffType);
    }

    [TestMethod]
    [DataRow("")]
    [DataRow(null)]
    public void Should_Have_Error_When_Units_Is_Null_Or_Empty(string data)
    {
        var model = minimalValidMetersModel;
        model.Units = data;
        var result = new TariffValidator().TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Units);
    }

    [TestMethod]
    [DataRow("")]
    [DataRow(null)]
    public void Should_Have_No_Error_When_Units_Is_Null_Or_Empty_And_Type_Fixed(string data)
    {
        var model = minimalValidFixedModel;
        model.Units = data;
        var result = new TariffValidator().TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(x => x.Units);
    }

    [TestMethod]
    public void Should_Have_Error_When_UseLimits_And_No_Limits()
    {
        var model = minimalValidMetersModel;
        model.UseLimits = true;
        var result = new TariffValidator().TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Limits);
    }

    [TestMethod]
    public void Should_Have_Error_When_UseAdditionalFee()
    {
        var model = minimalValidMetersModel;
        model.UseAdditionalFee = true;
        var result = new TariffValidator().TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.AdditionalFeeName);
        result.ShouldHaveValidationErrorFor(x => x.AdditionalFeeCost);
    }

    [TestMethod]
    public void Should_Have_Error_When_UseFixedPay()
    {
        var model = minimalValidMetersModel;
        model.UseFixedPay = true;
        var result = new TariffValidator().TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.FixedPay);
        result.ShouldHaveValidationErrorFor(x => x.FixedPayName);
    }

    [TestMethod]
    public void Should_Have_Error_When_IsArchived_Is_True()
    {
        var model = minimalValidMetersModel;
        model.IsArchived = true;
        var result = new TariffValidator().TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.IsArchived);
    }

}
