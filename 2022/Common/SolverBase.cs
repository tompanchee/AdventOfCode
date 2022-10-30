using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AOCCommon
{
    public abstract class SolverBase : ISolver
    {
        protected Logger logger;
        protected string[] data;

        protected SolverBase(string path, Logger logger)
        {
            this.logger = logger;

            var day = GetType().GetCustomAttribute<DayAttribute>()?.DayNumber;
            if (day == null) throw new Exception("Could not resolve day number");
            var inputFile = $"{path}\\day{day:D2}.txt";
            data = File.ReadAllLines(inputFile);
            PostConstruct();          
        }

        protected virtual void PostConstruct() { }

        public abstract void Solve1();
     
        public abstract void Solve2();
    }
}
