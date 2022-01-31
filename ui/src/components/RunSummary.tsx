import React from "react";
import { useParams } from "react-router";
import { Col, Container, Row } from "react-bootstrap";
import { GridPoint, IGridAnalitics, LostRobot } from "./Model/IGridAnalitics";
import { Position } from "./Model/IPosition";
import { downloadHandler } from "../common/downloadHandler";
import { GetGridAnaliticsData } from "../services/GridAnaliticsApiRequest";
import { GetRobotsByRunId } from "../services/RobotsApiRequest";
import { GridPointElement} from "../common/GridPointElement";
import { StatisticRow } from "../common/StatisticRow";

export const RunSummary = () =>
{
  const [robots, setRobots] = React.useState(0);
  const [data, setData] = React.useState<IGridAnalitics>();
  const { id } = useParams();

  const gridWidth: number = data? data.gridSize.x * 40 : 0;

  const fetchDataAsync = async () => {
    if (id)
    {
      const analiticsResponse = await GetGridAnaliticsData(id);
      setData(analiticsResponse.data);
      
      const robotsResponse = await GetRobotsByRunId(id);
      setRobots(robotsResponse.data);
    }
  }

  React.useEffect(() => {
      fetchDataAsync();
  }, []);

   return (
      <React.Fragment>
        {id && <Container className="pageMargins" fluid="lg">
          <Row className="align-items-center justify-content-md-center" >
            <Col className="col-md-auto">
              <button type="button" className="btn btn-warning" onClick={() => downloadHandler(id)}>Get Result File</button>
            </Col>
            <Col className="col-md-auto">
              {GenerateRobotDropDownList(robots, id)}
            </Col>
          </Row>
          <Row className="align-items-center justify-content-md-center m-2">
            <Col className="justify-content-center col-md-3">
              {data && GenerateStatistics(data, robots)}
            </Col>
          </Row>
        </Container>}
        {data && <div className="grid-position ">
          {GenerateGridRows(data.gridSize, data.gridPoints, data.lostRobots)}
        </div>}
      </React.Fragment>
    )
}

function GenerateStatistics(gridData: IGridAnalitics, noOfRobots: number): JSX.Element
{
  return (
    <React.Fragment>
      <StatisticRow name="Number of Robots: " value={noOfRobots} />
      <StatisticRow name="Lost Robots: " value={gridData.lostRobots.length} />
      <StatisticRow name="Total Grid Area (abs): " value={gridData.discoveredArea.totalArea} />
      <StatisticRow name="Discovered Area (abs): " value={gridData.discoveredArea.discoveredAreaAbsolute} />
      <StatisticRow name="Discovered Area (%): " value={gridData.discoveredArea.discoveredAreaPercent} />
    </React.Fragment>
  )
}

function GenerateGridRows(gridSize: Position, gridPoints: GridPoint[], lostRobots: LostRobot[]): JSX.Element[]
{
  var rows: JSX.Element[] = [];
  for (var i = gridSize.y; i >= 0; i--)
  {
    rows.push(<div >
      {GenerateGridColumns(gridSize, gridPoints, lostRobots, i)}
    </div>)
  }
  rows.push(<div >{GenerateXAxis(gridSize.x)}</div>)
  return rows;
}

function GenerateGridColumns(gridSize: Position, gridPoints: GridPoint[], lostRobots: LostRobot[], rowNo: number): JSX.Element[]
{
  var columns: JSX.Element[] = [];

  var axisYBStyle: string = "grid-y-padding ";
  
  if (rowNo < 10)
  {
    axisYBStyle += "axis-y-margin-small ";
  }


  columns.push(<span className={axisYBStyle}>{rowNo}</span>);
  for (var i = 0; i <= gridSize.x; i++)
  {
    var gridPoint: GridPoint[] = gridPoints.filter(gp => gp.coordinates.x == i && gp.coordinates.y == rowNo);
    var isDiscovered: boolean = gridPoint.length > 0;
    var noOfRobots: number = isDiscovered? gridPoint[0].robotsNumber : 0;
    var isLost: boolean = noOfRobots > 0? lostRobots.some(lr => lr.position.x == i && lr.position.y == rowNo) : false;

    console.log(isDiscovered, i, rowNo);

    columns.push(<GridPointElement x={i} y={rowNo} isDiscovered={isDiscovered} isLost={isLost} noOfRobots={noOfRobots}/>)
  }
  return columns;
}

function GenerateXAxis(gridX: number): JSX.Element[]
{
  var columns: JSX.Element[] = [];
    
  columns.push(<span className="grid-x-axis-position-placeholder"></span>);
  for (var i = 0; i <= gridX; i++)
  {
    columns.push(<span className="grid-x-axis-position ">{i}</span>)
  }
  return columns;
}

function GenerateRobotDropDownList(numberOfRobots: number, runId: string): JSX.Element
{
  return(
    <div className="dropdown">
      <button className="btn btn-secondary dropdown-toggle" type="button" id="dropdownMenuButton1" data-bs-toggle="dropdown" aria-expanded="false">Robots</button>
      <ul className="dropdown-menu menu-scroll" aria-labelledby="dropdownMenuButton1">
        {GenerateRobotList(numberOfRobots, runId)}
      </ul>
    </div>
    )
}

function GenerateRobotList(numberOfRobots: number, runId:string): JSX.Element[]
{
  var robots: JSX.Element[] = [];

  const link: string = "/run/" + runId + "/robot/";

  for (var i = 1; i <= numberOfRobots; i++)
  {
    robots.push(<li><a className="dropdown-item" href={link + i}>Robot No. {i}</a></li>);
  }

  return robots;
}