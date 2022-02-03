import React, { useState } from "react";
import { useParams } from "react-router";
import { Col, Container, Row } from "react-bootstrap";
import { generateGrid } from "../common/generateGrid";
import { StatisticRow } from "../common/StatisticRow";
import { RobotStep, getRectangularMoveCommandName } from "./Model/IRobotStep";
import { getGridByRunId } from "../services/DataSetApiRequest";
import { getRobotByRunIdRobotId } from "../services/RobotsApiRequest";
import { Position, getOrientationStateName } from "./Model/IPosition";
import { DisplayPoint } from "./Model/DisplayPoint";

export const RobotSummary = () =>
{
    const {runId, robotId} = useParams();
    const [gridSize, setGridSize] = useState<Position>({x: 0, y: 0});
    const [steps, setSteps] = useState<RobotStep[]>([]);
    const [currentStep, setCurrentStep] = useState<RobotStep>();
    const [stepNo, setStepNo] = useState(1);

    const fetchDataAsync = async () => {
        if (runId && robotId)
        {
          const response = await getRobotByRunIdRobotId(runId, robotId);
          setSteps(response.data);
          assignCurrentStep(response.data, stepNo);

          const gridResponse = await getGridByRunId(runId)
          setGridSize(gridResponse.data);
        }
      }

    function assignCurrentStep(steps: RobotStep[], stepNo: number): void
    {
        const newStep: RobotStep = steps.filter(s => s.stepNumber === stepNo)[0]; 
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


    const stepsToDisplay: RobotStep[] = steps.filter(s => s.stepNumber <= stepNo);
    const displayPoints: DisplayPoint[] = mapDataForDisplay(gridSize, stepsToDisplay);

    return (
        <React.Fragment>
            <Container className="pageMargins">
            {runId && <Row className=" justify-content-md-center navigate-row" >
              <Col className="col-md-auto">
                <a className="btn-link" href={"/run/" + runId}>Back</a>
              </Col>
            </Row>}
            {currentStep && <React.Fragment>
                <Row className="justify-content-center">
                    <Col className="col-3" >
                        <button type="button" className={"btn btn-outline-primary step-change-button"} onClick={() => setStepNo(stepNo - 1)} disabled={isPreviousDisabled}>Previous</button>
                        <input type="number" className="robot-input-step"  min="0" max={steps.length} pattern="/^[0-9\b]+$/" value={stepNo.toString()} onChange={(event) => inputStepHandler(event.target.value)} />
                        <button type="button" className="btn btn-outline-primary step-change-button" onClick={() => setStepNo(stepNo + 1)} disabled={isNextDisabled}>Next</button>
                    </Col>
                </Row>
                <Row className=" justify-content-center">
                    <Col className=" col-3">
                        <StatisticRow name={"Number of Steps: "} value={steps.length}/>
                        <StatisticRow name={"X: "} value={currentStep.position.x}/>
                        <StatisticRow name={"Y: "} value={currentStep.position.y}/>
                        <StatisticRow name={"Orientation: "} value={getOrientationStateName(currentStep.orientation)}/>
                        {currentStep.command ? <StatisticRow name={"Command: "} value={getRectangularMoveCommandName(currentStep.command)}/> : <StatisticRow name={"Lost: "} value={currentStep.isLost? "Yes" : "No"}/>}                        
                    </Col>
                </Row>
                {<div className="grid-position ">
                    {generateGrid(gridSize, displayPoints)}
                    </div>}
                    </React.Fragment>}
            </Container>           
        </React.Fragment>
    )
}

function mapDataForDisplay(gridSize: Position, robotSteps: RobotStep[]) : DisplayPoint[] {
    let displayPoints: DisplayPoint[]=[];

    let currentStep: RobotStep = robotSteps[robotSteps.length-1];
  
    for (let i = 0; i <= gridSize.y; i++)
    {
      for(let j = 0; j <= gridSize.x; j++)
      {
        let robotStep = robotSteps.filter(rs => rs.position.x === j && rs.position.y === i).sort(x => x.stepNumber)[0];
        let isLostRobot = robotStep? robotStep.isLost : false;
        let orientation = !!robotStep && robotStep.position.x === currentStep.position.x && robotStep.position.y === currentStep.position.y ? robotStep.orientation : undefined;
        let displayPoint = new DisplayPoint({x: j, y: i}, !!robotStep, isLostRobot, undefined, orientation);
        displayPoints.push(displayPoint);
      }
    }
  
    return displayPoints;
  }