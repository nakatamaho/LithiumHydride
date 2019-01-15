#region Using Statements
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Collections.Generic;
using Microsoft.Quantum.Simulation.Simulators;
using Microsoft.Quantum.Simulation.Core;
using System.Runtime.InteropServices;
using System.Linq;
using Mono.Options;
using Microsoft.Quantum.Chemistry;
#endregion

namespace Microsoft.Quantum.Chemistry.Samples.LithiumHydride
{
    internal static class LiHSimulation
    {
        internal static string[] bondLengths = new string[]
        {
            "0.800","1.000","1.200","1.400","1.500","1.550","1.580","1.600","1.624",
            "1.640","1.680","1.700","1.800","2.000","2.200","2.500","2.700","3.000",
            "3.200","3.500","4.000","5.000"
        };
        internal static string[] filenames = bondLengths.Select(o => $@"./IntegralData/YAML/LiHData/integrals_lih_sto-3g_{o}.nw.out.yaml").ToArray();
        internal static FermionHamiltonian[] hamiltonianData = filenames.Select(o => FermionHamiltonian.LoadFromYAML(o).Single()).ToArray();
        public static Int64 IntegratorOrder = 1;
        public static Int64 bitsOfPrecision = 10;
        public static Double trotterStepSize = 0.2;

        internal static (Double, Double) GetSimulationResult(int idxBond, string inputState = "Greedy")          {
               var hamiltonian = hamiltonianData.ElementAt(idxBond);
               var bondLength = Double.Parse(bondLengths[idxBond]);
               var jordanWignerEncoding = JordanWignerEncoding.Create(hamiltonian);
               using (var qSim = new QuantumSimulator())
               {
                   var qSharpData = jordanWignerEncoding.QSharpData(inputState);
                   Console.WriteLine($"Estimating at {bondLength} A;  {idxBond}-th bond length:");
                   var (phaseEst, energyEst) = GetEnergyByTrotterization.Run(qSim, qSharpData, bitsOfPrecision, trotterStepSize, IntegratorOrder).Result;
                    return (bondLength, energyEst);
                }
          }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var integratorOrder = 2L;
            var stepSize = 0.2;
            var bitsOfPrecision = 10L;
            var options = new OptionSet {
                { "o|integrator-order=", "Order of Trotter-Suzuki integrator", (Int64 o) => integratorOrder = o},
                { "s|step-size=", "Step size of Trotter-Suzuki integrator", (Double s) => stepSize = s},
                { "b|bits-precision=", "Bits of preicison in quantum phase estimation algorithm", (Int64 b) => bitsOfPrecision = b},
            };
            LiHSimulation.IntegratorOrder = integratorOrder;
            LiHSimulation.trotterStepSize = stepSize;
            LiHSimulation.bitsOfPrecision = bitsOfPrecision;

            string[] states = new string[] { "|G>", "|E1>", "|E2>", "|E3>", "|E4>", "|E5>" };
            foreach (var state in states) {
                foreach (var idxBond in Enumerable.Range(0, LiHSimulation.hamiltonianData.Length))  {
                    var (bondLength, energyEst) = LiHSimulation.GetSimulationResult(idxBond, state);
                    Console.WriteLine($"Energy estimate: {energyEst}; Bond length: {bondLength}; State: {state}");
                }
            }
       }
   }
}
