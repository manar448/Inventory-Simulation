using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace InventoryModels
{
    public class SimulationSystem
    {
        public SimulationSystem()
        {
            DemandDistribution = new List<Distribution>();
            LeadDaysDistribution = new List<Distribution>();
            SimulationTable = new List<SimulationCase>();
            PerformanceMeasures = new PerformanceMeasures();
        }

        ///////////// INPUTS /////////////

        public int OrderUpTo { get; set; }
        public int ReviewPeriod { get; set; }
        public int NumberOfDays { get; set; }
        public int StartInventoryQuantity { get; set; }
        public int StartLeadDays { get; set; }
        public int StartOrderQuantity { get; set; }
        public List<Distribution> DemandDistribution { get; set; }
        public List<Distribution> LeadDaysDistribution { get; set; }

        ///////////// OUTPUTS /////////////

        public List<SimulationCase> SimulationTable { get; set; }
        public PerformanceMeasures PerformanceMeasures { get; set; }

        public void Calculate1_CummProbability_RandomDigitAssigmint(List<int> demand, List<Decimal> prob)
        {
            DemandDistribution = new List<Distribution>();
            for (int i = 0; i < demand.Count; i++)
            {

                if (i == 0)
                {
                    DemandDistribution.Add(new Distribution());
                    DemandDistribution[i].Value = demand[i];
                    DemandDistribution[i].Probability = prob[i];
                    DemandDistribution[i].CummProbability = prob[i];
                    DemandDistribution[i].MinRange = 1;
                    DemandDistribution[i].MaxRange = ((int)(DemandDistribution[i].CummProbability * 100));
                }
                else
                {
                    DemandDistribution.Add(new Distribution());
                    DemandDistribution[i].Value = demand[i];
                    DemandDistribution[i].Probability = prob[i];
                    DemandDistribution[i].CummProbability = prob[i] + DemandDistribution[i - 1].CummProbability;
                    DemandDistribution[i].MinRange = ((int)(DemandDistribution[i - 1].CummProbability * 100)) + 1;
                    DemandDistribution[i].MaxRange = ((int)(DemandDistribution[i].CummProbability * 100));
                }
            }
        }
        public void Calculate2_CummProbability_RandomDigitAssigmint(List<int> time, List<Decimal> prob)
        {
            LeadDaysDistribution = new List<Distribution>();
            for (int i = 0; i < time.Count; i++)
            {

                if (i == 0)
                {
                    LeadDaysDistribution.Add(new Distribution());
                    LeadDaysDistribution[i].Value = time[i];
                    LeadDaysDistribution[i].Probability = prob[i];
                    LeadDaysDistribution[i].CummProbability = prob[i];
                    LeadDaysDistribution[i].MinRange = 1;
                    LeadDaysDistribution[i].MaxRange = ((int)(LeadDaysDistribution[i].CummProbability * 100));
                }
                else
                {
                    LeadDaysDistribution.Add(new Distribution());
                    LeadDaysDistribution[i].Value = time[i];
                    LeadDaysDistribution[i].Probability = prob[i];
                    LeadDaysDistribution[i].CummProbability = prob[i] + LeadDaysDistribution[i - 1].CummProbability;
                    LeadDaysDistribution[i].MinRange = ((int)(LeadDaysDistribution[i - 1].CummProbability * 100)) + 1;
                    LeadDaysDistribution[i].MaxRange = ((int)(LeadDaysDistribution[i].CummProbability * 100));
                }
            }
        }
        int get_distribution(int rand_value, List<Distribution> list_distribution)
        {
            for (int i = 0; i < list_distribution.Count; i++)
            {
                if (rand_value >= list_distribution[i].MinRange && rand_value <= list_distribution[i].MaxRange)
                {
                    return list_distribution[i].Value;
                }
            }
            return 0;
        }
       
        public void system_output()
        {
            SimulationCase sim_case;
            SimulationTable = new List<SimulationCase>();
            Random rand = new Random();
            int quantity = 0;
            int DaysUntilOrder= StartLeadDays;
            bool flag = true;
            decimal EndingInventoryAverage = 0;
            decimal ShortageQuantityAverage = 0;
            for (int i = 0; i < NumberOfDays; ++i)
            {
                int rand_demand = rand.Next(1, 100);
                int rand_time = rand.Next(1, 100);
                sim_case = new SimulationCase();
              
                sim_case.RandomDemand= rand_demand;
                sim_case.Demand = get_distribution(rand_demand, DemandDistribution);
               

                if (i == 0)
                {
                    sim_case.Day = 1;
                    sim_case.Cycle = 1;
                    sim_case.DayWithinCycle = 1;
                    sim_case.BeginningInventory = StartInventoryQuantity;
                    sim_case.RandomDemand = rand_demand;
                    sim_case.Demand= get_distribution(rand_demand, DemandDistribution);
                    if ((sim_case.BeginningInventory - sim_case.Demand) > 0) { sim_case.EndingInventory = sim_case.BeginningInventory - sim_case.Demand; }
                    if((sim_case.Demand - sim_case.BeginningInventory)>0) { sim_case.ShortageQuantity=(sim_case.Demand - sim_case.BeginningInventory);}
                    DaysUntilOrder = StartLeadDays - 1;
                }
                else
                {
                    sim_case.Day = SimulationTable[i-1].Day+1;
                    int cycle = sim_case.Day / ReviewPeriod;
                    if (sim_case.Day % ReviewPeriod != 0)
                    {
                        sim_case.Cycle = cycle + 1;
                    }
                    else
                    {
                        sim_case.Cycle = cycle;
                    }
                    sim_case.DayWithinCycle = SimulationTable[i - 1].DayWithinCycle + 1;
                    if (sim_case.DayWithinCycle > ReviewPeriod){sim_case.DayWithinCycle = 1;}
                    sim_case.BeginningInventory = SimulationTable[i - 1].EndingInventory + quantity;
                    sim_case.RandomDemand = rand_demand;
                    sim_case.Demand = get_distribution(rand_demand, DemandDistribution);
                    if ((sim_case.BeginningInventory - sim_case.Demand - SimulationTable[i - 1].ShortageQuantity) >= 0) { sim_case.EndingInventory = sim_case.BeginningInventory - sim_case.Demand-SimulationTable[i-1].ShortageQuantity;}
                    else  { sim_case.ShortageQuantity = SimulationTable[i-1].ShortageQuantity+(sim_case.Demand - sim_case.BeginningInventory); }
                    if (sim_case.DayWithinCycle == ReviewPeriod)
                    {
                        sim_case.OrderQuantity = OrderUpTo - (sim_case.EndingInventory) + (sim_case.ShortageQuantity);
                        sim_case.RandomLeadDays = rand_time;
                        sim_case.LeadDays = get_distribution(rand_time, LeadDaysDistribution);
                        DaysUntilOrder = sim_case.LeadDays;
                    }
                    else if (DaysUntilOrder != 0)
                    {
                        DaysUntilOrder--;
                        if (DaysUntilOrder == 0)
                        {
                            
                            if (flag )
                            {quantity = StartOrderQuantity;
                               
                                flag = false;
                            }
                            else {  quantity = SimulationTable[((sim_case.Cycle-1) * ReviewPeriod)-1].OrderQuantity; }
                            
                        }
                    }
                    else { quantity = 0; }
                }
                
                //rest calculations
                SimulationTable.Add(sim_case);
                ShortageQuantityAverage = ShortageQuantityAverage + sim_case.ShortageQuantity;
                EndingInventoryAverage = EndingInventoryAverage + sim_case.EndingInventory;
            }
            PerformanceMeasures.EndingInventoryAverage =EndingInventoryAverage/SimulationTable.Count;
            PerformanceMeasures.ShortageQuantityAverage = ShortageQuantityAverage/SimulationTable.Count;
            
        
        }
        
    }
}
