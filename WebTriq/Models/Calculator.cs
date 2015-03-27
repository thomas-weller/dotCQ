// ************************************************************************************************************************************************************
// <copyright file="Calculator.cs" company="dotCQ Software Development Ltd. & Co. KG">
//     Copyright (c) 2013, dotCQ Software Development Ltd. & Co. KG. All rights reserved.
// </copyright>
// <authors>
//   <author>Thomas Weller</author>
// </authors>
// <license name="Apache License, Version 2.0">
// 
// Copyright (c) 2013, dotCQ Software Development Ltd. & Co. KG. All rights reserved.
// 
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance with the License. A copy of the License
// is included with the software project which contains this file; or you may obtain a copy at http://www.apache.org/licenses/LICENSE-2.0.
// 
// Unless required by applicable law or agreed to in writing, software distributed under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES 
// OR CONDITIONS OF ANY KIND, either express or implied. See the License for the specific language governing permissions and limitations under the License.
// 
// </license>
// <Description/>
// ************************************************************************************************************************************************************

namespace WebTriq.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Calculator
    {
        public const double TechnologyFactor = 0.0017;
        public const double MaintenanceFraction = 0.15;
        public const double GrowthRate = 0.05;

        public const int LocPerMonth = 590;
        public const double FpPerMonth = 10.67;

        private const double OptimumLevel = 5.001;

        public double NewQualityLevel { get; private set; }

        public CalculatorSettings Settings { get; private set; }

        public int SystemSize { get; private set; } // in SLOC
        public int Fp { get; private set; }

        public Dictionary<double, double[]> YearlyEffort { get; private set; } // in staff-months
        public Dictionary<double, long[]> YearlyCosts { get; private set; }

        public Dictionary<double, long> TotalCosts { get; private set; }

        public long DevelopmentCosts { get; private set; }
        public long ModifiedDevelopmentCosts { get; private set; }
        public long SurplusInvestment { get; private set; }
               
        public long MaintanceCosts { get; private set; }
        public long ModifiedMaintenanceCosts { get; private set; }
        public long MaintanceCostsDiff { get; private set; }
        public long TotalProjectCosts { get; private set; }
        public long ModifiedTotalProjectCosts { get; private set; }
        public long TotalProjectCostsDiff { get; private set; }

        public int MaintenancePercentage { get; private set; }
        public int ModifiedMaintenancePercentage { get; private set; }

        public double BreakEvenPoint { get; private set; }
        public double Roi { get; private set; }


        public Calculator()
        {
            this.Settings = new CalculatorSettings();
        }

        public Calculator(CalculatorSettings settings)
        {
            this.Settings = settings;
        }

        public long GetAccumulatedCosts(double years)
        {
            if (years > this.Settings.Lifetime)
            {
                return -1;
            }

            var floor = (int)Math.Floor(years);

            long costs = this.YearlyCosts[Settings.InitialQuality].Take(floor).Sum();
            costs += (long)Math.Round((years - floor) * this.YearlyCosts[Settings.InitialQuality][floor], 0);

            return this.DevelopmentCosts + costs;
        }

        public void Calculate()
        {
            this.CalculateSs();
            this.CalculateNewQualityLevel();
            this.CalculateDevelopmentCosts();
            this.CalculateYearlyData();
            this.CalculateBreakEven();
            this.CalculateOtherProjectCosts();
        }

        private void CalculateOtherProjectCosts()
        {
            this.MaintanceCosts = this.YearlyCosts[Settings.InitialQuality].Sum();
            this.ModifiedMaintenanceCosts = this.YearlyCosts[this.NewQualityLevel].Sum();
            this.TotalProjectCosts = this.DevelopmentCosts + this.MaintanceCosts;
            this.ModifiedTotalProjectCosts = this.ModifiedDevelopmentCosts + this.ModifiedMaintenanceCosts;
            this.MaintenancePercentage = (int)Math.Round(((double)this.MaintanceCosts / this.TotalProjectCosts) * 100);
            this.ModifiedMaintenancePercentage = (int)Math.Round(((double)this.ModifiedMaintenanceCosts / this.ModifiedTotalProjectCosts) * 100);
            this.TotalProjectCostsDiff = this.ModifiedTotalProjectCosts - this.TotalProjectCosts;
            this.MaintanceCostsDiff = this.ModifiedMaintenanceCosts - this.MaintanceCosts;
            this.Roi = (this.MaintanceCostsDiff * -1 - this.SurplusInvestment) / (double)this.SurplusInvestment;
        }

        private void CalculateDevelopmentCosts()
        {
            this.DevelopmentCosts = Convert.ToInt64(Settings.ProjectSize * Settings.StaffCostsPerYear);
            this.ModifiedDevelopmentCosts = Convert.ToInt64(this.DevelopmentCosts * (1 + ((double)Settings.AdditionalQualityEffort / 100)));
            this.SurplusInvestment = this.ModifiedDevelopmentCosts - this.DevelopmentCosts;
        }

        public void CalculateBreakEven()
        {
            long currentSavings = this.DevelopmentCosts - this.ModifiedDevelopmentCosts;

            double years = 0;
            long nextYearSaving = 0;

            long[] yearlySavings = this.YearlyCosts[-1];

            if (yearlySavings.Length == 0)
            {
                this.BreakEvenPoint = double.NaN;
                return;
            }

            for (int i = 0; i < yearlySavings.Length; i++)
            {
                nextYearSaving = this.YearlyCosts[-1][i];

                if ((currentSavings + nextYearSaving) > 0)
                {
                    break;
                }

                years += 1;
                currentSavings += nextYearSaving;
            }

            long rest = Math.Abs(currentSavings);

            this.BreakEvenPoint = years + ((double)rest / nextYearSaving);
        }

        private void CalculateSs()
        {
            this.SystemSize = Convert.ToInt32(Settings.ProjectSize * 12 * LocPerMonth);
            this.Fp = Convert.ToInt32(Settings.ProjectSize * 12 * FpPerMonth);
        }

        private void CalculateYearlyData()
        {
            this.YearlyEffort = new Dictionary<double, double[]>
                                  {
                                      { OptimumLevel, new double[Settings.Lifetime] },
                                      { Settings.InitialQuality, new double[Settings.Lifetime] },
                                      { this.NewQualityLevel, new double[Settings.Lifetime] }
                                  };

            this.YearlyCosts = new Dictionary<double, long[]>
                                  {
                                      { OptimumLevel, new long[Settings.Lifetime] },
                                      { Settings.InitialQuality, new long[Settings.Lifetime] },
                                      { this.NewQualityLevel, new long[Settings.Lifetime] },
                                      { -1, new long[Settings.Lifetime] }
                                  };

            this.TotalCosts = new Dictionary<double, long>(this.YearlyCosts.Count);

            this.CalculateYearlyEffort(OptimumLevel);
            this.CalculateYearlyEffort(Settings.InitialQuality);
            this.CalculateYearlyEffort(this.NewQualityLevel);

            this.CalculateYearlyCosts(OptimumLevel);
            this.CalculateYearlyCosts(Settings.InitialQuality);
            this.CalculateYearlyCosts(this.NewQualityLevel);

            this.CalculateYearlyCostSavings(Settings.InitialQuality, this.NewQualityLevel);

            this.CalculateTotalCosts();
        }

        private void CalculateTotalCosts()
        {
            foreach (double qualityLevel in YearlyCosts.Keys)
            {
                this.TotalCosts.Add(qualityLevel, this.YearlyCosts[qualityLevel].Sum());
            }
        }

        public void CalculateYearlyCosts(double qualityLevel)
        {
            long[] costs = this.YearlyCosts[qualityLevel];

            double[] efforts = YearlyEffort[qualityLevel];

            for (int i = 0; i < costs.Length; i++)
            {
                costs[i] = Convert.ToInt64(efforts[i] * Settings.StaffCostsPerYear / 12);
            }
        }

        private void CalculateYearlyCostSavings(double initialQuality, double newQualityLevel)
        {
            long[] originalCosts = this.YearlyCosts[initialQuality];
            long[] newCosts = this.YearlyCosts[newQualityLevel];
            long[] diffs = this.YearlyCosts[-1];

            for (int i = 0; i < Settings.Lifetime; i++)
            {
                diffs[i] = originalCosts[i] - newCosts[i];
            }
        }

        private void CalculateYearlyEffort(double qualityLevel)
        {
            double[] row = this.YearlyEffort[qualityLevel];

            for (int i = 0; i < Settings.Lifetime; i++)
            {
                double rebuildValue = Math.Pow(1 + GrowthRate, i + 1) * SystemSize * TechnologyFactor;
                row[i] = (MaintenanceFraction * rebuildValue) / this.GetQualityFactor(qualityLevel);
            }
        }

        private void CalculateNewQualityLevel()
        {
            double currentLevel = Settings.InitialQuality;
            double additionalEffort = Settings.AdditionalQualityEffort;

            double distanceToNextLevel = GetDistanceToNextLevel((int)currentLevel);

            while (distanceToNextLevel > 0 && additionalEffort > distanceToNextLevel)
            {
                currentLevel++;
                additionalEffort -= distanceToNextLevel;

                distanceToNextLevel = GetDistanceToNextLevel((int)currentLevel);
            }

            if (distanceToNextLevel > 0)
            {
                currentLevel += Math.Round(additionalEffort / distanceToNextLevel, 1);
            }

            this.NewQualityLevel = currentLevel;
        }

        private static double GetDistanceToNextLevel(int level)
        {
            switch (level)
            {
                case 1:
                    return 55;
                case 2:
                    return 45;
                case 3:
                    return 40;
                case 4:
                    return 35;
                default:
                    return 0;
            }
        }

        public double GetQualityFactor(double level)
        {
            return Math.Pow(2, (level - 3) / 2);
        }

    } // class Calculator

} // namespace WebTriq.Models