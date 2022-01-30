import React from "react";
import { useNavigate, NavigateFunction } from "react-router";
import { Col, Container, Row } from "react-bootstrap";
import { IDataSet } from "./Model/IDataSet";
import { GetDataSets, UploadFile, DeleteDataSet } from '../services/DataSetApiRequest';
import { downloadHandler } from "../common/downloadHandler";
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
  const isData = data.length > 0;

  const fetchDataAsync = async () => {
    const response = await GetDataSets();
    setData(response.data)};

  React.useEffect(() => {
      fetchDataAsync();
  }, []);

  React.useEffect(() => {
    setPages(Math.ceil(data.length/displayedItem));
  }, [data, displayedItem])

  async function onFileUploadHandler(selectedFile: File | undefined, runName: string): Promise<void> {
    if(selectedFile)
    {
      await UploadFile(selectedFile, runName)     
      await fetchDataAsync();
    };
  }

  const deleteHandler = async (runId: string) => {
    await DeleteDataSet(runId);
    var newData = data.filter(x => x.runId != runId);
    setData(newData);
  }

  const isPaginated: boolean = data.length > displayedItem;

  var paginatedData: IDataSet[] = isPaginated? Paginate(data, page, displayedItem) : data;

  return (
    <React.Fragment>
      <Container className="pageMargins lg" fluid="lg">
        <Row className="justify-content-md-center">          
          <input className="form-control upload-form" type="file" onChange={(event) => setSelectedFile(event.target.files? event.target.files[0] : undefined)} />
        </Row>
        <Row className="justify-content-md-center">
          <input className="form-control upload-form" placeholder="Assign custom name for the new run..." onChange={(event) => setSelectedName(event.target.value)} />
        </Row>
        <Row className="justify-content-md-center">
          <button type="button" className="m-2 btn btn-success upload-form" onClick={() => onFileUploadHandler(selectedFile, selectedName)}>Upload file</button>
        </Row>
        <Row className="justify-content-md-center">
          <button type="button" className="btn btn-warning upload-form" onClick={async () => await fetchDataAsync()}>Refresh</button>
        </Row>
        {isData && <Row className="m-3 align-items-center">
          <Col>
          <label className="d-flex justify-content-center bold-text">OR</label>
          <label className="d-flex justify-content-center bold-text">choose already finished run</label>
          </Col>
        </Row>}
        {isData && <Row className="justify-content-md-center listHeader row-cols-2" style={{position: "relative"}}>          
          <Col>
            <Row className=" listHeader data-set-rows">
              <Col className="d-flex justify-content-center col-4">Run name / File Name</Col>
              <Col className="d-flex justify-content-center col-2">Date (UTC)</Col>
              <Col className="d-flex justify-content-center col-2"></Col>
            </Row>
            {GenerateDataSetList(navigate, downloadHandler, deleteHandler, paginatedData)}
            {isPaginated && <Row className="justify-content-md-center">
              <Col className="d-flex justify-content-center col-4 pagination-wrapper">
                <Pagination page={page} pages={pages} onClick={setPage} />
              </Col>
            </Row>}
          </Col>
        </Row>}
      </Container>
    </React.Fragment>
  )
}

function Paginate(data: IDataSet[], page: number, elementNumber: number): IDataSet[] {
  const startIndex = (page - 1) * elementNumber;
  const endIndex = data.length > startIndex + elementNumber ? startIndex + elementNumber : data.length;
  return data.slice(startIndex, endIndex);
}

function GenerateDataSetList(navigate: NavigateFunction, downloadHandler: (runId: string) => void, deleteHandler: (runId: string) => void, paginatedData: IDataSet[]): JSX.Element
{
  return <React.Fragment>
  {paginatedData.map((x) => { return (
    <Row className="m-2 justify-content-center data-set-rows">
      <Col className="d-flex justify-content-center col-3">
        <button type="button" className="btn btn-link" onClick={() => navigate('/run/' + x.runId)}>{x.name}</button>
      </Col>
      <Col className="d-flex justify-content-center col-4">
        {x.generationDate}
      </Col>
      <Col className="d-flex justify-content-center col-3">
        {x.runId && <button type="button" className="btn btn-warning" onClick={() => downloadHandler(x.runId)}>Get Result File</button>}
      </Col>
      <Col className="d-flex justify-content-center">
        <button type="button" className="btn btn-secondary" onClick={() => deleteHandler(x.runId)}>DELETE</button>
      </Col>
    </Row>
    )})}
    </React.Fragment>
}

