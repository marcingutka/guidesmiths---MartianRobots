import React from "react";
import { useParams } from "react-router";
import { Col, Container, Row } from "react-bootstrap";
import { IGridAnalitics } from "./Model/IGridAnalitics";
import { downloadHandler } from "../common/downloadHandler";
import { GetGridAnaliticsData } from "../services/GridAnaliticsApiRequest";

import { Pagination } from './utils/Pagination';

export const RunSummary = () =>
{
  const displayedItem: number = 10;
  const [page, setPage] = React.useState(1);
  const [pages, setPages] = React.useState(1);
  const [data, setData] = React.useState<IGridAnalitics>();
  const {id} = useParams();

  const fetchDataAsync = async () => {
    if (id)
    {
      const response = await GetGridAnaliticsData(id);
      setData(response.data)};
    }

  React.useEffect(() => {
      fetchDataAsync();
  }, []);

  



   return (
    <React.Fragment>
      <Container className="pageMargins" fluid="lg">
        <Row>
          <Col>
            {id}
          </Col>
          <Col>
            {id && <button type="button" className="btn btn-warning" onClick={() => downloadHandler(id)}>Get Result File</button>}
          </Col>
        </Row>
        <Row className="align-items-center justify-content-md-center m-5">
          <Col className="justify-content-center col-4">
            {data && GenerateDataSetList(data)}
          </Col>
        </Row>
      </Container>
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
  columns.push(<span className="grid-y-padding">{rowNo}</span>);
  for (var i = 0; i <= gridData.gridSize.x; i++)
  {
    columns.push(<span className="grid-point-size-column">
      <button className=" grid-point-size-column">{i} {rowNo}</button>
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

