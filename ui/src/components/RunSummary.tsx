import React from "react";
import { useParams } from "react-router";
import { Col, Container, Row } from "react-bootstrap";
import { IGridAnalitics} from "./Model/IGridAnalitics";
import { downloadHandler } from "../common/downloadHandler";
import { GetGridAnaliticsData } from "../services/GridAnaliticsApiRequest";
import { GetRobotsByRunId } from "../services/RobotsApiRequest";
import { StatisticRow } from "../common/StatisticRow";
import { generateGrid } from "../common/generateGrid";

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
              {generateRobotDropDownList(robots, id)}
            </Col>
          </Row>
          <Row className="align-items-center justify-content-md-center m-2">
            <Col className="justify-content-center col-md-3">
              {data && generateStatistics(data, robots)}
            </Col>
          </Row>
        </Container>}
        {data && <div className="grid-position ">
          {generateGrid(data.gridSize, data.gridPoints, data.lostRobots)}
        </div>}
      </React.Fragment>
    )
}

function generateStatistics(gridData: IGridAnalitics, noOfRobots: number): JSX.Element
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

function generateRobotDropDownList(numberOfRobots: number, runId: string): JSX.Element
{
  return(
    <div className="dropdown">
      <button className="btn btn-secondary dropdown-toggle" type="button" id="dropdownMenuButton1" data-bs-toggle="dropdown" aria-expanded="false">Robots</button>
      <ul className="dropdown-menu menu-scroll" aria-labelledby="dropdownMenuButton1">
        {generateRobotList(numberOfRobots, runId)}
      </ul>
    </div>
    )
}

function generateRobotList(numberOfRobots: number, runId:string): JSX.Element[]
{
  var robots: JSX.Element[] = [];

  const link: string = "/run/" + runId + "/robot/";

  for (var i = 1; i <= numberOfRobots; i++)
  {
    robots.push(<li><a className="dropdown-item" href={link + i}>Robot No. {i}</a></li>);
  }

  return robots;
}