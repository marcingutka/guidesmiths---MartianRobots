import { GridPoint, LostRobot } from "../components/Model/IGridAnalitics";
import { Position } from "../components/Model/IPosition";
import { GridPointElement} from "../common/GridPointElement";

export function generateGrid(gridSize: Position, gridPoints: GridPoint[], lostRobots: LostRobot[]): JSX.Element[]
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