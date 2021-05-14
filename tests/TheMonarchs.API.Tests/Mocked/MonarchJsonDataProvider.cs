using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using TheMonarchs.Core.Entities;
using TheMonarchs.Core.Interfaces;
using TheMonarchs.Core.Utilities;
using TheMonarchs.Infrastructure.Models;

namespace TheMonarchs.API.Tests.Mocked
{
    public class MonarchJsonDataProvider : IMonarchDataProvider
    {
        const string InputJson = @"[
    {
        ""id"": 1,
        ""nm"": ""Edward the Elder"",
        ""cty"": ""United Kingdom"",
        ""hse"": ""House of Wessex"",
        ""yrs"": ""899-925""
    },
    {
        ""id"": 2,
        ""nm"": ""Athelstan"",
        ""cty"": ""United Kingdom"",
        ""hse"": ""House of Wessex"",
        ""yrs"": ""925-940""
    },
    {
        ""id"": 3,
        ""nm"": ""Edmund"",
        ""cty"": ""United Kingdom"",
        ""hse"": ""House of Wessex"",
        ""yrs"": ""940-946""
    },
    {
        ""id"": 4,
        ""nm"": ""Edred"",
        ""cty"": ""United Kingdom"",
        ""hse"": ""House of Wessex"",
        ""yrs"": ""946-955""
    },
    {
        ""id"": 5,
        ""nm"": ""Edwy"",
        ""cty"": ""United Kingdom"",
        ""hse"": ""House of Wessex"",
        ""yrs"": ""955-959""
    },
    {
        ""id"": 6,
        ""nm"": ""Edgar"",
        ""cty"": ""United Kingdom"",
        ""hse"": ""House of Wessex"",
        ""yrs"": ""959-975""
    },
    {
        ""id"": 17,
        ""nm"": ""Henry I"",
        ""cty"": ""United Kingdom"",
        ""hse"": ""House of Normandy"",
        ""yrs"": ""1100-1135""
    },
    {
        ""id"": 18,
        ""nm"": ""Stephen"",
        ""cty"": ""United Kingdom"",
        ""hse"": ""House of Blois"",
        ""yrs"": ""1135-1154""
    },
    {
        ""id"": 19,
        ""nm"": ""Henry II"",
        ""cty"": ""United Kingdom"",
        ""hse"": ""House of Angevin"",
        ""yrs"": ""1154-1189""
    },
    {
        ""id"": 20,
        ""nm"": ""Richard I"",
        ""cty"": ""United Kingdom"",
        ""hse"": ""House of Angevin"",
        ""yrs"": ""1189-1199""
    },
    {
        ""id"": 21,
        ""nm"": ""John"",
        ""cty"": ""United Kingdom"",
        ""hse"": ""House of Angevin"",
        ""yrs"": ""1199-1216""
    },
    {
        ""id"": 22,
        ""nm"": ""Henry III"",
        ""cty"": ""United Kingdom"",
        ""hse"": ""House of Plantagenet"",
        ""yrs"": ""1216-1272""
    },
    {
        ""id"": 23,
        ""nm"": ""Edward I"",
        ""cty"": ""United Kingdom"",
        ""hse"": ""House of Plantagenet"",
        ""yrs"": ""1272-1307""
    },
    {
        ""id"": 24,
        ""nm"": ""Edward II"",
        ""cty"": ""United Kingdom"",
        ""hse"": ""House of Plantagenet"",
        ""yrs"": ""1307-1327""
    }
]";
        private readonly IQueryable<Monarch> _dataSource;
        public MonarchJsonDataProvider()
        {
            _dataSource = LoadData();
        }

        public IQueryable<Monarch> MonarchesDataSource => _dataSource;



        private IQueryable<Monarch> LoadData()
        {

            try
            {
                var jsonModel = JsonSerializer.Deserialize<IEnumerable<MonarchJsonModel>>(InputJson);

                return jsonModel.Select(m => MapToMonarch(m)).AsQueryable();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private Monarch MapToMonarch(MonarchJsonModel monarchJsonModel)
        {
            var firstLastNames = DataHelper.ExtractFirstLastName(monarchJsonModel.Name);
            var startEndYears = DataHelper.ExtractStartEndYear(monarchJsonModel.Yrs);

            return new Monarch
            {
                Id = monarchJsonModel.Id,
                Name = monarchJsonModel.Name,
                FirstName = firstLastNames?.firstName,
                LastName = firstLastNames?.lastName,
                House = monarchJsonModel.House,
                Country = monarchJsonModel.City,
                YearStart = startEndYears?.startYear,
                YearEnd = startEndYears?.endYear
            };
        }
    }
}
