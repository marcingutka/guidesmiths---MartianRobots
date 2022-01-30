import React from "react";
import { useParams } from "react-router";
import { Col, Container, Row } from "react-bootstrap";
import { IDataSet } from "./Model/IDataSet";
import { downloadHandler } from "../common/downloadHandler";

import { Pagination } from './utils/Pagination';

export const RunSummary = () =>
{
  const displayedItem: number = 10;
  const [page, setPage] = React.useState(1);
  const [pages, setPages] = React.useState(1);
  const [data, setData] = React.useState<IDataSet[]>([]);
  const {id} = useParams();

  React.useEffect(() => {
    setPages(Math.ceil(data.length/displayedItem));
  }, [data, displayedItem])

  

  const isPaginated: boolean = data.length > displayedItem;
  
  var paginatedData: IDataSet[];

  paginatedData = isPaginated? Paginate(data, page, displayedItem) : data;


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
      </Container>
    </React.Fragment>
  )
}

function Paginate(data: IDataSet[], page: number, elementNumber: number): IDataSet[] {
  const startIndex = (page - 1) * elementNumber;
  const endIndex = data.length > startIndex + elementNumber ? startIndex + elementNumber : data.length;
  return data.slice(startIndex, endIndex);
}

function onClickDataNameHandler(runId: string)
{
  console.log("runId", runId);
}