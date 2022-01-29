import React from "react";
import { useParams } from "react-router";
import { Col, Container, Row } from "react-bootstrap";
import FileDownload  from "js-file-download";
import { IDataSet } from "./Model/IDataSet";
import { GetRobotsResults } from "../services/RobotsApiRequest";

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

  const downloadHandler = async (runId: string) => {
    const file = await GetRobotsResults(runId);
    console.log("file: ", file.data);
    FileDownload(file.data, 'TestName.txt');
  }

  const isPaginated: boolean = data.length > displayedItem;
  
  var paginatedData: IDataSet[];

  paginatedData = isPaginated? Paginate(data, page, displayedItem) : data;


   return (
    <React.Fragment>
      <Container>
        <Row>
          <Col>Upload File</Col>
          <Col>
            {id}
          </Col>
          <Col>
            {id && <button type="button" className="btn btn-success" onClick={() => downloadHandler(id)}>DOWNLOAD</button>}
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