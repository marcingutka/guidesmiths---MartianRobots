import { Position } from "../components/Model/IPosition";
import { GridPointElement} from "../common/GridPointElement";
import { DisplayPoint } from "../components/Model/DisplayPoint";

export function generateGrid(gridSize: Position, displayPoints: DisplayPoint[]): JSX.Element
{
  var rows: JSX.Element[] = [];
  
  const positionX: number = -gridSize.x - 800;

  for (var i = gridSize.y; i >= 0; i--)
  {
    rows.push(<div>
      {GenerateGridColumns(gridSize, displayPoints, i)}
    </div>)
  }
  rows.push(<div >{GenerateXAxis(gridSize.x)}</div>)
  return <div className="grid-position " style={{marginLeft: positionX}}>{rows}</div>;
}

function GenerateGridColumns(gridSize: Position, displayPoints: DisplayPoint[], rowNo: number): JSX.Element[]
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
    var displayPoint: DisplayPoint = displayPoints.filter(dp => dp.coordinates.x === i && dp.coordinates.y === rowNo)[0]

    columns.push(<GridPointElement x={i} y={rowNo} isDiscovered={displayPoint.isDiscovered} isLost={displayPoint.isLost} noOfRobots={displayPoint.robotsNumber?? 0} robotOrientation={displayPoint.orientation?? undefined}/>)
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