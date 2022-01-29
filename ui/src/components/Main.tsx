import React from "react";
import { useNavigate, NavigateFunction } from "react-router";
import { Col, Container, Row } from "react-bootstrap";
import { IDataSet } from "./Model/IDataSet";
import { GetDataSets, UploadFile } from '../services/DataSetApiRequest';

import { Pagination } from './utils/Pagination';

export const Main = () =>
{
  const navigate = useNavigate();
  const displayedItem: number = 10;
  const [page, setPage] = React.useState(1);
  const [pages, setPages] = React.useState(1);
  const [data, setData] = React.useState<IDataSet[]>([]);
  const [selectedFile, setSelectedFile] = React.useState<File>();
  const [selectedName, setSelectedName] = React.useState<string>("");

  const fetchData = async () => {
    const response = await GetDataSets();
    setData(response.data)};

  React.useEffect(() => {
      fetchData();
  }, []);

  React.useEffect(() => {
    setPages(Math.ceil(data.length/displayedItem));
  }, [data, displayedItem])

  async function onFileUploadHandler(selectedFile: File | undefined, runName: string): Promise<void> {
    if(selectedFile)
    {
      await UploadFile(selectedFile, runName)     
      fetchData();
    };
  }

  const isPaginated: boolean = data.length > displayedItem;  

  var paginatedData: IDataSet[] = isPaginated? Paginate(data, page, displayedItem) : data;

  console.log('naem', selectedName);


   return (
    <React.Fragment>
      <Container className="pageMargins" fluid="sm">
        <Row className="justify-content-md-center">          
          <input className="form-control upload-form" type="file" onChange={(event) => setSelectedFile(event.target.files? event.target.files[0] : undefined)} />
        </Row>
        <Row className="justify-content-md-center">
          <input className="form-control upload-form" placeholder="Assign custom name for the new run..." onChange={(event) => setSelectedName(event.target.value)} />
        </Row>
        <Row className="justify-content-md-center">
          <button type="button" className="m-2 btn btn-success upload-form" onClick={() => onFileUploadHandler(selectedFile, selectedName)} >Upload file</button>
        </Row>
        <Row className="align-items-center ">
          <Col>
          <label className="d-flex justify-content-center">OR</label>
          <label className="d-flex justify-content-center">choose already finished run</label>
          </Col>
        </Row>
        <Row className="justify-content-md-center listHeader">          
          <Col>
            <Row className="center-row listHeader">
              <Col className="d-flex justify-content-center">Run name / File Name</Col>
              <Col className="d-flex justify-content-center">Date (UTC)</Col>
              <Col className="d-flex justify-content-center"></Col>
            </Row>
            {GenerateDataSetList(navigate, paginatedData)}
            {<Row>
              <Col>
                {isPaginated && <Pagination page={page} pages={pages} onClick={setPage} />}
              </Col>
            </Row>}
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

function GenerateDataSetList(navigate: NavigateFunction, paginatedData: IDataSet[]): JSX.Element
{
  return <React.Fragment>
  {paginatedData.map((x) => { return (
    <Row className="m-2 justify-content-center">
      <Col className="d-flex justify-content-center">
        <button type="button" className="btn btn-link" onClick={() => navigate('/run/' + x.runId)}>{x.name}</button>
      </Col>
      <Col className="d-flex justify-content-center">
        {x.generationDate}
      </Col>
      <Col className="d-flex justify-content-center">
        <button type="button" className="btn btn-warning">DELETE</button>
      </Col>
    </Row>
    )})}
    </React.Fragment>
}

