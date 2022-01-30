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
      <Container className="pageMargins lg" fluid="lg">
        <Row>
          <Col>
            {id}
          </Col>
          <Col>
            {id && <button type="button" className="btn btn-warning" onClick={() => downloadHandler(id)}>Get Result File</button>}
          </Col>
        </Row>
        <Row>
        {data && GenerateDataSetList(data)}
        </Row>
      </Container>
    </React.Fragment>
  )
}

function onClickDataNameHandler(runId: string)
{
  console.log("runId", runId);
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
  for (var i = gridData.gridSize.x; i >= 0; i--)
  {
    rows.push(<Row>
      {GenerateGridColumns(gridData, i)}
    </Row>)
  }
  return rows;
}

function GenerateGridColumns(gridData: IGridAnalitics, rowNo: number): JSX.Element[]
{
  var columns: JSX.Element[] = [];
  for (var i = 0; i <= gridData.gridSize.y; i++)
  {
    columns.push(<Col>
      <label>{i} {rowNo}</label>
    </Col>)
  }
  return columns;
}


const deleteHandler = async (runId: string) => {

}

