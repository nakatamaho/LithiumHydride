namespace Microsoft.Quantum.Chemistry.Samples.LithiumHydride {
    
    open Microsoft.Quantum.Primitive;
    open Microsoft.Quantum.Canon;
    open Microsoft.Quantum.Extensions.Convert;
    open Microsoft.Quantum.Extensions.Math;
    open Microsoft.Quantum.Chemistry.JordanWigner;   

    operation GetEnergyByTrotterization (qSharpData : JordanWignerEncodingData, nBitsPrecision : Int, trotterStepSize : Double, trotterOrder : Int) : (Double, Double) {
        let (nSpinOrbitals, fermionTermData, statePrepData, energyOffset) = qSharpData!;
        let (nQubits, (rescaleFactor, oracle)) = TrotterStepOracle(qSharpData, trotterStepSize, trotterOrder);
        let statePrep = PrepareTrialState(statePrepData, _);
        let phaseEstAlgorithm = RobustPhaseEstimation(nBitsPrecision, _, _);
        let estPhase = EstimateEnergy(nQubits, statePrep, oracle, phaseEstAlgorithm);
        let estEnergy = estPhase * rescaleFactor + energyOffset;
        return (estPhase, estEnergy);
    }
}


