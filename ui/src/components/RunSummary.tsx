import React from "react";
import { useParams } from "react-router";
import { Col, Container, Row } from "react-bootstrap";
import { GridPoint, IGridAnalitics } from "./Model/IGridAnalitics";
import { downloadHandler } from "../common/downloadHandler";
import { GetGridAnaliticsData } from "../services/GridAnaliticsApiRequest";
import { GetRobotsByRunId } from "../services/RobotsApiRequest";
import { GridPointElement} from "../common/GridPointElement";

import { Pagination } from './utils/Pagination';

export const RunSummary = () =>
{
  const [robots, setRobots] = React.useState(0);
  const [data, setData] = React.useState<IGridAnalitics>();
  const {id} = useParams();

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
          <Row>
            <Col className="col-md-auto">
              <button type="button" className="btn btn-warning" onClick={() => downloadHandler(id)}>Get Result File</button>
            </Col>
            <Col className="col-md-auto">
              {GenerateRobotDropDownList(robots, id)}
            </Col>
          </Row>
          <Row className="align-items-center justify-content-md-center m-5">
            <Col className="justify-content-center col-md-auto">
              Statistics
            </Col>
          </Row>
        </Container>}
        {data && <div className="grid-position ">
          {GenerateDataSetList(data)}
        </div>}
      </React.Fragment>
    )
}


function GenerateDataSetList(gridData: IGridAnalitics): JSX.Element
{  
  {console.log("tre", gridData)}
  return (
    <React.Fragment>
      {GenerateGridRows(gridData)}
    </React.Fragment>
  )
}

function GenerateGridRows(gridData: IGridAnalitics): JSX.Element[]
{
  var rows: JSX.Element[] = [];
  for (var i = gridData.gridSize.y; i >= 0; i--)
  {
    rows.push(<div >
      {GenerateGridColumns(gridData, i)}
    </div>)
  }
  rows.push(<div >{GenerateXAxis(gridData.gridSize.x)}</div>)
  return rows;
}

function GenerateGridColumns(gridData: IGridAnalitics, rowNo: number): JSX.Element[]
{
  var columns: JSX.Element[] = [];

  var axisYBStyle: string = "grid-y-padding ";
  
  if (rowNo < 10)
  {
    axisYBStyle += "axis-y-margin-small ";
  }


  columns.push(<span className={axisYBStyle}>{rowNo}</span>);
  for (var i = 0; i <= gridData.gridSize.x; i++)
  {
    var gridPoint: GridPoint[] = gridData.gridPoints.filter(gp => gp.coordinates.x == i && gp.coordinates.y == rowNo);
    var isDiscovered: boolean = gridPoint.length > 0;
    var noOfRobots: number = isDiscovered? gridPoint[0].robotsNumber : 0;
    var isLost: boolean = noOfRobots > 0? gridData.lostRobots.some(lr => lr.position.x == i && lr.position.y == rowNo) : false;

    console.log(isDiscovered, i, rowNo);

    columns.push(<span className="grid-point-size-column">
      <GridPointElement x={i} y={rowNo} isDiscovered={isDiscovered} isLost={isLost} noOfRobots={noOfRobots}/>
    </span>)
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