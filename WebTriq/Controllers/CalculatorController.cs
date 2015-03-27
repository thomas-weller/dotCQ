using System.Web;

namespace WebTriq.Controllers
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    using Newtonsoft.Json;

    using ProductZero.Data;

    using WebTriq.Models;

    public class CalculatorController : ZeroController
    {
        //
        // GET: /Calculator/

        public ActionResult Index()
        {
            const string Meta =
                "This Calculator gives an estimate of the maintenance effort, and consequently the related financial effects, " + 
                "that you can expect over the years when purposefully investing in code quality.";

            ViewBag.Title = "Project Cost Calculator - .CQ";
            ViewBag.MetaDescription = Meta;
            ViewBag.DisqusName = "Calculator";

            var calculator = new Calculator(); // causes settings defaults
            calculator.Calculate();

            return View(calculator);
        }

        public JsonpResult Calc()
        {
            string requestData = HttpUtility.ParseQueryString(Request.Url.Query)[1];

            try
            {
                var settings = JsonConvert.DeserializeObject<CalculatorSettings>(requestData);

                var calculator = new Calculator(settings);

                calculator.Calculate();

                Task.Factory.StartNew(() => this.SaveCalculationData(calculator));

                return this.Jsonp(CalcDataToJson(calculator));
            }
            catch
            {
                return this.Jsonp(ErrorJson());
            }
        }

        private static string ErrorJson()
        {
            return "{\"error\":\"true\"}";
        }

        public static string CalcDataToJson(Calculator calculator)
        {
            var stringBuilder = new StringBuilder("{\"error\":\"false\",");
            int i = 0;
            double[] data;
            long[] costs;
            long accumulatedCosts;

            // size
            // ****
            stringBuilder.Append("\"sloc\":" + calculator.SystemSize + ",");

            // size in fp
            // **********
            stringBuilder.Append("\"fp\":" + calculator.Fp + ",");

            // effort years
            // ************
            stringBuilder.Append("\"effortX\":[1");
            for (i = 2; i <= calculator.Settings.Lifetime; i++)
            {
                stringBuilder.Append("," + i);
            }
            stringBuilder.Append("],");

            // effort unit
            // ***********
            bool unitMonths = calculator.Settings.ProjectSize < 10;
            double effortFactor = unitMonths ? 1 : 12;
            stringBuilder.Append("\"effortUnit\":\"" + (unitMonths ? "Staff-months" : "Staff-years") + "\",");

            // effort current
            // **************
            data = calculator.YearlyEffort[calculator.Settings.InitialQuality].Select(d => Math.Round(d / effortFactor, 1)).ToArray();
            if (data.Length > 0)
            {
                stringBuilder.Append("\"effortCurrent\":[" + data[0].ToString(CultureInfo.InvariantCulture));
                for (i = 1; i < data.Length; i++)
                {
                    stringBuilder.Append("," + data[i].ToString(CultureInfo.InvariantCulture));
                }
                stringBuilder.Append("],");
            }

            // effort modified
            // ***************
            data = calculator.YearlyEffort[calculator.NewQualityLevel].Select(d => Math.Round(d / effortFactor, 1)).ToArray();
            if (data.Length > 0)
            {
                stringBuilder.Append("\"effortModified\":[" + data[0].ToString(CultureInfo.InvariantCulture));
                for (i = 1; i < data.Length; i++)
                {
                    stringBuilder.Append("," + data[i].ToString(CultureInfo.InvariantCulture));
                }
                stringBuilder.Append("],");
            }

            // costs years
            // ***********
            stringBuilder.Append("\"costsX\":[0, 1");
            for (i = 2; i <= calculator.Settings.Lifetime; i++)
            {
                stringBuilder.Append("," + i);
            }
            stringBuilder.Append("],");

            // costs current
            // *************
            accumulatedCosts = 0;
            costs = calculator.YearlyCosts[calculator.Settings.InitialQuality];
            if (costs.Length > 0)
            {
                stringBuilder.Append("\"costsCurrent\":[" + calculator.DevelopmentCosts);
                for (i = 0; i < costs.Length; i++)
                {
                    accumulatedCosts += costs[i];
                    stringBuilder.Append("," + (calculator.DevelopmentCosts + accumulatedCosts));
                }
                stringBuilder.Append("],");
            }

            // costs modified
            // **************
            accumulatedCosts = 0;
            costs = calculator.YearlyCosts[calculator.NewQualityLevel];
            if (costs.Length > 0)
            {
                stringBuilder.Append("\"costsModified\":[" + calculator.ModifiedDevelopmentCosts);
                for (i = 0; i < costs.Length; i++)
                {
                    accumulatedCosts += costs[i];
                    stringBuilder.Append("," + (calculator.ModifiedDevelopmentCosts + accumulatedCosts));
                }
                stringBuilder.Append("],");
            }

            // yearly savings
            // **************
            costs = calculator.YearlyCosts[-1];
            if (costs.Length > 0)
            {
                stringBuilder.Append("\"avgSaving\":" + (int)costs.Average() + ",");
                stringBuilder.Append("\"maxSaving\":" + costs.Max() + ",");
                stringBuilder.Append("\"minSaving\":" + costs.Min() + ",");
            }

            // project costs (org.)
            // ********************
            stringBuilder.Append("\"devOrg\":" + calculator.DevelopmentCosts + ",");
            stringBuilder.Append("\"maintOrg\":" + calculator.MaintanceCosts + ",");
            stringBuilder.Append("\"totalOrg\":" + calculator.TotalProjectCosts + ",");
            stringBuilder.Append("\"maintPercentOrg\":" + calculator.MaintenancePercentage + ",");

            // project costs (mod.)
            // ********************
            stringBuilder.Append("\"devMod\":" + calculator.ModifiedDevelopmentCosts + ",");
            stringBuilder.Append("\"maintMod\":" + calculator.ModifiedMaintenanceCosts + ",");
            stringBuilder.Append("\"totalMod\":" + calculator.ModifiedTotalProjectCosts + ",");
            stringBuilder.Append("\"maintPercentMod\":" + calculator.ModifiedMaintenancePercentage + ",");

            // project costs (diff.)
            // *********************
            stringBuilder.Append("\"maintDiff\":" + calculator.MaintanceCostsDiff + ",");
            stringBuilder.Append("\"totalDiff\":" + calculator.TotalProjectCostsDiff + ",");

            // break even (years)
            // **********
            stringBuilder.Append("\"breakEvenPoint\":" + calculator.BreakEvenPoint.ToString(CultureInfo.InvariantCulture) + ",");

            // ROI
            // ***
            stringBuilder.Append("\"roi\":" + Math.Round(calculator.Roi, 1).ToString(CultureInfo.InvariantCulture) + ",");

            // costs until breakeven
            // *********************
            stringBuilder.Append("\"breakEvenCosts\":" + calculator.GetAccumulatedCosts(calculator.BreakEvenPoint) + ",");

            // lifetime
            // ********
            stringBuilder.Append("\"lifetime\":" + calculator.Settings.Lifetime + ",");

            // quality level
            // *************
            stringBuilder.Append("\"quality\":" + calculator.Settings.InitialQuality + ",");

            // surplus
            // *******
            stringBuilder.Append("\"surplus\":" + calculator.SurplusInvestment + ",");

            // currency
            // ********
            stringBuilder.Append("\"currency\":\"" + calculator.Settings.Currency + "\"");

            return stringBuilder.Append("}").ToString();
        }

        private void SaveCalculationData(Calculator calculator)
        {
            //try
            //{
            //    IpAddresses address = GetIpAddress(Request.UserHostAddress);

            //    Entities.AddToCalculations(new Calculations
            //                                   {
            //                                       Id = Guid.NewGuid(),
            //                                       Timestamp = DateTime.UtcNow,
            //                                       IpAddress = address,
            //                                       StaffCostsPerYear = calculator.Settings.StaffCostsPerYear,
            //                                       Currency = calculator.Settings.Currency,
            //                                       ProjectSize = calculator.Settings.ProjectSize,
            //                                       Lifetime = calculator.Settings.Lifetime,
            //                                       Quality = calculator.Settings.InitialQuality,
            //                                       SurplusPercent = calculator.Settings.AdditionalQualityEffort,
            //                                       SurplusInvestment = calculator.SurplusInvestment,
            //                                       DevCosts = calculator.DevelopmentCosts,
            //                                       ModDevCosts = calculator.ModifiedDevelopmentCosts,
            //                                       MaintCosts = calculator.MaintanceCosts,
            //                                       ModMaintCosts = calculator.ModifiedMaintenanceCosts,
            //                                       TotalCosts = calculator.TotalProjectCosts,
            //                                       ModTotalCosts = calculator.ModifiedTotalProjectCosts,
            //                                       MaintPercent = calculator.MaintenancePercentage,
            //                                       ModMaintPercent = calculator.ModifiedMaintenancePercentage,
            //                                       BreakEven = calculator.BreakEvenPoint,
            //                                       Roi = calculator.Roi,
            //                                       AvgYearlySaving = (int)calculator.YearlyCosts[-1].Average(),
            //                                       MaxYearlySaving = calculator.YearlyCosts[-1].Max(),
            //                                       MinYearlySaving = calculator.YearlyCosts[-1].Min()
            //                                   });

            //    Entities.SaveChanges();
            //}
            //catch
            //{
            //}
        }
    }
}
