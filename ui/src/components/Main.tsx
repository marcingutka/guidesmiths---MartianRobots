import React from "react";
import { Col, Container, Row } from "react-bootstrap";
import axios from 'axios';

import { Pagination } from './utils/Pagination';

export const Main = () =>
{
  const displayedItem: number = 10;
  const [page, setPage] = React.useState(1);
  const [pages, setPages] = React.useState(1);
  const [data, setData] = React.useState<IGetResult[]>([]);

  React.useEffect(() => {
    const fetchData = async () => {const res = await axios.get('https://localhost:7236/api/dataSet');setData(res.data)}; fetchData();
  }, []);

  React.useEffect(() => {
    setPages(Math.ceil(data.length/displayedItem));
  }, [data, displayedItem])

  const isPaginated: boolean = data.length > displayedItem;
  
  var paginatedData: IGetResult[];

  paginatedData = isPaginated? Paginate(data, page, displayedItem) : data;


   return (
    <React.Fragment>
      <Container>
        <Row>
          <Col>Upload File</Col>
          <Col>
            <Row className="center-row">
              <Col className="d-flex justify-content-center">Name</Col>
              <Col className="d-flex justify-content-center">Date</Col>
              <Col className="d-flex justify-content-center"></Col>
            </Row>
            {paginatedData.map((x) => { return (
            <Row className="m-2">
              <Col className="d-flex justify-content-center">
                <button type="button" className="btn btn-link">{x.name}</button>
              </Col>
              <Col className="d-flex justify-content-center">
                {x.generationDate}
              </Col>
              <Col className="d-flex justify-content-center">
                <button type="button" className="btn btn-warning">DELETE</button>
              </Col>
            </Row>
            )})}
            {<Row>
              <Col>
                {isPaginated && <Pagination page={page} pages={pages} onClick={setPage} />}
              </Col>
            </Row>}
          </Col>
        </Row>
        Pages: {pages}
      </Container>
    </React.Fragment>
  )
}

function Paginate(data: IGetResult[], page: number, elementNumber: number): IGetResult[] {
  const startIndex = (page - 1) * elementNumber;
  const endIndex = data.length > startIndex + elementNumber ? startIndex + elementNumber : data.length;
  return data.slice(startIndex, endIndex);
}

export interface IGetResult {
  runId: string;
  name: string;
  generationDate: string;
}