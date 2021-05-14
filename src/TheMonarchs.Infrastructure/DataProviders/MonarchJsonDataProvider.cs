using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using TheMonarchs.Core.Entities;
using TheMonarchs.Core.Interfaces;
using TheMonarchs.Core.Utilities;
using TheMonarchs.Infrastructure.Models;

namespace TheMonarchs.Infrastructure.DataProviders
{
    public class MonarchJsonDataProvider : IMonarchDataProvider
    {
        private readonly string _datasourceFilePath;
        private readonly IQueryable<Monarch> _dataSource;

        public IQueryable<Monarch> MonarchesDataSource => _dataSource;

        public MonarchJsonDataProvider(string datasourceFilePath)
        {
            _datasourceFilePath = datasourceFilePath;
            _dataSource = LoadData();
        }

        private IQueryable<Monarch> LoadData()
        {

            try
            {
                var jsonText = File.ReadAllText(_datasourceFilePath);

                var jsonModel = JsonSerializer.Deserialize<IEnumerable<MonarchJsonModel>>(jsonText);

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
