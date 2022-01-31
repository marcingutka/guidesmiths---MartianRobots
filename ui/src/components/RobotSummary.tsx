import React, { useState } from "react";
import { useParams } from "react-router";
import { Col, Container, Row } from "react-bootstrap";
import { generateGrid } from "../common/generateGrid";
import { StatisticRow } from "../common/StatisticRow";
import { IRobotStep, getRectangularMoveCommandName } from "./Model/IRobotStep";
import { Position } from "./Model/IPosition";
import { GetGridByRunId } from "../services/DataSetApiRequest";
import { GetRobotByRunIdRobotId } from "../services/RobotsApiRequest";
import { GridPoint } from "./Model/IGridAnalitics";
import { GridPointElement } from "../common/GridPointElement";

export const RobotSummary = () =>
{
    const {runId, robotId} = useParams();
    const [gridSize, setGridSize] = useState<Position>({x: 0, y: 0});
    const [steps, setSteps] = useState<IRobotStep[]>([]);
    const [currentStep, setCurrentStep] = useState<IRobotStep>();
    const [stepNo, setStepNo] = useState(1);

    const fetchDataAsync = async () => {
        if (runId && robotId)
        {
          const response = await GetRobotByRunIdRobotId(runId, robotId);
          setSteps(response.data);
          assignCurrentStep(response.data, stepNo);

          const gridResponse = await GetGridByRunId(runId)
          setGridSize(gridResponse.data);
        }
      }

    function assignCurrentStep(steps: IRobotStep[], stepNo: number): void
    {
        const newStep: IRobotStep = steps.filter(s => s.stepNumber === stepNo)[0]; 
        setCurrentStep(newStep);    
    }
    
    React.useEffect(() => {
        fetchDataAsync();
    }, []);

    React.useEffect(() => {
        assignCurrentStep(steps, stepNo)
    }, [stepNo]);

    function inputStepHandler(input: string) : void {
        if (!input) return;

        const newStepNo = parseInt(input);
        if (newStepNo > steps.length) setStepNo(steps.length);
        else if (newStepNo <= 0) setStepNo(1);
        else setStepNo(newStepNo);
    }

    console.log("Steps", steps);

    const isPreviousDisabled: boolean = stepNo <= 1;
    const isNextDisabled: boolean = stepNo >= steps.length;


    //const stepsTo = generateGridPoints();

    return (
        <React.Fragment>
            <label>StepNo: {stepNo}</label>
            <label>CurrentStep: {currentStep?.stepNumber}</label>
            {currentStep && <Container className="pageMargins" fluid="lg">
                <Row className="align-items-center justify-content-md-center">
                    <Col className="col-md-auto">
                        <button type="button" className={"btn btn-outline-primary"} onClick={() => setStepNo(stepNo - 1)} disabled={isPreviousDisabled}>Previous</button>
                        <input className="robot-input-step " type="number" min="0" max={steps.length} pattern="/^[0-9\b]+$/" value={stepNo.toString()} onChange={(event) => inputStepHandler(event.target.value)} />
                        <button type="button" className="btn btn-outline-primary" onClick={() => setStepNo(stepNo + 1)} disabled={isNextDisabled}>Next</button>
                    </Col>
                </Row>
                <Row className="align-items-center justify-content-md-center">
                    <Col className="justify-content-center col-sm-2">
                        <StatisticRow name={"Number of Steps: "} value={steps.length}/>
                        <StatisticRow name={"X: "} value={currentStep.position.x}/>
                        <StatisticRow name={"Y: "} value={currentStep.position.y}/>
                        <StatisticRow name={"Orientation: "} value={currentStep.orientation}/>
                        {currentStep.command ? <StatisticRow name={"Command: "} value={getRectangularMoveCommandName(currentStep.command)}/> : <StatisticRow name={"Lost: "} value={currentStep.isLost? "Yes" : "No"}/>}                        
                    </Col>
                </Row>
            </Container>}
            <div>
            </div>
            
        </React.Fragment>
    )
}